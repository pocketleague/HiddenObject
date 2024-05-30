namespace MyApp.HiddenObjects
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    public class GameManager : MonoBehaviour
    {
        #region enum
        public enum Mode { TwoDimensional, ThreeDimensional }
        #endregion
        #region variable
        public Mode mode = Mode.TwoDimensional;
        public HSObjectData[] nodes;
        [Min(0)]
        public float onHitEventDelay;
        public UnityEvent onHitEvent;
        public UnityEvent onGameIsOverEvent;
        [Min(0)]
        public float onGameIsOverEventDelay;
        //hint
        public bool hint;
        [Min(0)]
        public float hintDelay;
        public UnityEvent hintEvent;
        public Button hintBtn;
        public GameObject hintImage;
        public bool hintOnObject;
        public float hintCameraDistance = 1;
        [Min(0)]
        public float hintImageShowSecond = 1;
        //pixel hunting
        public bool pixelHunting;
        [Min(0)]
        public float pixelHuntingThresholdDelay;
        public UnityEvent pixelHuntEvent;
        public GameObject pixelHuntImage;
        //canvas
        public Vector3 canvasObjectScale = Vector3.one;
        public GameObject canvasObjects;
        [Min(1)]
        public int maxCanvasObjectsCount = 1;
        #region HideInInspector
        [HideInInspector] public List<HiddenObjectUI> _hoNodes;
        [HideInInspector] public int tapCount;
        [HideInInspector] public bool _gameIsOver;
        [HideInInspector] public RectTransform _hintImage_RectTransform;
        [HideInInspector] public RectTransform _pixelHuntImage_RectTransform;
        public Camera MainCamera { get; set; }
        public List<int> HuntedIDs { get; set; }
        public bool IsLock { get; set; }
        public bool GameIsOver { get { return _gameIsOver; } }
        public string HOResourcesPath { get { return HSGlobals.ProjectName + "/Prefabs/Object"; } }
        #endregion
        #region private
        private float lastPixelHuntingTime;
        private bool isLock;
        private bool lastPixelHuntingImageActive;
        private bool lastHintImageActive;
        private float lastHintImageTime;
        private bool hintShow;
        private bool pixelHuntShow;
        #endregion
        #region Editor
#if UNITY_EDITOR
        [HideInInspector] public bool parametersEnable = true;
        [HideInInspector] public bool hintParametersEnable = true;
        [HideInInspector] public bool pixelHuntParametersEnable = true;
        [HideInInspector] public bool canvasParametersEnable = true;
        [HideInInspector] public bool infoEnable = true;
#endif
        #endregion
        #endregion
        #region Functions
        private void Awake()
        {
            hint_Init();
            hiddenObjectUI_Init();
            huntedHSObjectDataID_Reset();
        }
        private void Start()
        {
            if (MainCamera == null) MainCamera = Camera.main;
            pixelHuntImage_setActive(false, true);
            hintImage_setActive(false, true);
            _gameIsOver = pixelHuntShow = hintShow = false;
            lastPixelHuntingTime = -pixelHuntingThresholdDelay;
            lastHintImageTime = -hintImageShowSecond;
            checkGameIsOver();
            if (hintImage != null) _hintImage_RectTransform = hintImage.GetComponent<RectTransform>();
            if (pixelHuntImage != null) _pixelHuntImage_RectTransform = pixelHuntImage.GetComponent<RectTransform>();
        }
        public void Update()
        {
            if (isLock || IsLock || GameIsOver) return;
            if (Input.GetMouseButtonDown(0))
            {
                if (allowPixelHunting())
                {
                    tapCount++;
                    var node = isHit();
                    if (node != null)
                    {
                        var data = getNode(node.referenceObjectId);
                        if (data != null)
                        {
                            huntedHSObjectDataID_Add(data.ID);
                            int index = getHiddenObjectUI_Index(data.ID);
                            if (index > -1 && _hoNodes[index].gameObject.activeSelf)
                            {
                                _hoNodes[index].OnDestroy();
                                _hoNodes.RemoveAt(index);
                                hiddenObjectUI_ShowAnotherNode();
                                node.SetActive(false);
                                invokeEvent(onHitEvent, onHitEventDelay);
                            }
                            if (checkGameIsOver())
                            {
                                invokeEvent(onGameIsOverEvent, onGameIsOverEventDelay);
                            }
                        }
                    }
                }
            }
        }
        private void FixedUpdate()
        {
            if (isLock || IsLock || GameIsOver) return;
            checkHintImageActive();
            checkPixelHuntActive();
        }
        #endregion
        #region functions
        #region HiddenObjectUI
        public int getHiddenObjectUI_Index(int id)
        {
            if (_hoNodes != null)
            {
                for (int i = 0; i < _hoNodes.Count; i++)
                {
                    if (_hoNodes[i].ID == id)
                    {
                        return i;
                    }
                }
            }
            return -1;
        }
        public void hiddenObjectUI_ShowAnotherNode()
        {
            if (_hoNodes != null)
            {
                for (int i = 0; i < _hoNodes.Count; i++)
                {
                    if (!_hoNodes[i].gameObject.activeSelf)
                    {
                        _hoNodes[i].SetActive(true);
                        return;
                    }
                }
            }
        }
        private void hiddenObjectUI_Init()
        {
            hiddenObjectUI_Destroy();
            _hoNodes = new List<HiddenObjectUI>();
            for (int i = 0; i < getNodes_Count(); i++)
            {
                var node = CreateHiddenObjectUI("Object " + i.ToString());
                if (node == null) continue;
                node.Init(nodes[i].ID, nodes[i].name, nodes[i].sprite);
                node.SetActive(i < maxCanvasObjectsCount);
                _hoNodes.Add(node);
            }
        }
        private void hiddenObjectUI_Destroy()
        {
            if (_hoNodes == null) return;
            while (_hoNodes.Count > 0)
            {
                if (_hoNodes[0] != null)
                {
                    _hoNodes[0].OnDestroy();
                }
                _hoNodes.RemoveAt(0);
            }
        }
        public HiddenObjectUI CreateHiddenObjectUI(string name = null)
        {
            var reference = (GameObject)Resources.Load(HOResourcesPath);
            if (reference == null) return null;
            var node = GameObject.Instantiate(reference);
            if (!string.IsNullOrEmpty(name)) node.name = name;
            if (canvasObjects != null)
            {
                node.transform.SetParent(canvasObjects.transform);
                var rt = node.GetComponent<RectTransform>();
                if (rt != null)
                {
                    rt.localScale = canvasObjectScale;
                }
            }

            HiddenObjectUI function = node.GetComponent<HiddenObjectUI>();
            if (function == null)
            {
                function = node.AddComponent<HiddenObjectUI>();
            }
            return function;
        }
        #endregion
        #region huntedHSObjectDataID
        private void huntedHSObjectDataID_Reset()
        {
            HuntedIDs = new List<int>();
        }
        private void huntedHSObjectDataID_Add(int id)
        {
            if (HuntedIDs == null) huntedHSObjectDataID_Reset();
            if (!HuntedIDs.Contains(id))
                HuntedIDs.Add(id);
        }
        #endregion
        #region HSObjectData
        public int getNodes_Count()
        {
            return nodes == null ? -1 : nodes.Length;
        }
        public HSObjectData getNode(int nodeID)
        {
            int index = getNodeIndex(nodeID);
            if (index < 0)
            {
                return null;
            }
            return nodes[index];
        }
        public int getNodeIndex(int nodeID)
        {
            if (nodes != null)
            {
                for (int i = 0; i < nodes.Length; i++)
                {
                    if (nodes[i].id == nodeID) return i;
                }
            }
            return -1;
        }
        public void AutoID()
        {
            if (nodes == null || nodes.Length < 1) return;
            var hsnodesUI = FindObjectsOfType<HSObjectNode>();
            if (hsnodesUI == null || hsnodesUI.Length < 1) return;
            for (int i = 0; i < nodes.Length; i++)
            {
                for (int j = 0; j < hsnodesUI.Length; j++)
                {
                    if (nodes[i].ID == hsnodesUI[j].referenceObjectId)
                    {
                        hsnodesUI[j].referenceObjectId = i;
                        break;
                    }
                    else continue;
                }
                nodes[i].id = i;
            }
        }
        #endregion
        #region hint
        public void hint_Init()
        {
            if (hintBtn != null)
            {
                hintBtn.onClick.AddListener(onHintClick);
            }
        }
        public void onHintClick()
        {
            invokeEvent(hintEvent, hintDelay);
            setHitImagePosition();
        }
        private void hintImage_setActive(bool value, bool force = false)
        {
            if (hintImage == null) return;
            if (!force && lastHintImageActive == value) return;
            hintImage.SetActive(lastHintImageActive = value);
        }
        private void setHitImagePosition()
        {
            if (hintShow || _hoNodes == null || hintImage == null) return;
            int index = -1;
            for (int i = 0; i < _hoNodes.Count; i++)
            {
                if (_hoNodes[i].gameObject.activeSelf)
                {
                    index = i;
                    break;
                }
            }
            if (index < 0) return;
            var items = FindObjectsOfType<HSObjectNode>();
            if (items != null)
            {
                foreach (var item in items)
                {
                    if (item.referenceObjectId == _hoNodes[index].ID)
                    {
                        lastHintImageTime = Time.time;
                        hintImage_setActive(hintShow = true);
                        if (mode == Mode.TwoDimensional) { hintImage.transform.position = item.transform.position; }
                        else if (mode == Mode.ThreeDimensional)
                        {
                            if (hintOnObject)
                            {
                                hintImage.transform.position = item.transform.position;
                            }
                            else
                            {
                                var dir = (MainCamera.transform.position - item.transform.position).normalized;
                                hintImage.transform.position = MainCamera.transform.position + dir * -hintCameraDistance;
                            }
                            if (_hintImage_RectTransform != null)
                            {
                                _hintImage_RectTransform.rotation = Quaternion.LookRotation(MainCamera.transform.position, item.transform.position);
                            }
                        }
                        return;
                    }
                }
            }
        }
        private void checkHintImageActive()
        {
            if (!hintShow || hintImage == null) return;
            float t = Time.time;
            if (t > lastHintImageTime + hintImageShowSecond)
            {
                hintImage_setActive(hintShow = false);
            }
        }
        #endregion
        #region pixel hunting
        private bool allowPixelHunting()
        {
            if (!pixelHunting) return true;
            float t = Time.time;
            if (t > lastPixelHuntingTime + pixelHuntingThresholdDelay)
            {
                lastPixelHuntingTime = t;
                pixelHuntImage_setActive(false);
                return true;
            }
            invokeEvent(pixelHuntEvent);
            setPixelHuntImagePosition();
            pixelHuntShow = true;
            pixelHuntImage_setActive(true);
            return false;
        }
        private void pixelHuntImage_setActive(bool value, bool force = false)
        {
            if (pixelHuntImage == null) return;
            if (!force && lastPixelHuntingImageActive == value) return;
            pixelHuntImage.SetActive(lastPixelHuntingImageActive = value);
        }
        private void setPixelHuntImagePosition()
        {
            if (pixelHuntImage == null) return;
            if (mode == Mode.TwoDimensional)
            {
                pixelHuntImage.transform.position = getMousePosition2D();
            }
            else if (mode == Mode.ThreeDimensional)
            {
                RaycastHit hit;
                Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    pixelHuntImage.transform.position = (hit.point - ray.direction.normalized);
                    if (_pixelHuntImage_RectTransform != null)
                    {
                        _pixelHuntImage_RectTransform.rotation = Quaternion.LookRotation(MainCamera.transform.position, (hit.point + ray.direction));
                    }
                }
            }
        }
        private void checkPixelHuntActive()
        {
            if (!pixelHuntShow || pixelHuntImage == null) return;
            float t = Time.time;
            if (t > lastPixelHuntingTime + pixelHuntingThresholdDelay)
            {
                pixelHuntImage_setActive(pixelHuntShow = false);
            }
        }
        #endregion
        #region events - delay
        public void invokeEvent(UnityEvent e, float delay = 0)
        {
            if (e == null) return;
            if (delay > 0)
            {
                StartCoroutine(Coroutine(delay, e));
            }
            else
            {
                e.Invoke();
            }
        }
        private IEnumerator Coroutine(float seconds, UnityEvent e)
        {
            yield return new WaitForSeconds(seconds);
            if (e != null) e.Invoke();
        }
        #endregion
        #region physics - HSObjectNode
        public HSObjectNode isHit()
        {
            if (mode == Mode.ThreeDimensional)
            {
                return isHit_3D();
            }
            else if (mode == Mode.TwoDimensional)
            {
                return isHit_2D();
            }
            return null;
        }
        public HSObjectNode isHit_2D()
        {
            RaycastHit2D hit = Physics2D.Raycast(getMousePosition2D(),
                             MainCamera.ScreenPointToRay(Input.mousePosition).direction,
                             Mathf.Infinity);
            if (hit.collider != null)
            {
                return hit.collider.gameObject.GetComponent<HSObjectNode>();
            }
            return null;
        }
        public HSObjectNode isHit_3D()
        {
            RaycastHit hit;
            Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider != null)
                {
                    return hit.collider.gameObject.GetComponent<HSObjectNode>();
                }
            }
            return null;
        }
        private Vector2 getMousePosition2D()
        {
            return MainCamera.ScreenToWorldPoint(Input.mousePosition);
        }
        #endregion
        #region logic
        private bool checkGameIsOver()
        {
            int count = getNodes_Count();
            if (count < 1) { return _gameIsOver = true; }
            if (HuntedIDs != null)
            {
                for (int i = 0; i < nodes.Length; i++)
                {
                    if (!HuntedIDs.Contains(nodes[i].ID))
                    {
                        return _gameIsOver = false;
                    }
                }
                return _gameIsOver = true;
            }
            return _gameIsOver = false;
        }
        #endregion
        #endregion
    }
}