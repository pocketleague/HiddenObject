using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Video
{
    public class ItemView : MonoBehaviour
    {
        public GameManager gameManager;
        public ItemConfig itemConfig;

        public bool IsClickable;

        public GameObject mesh, confetti, poof;

        public Vector3 posCache;

        private void Awake()
        {
            posCache = transform.position;
        }

        public ItemConfig OnClick(Vector3 hitPos)
        {
            //mesh.SetActive(false);
            confetti.SetActive(true);

            StartCoroutine(Move());
            return itemConfig;
        }

        public void HandleClickable(bool status)
        {
            IsClickable = status;
        }

        IEnumerator Move()
        {
            // Move Up
            LeanTween.move(gameObject, new Vector3(posCache.x, posCache.y + 5, posCache.z), 0.5f) ;
            LeanTween.rotate(mesh, new Vector3(0,0,0), 0.5f);

            yield return new WaitForSeconds(1);

            //// Move To Center
            //LeanTween.move(gameObject, gameManager.itemCenterPos, 0.5f);
            //LeanTween.move(gameObject, gameManager.itemCenterPos, 0.5f);
            //yield return new WaitForSeconds(2);

            // Move To Girl
            LeanTween.move(gameObject, gameManager.itemPosAtGirl, 0.5f);

            yield return new WaitForSeconds(1f);
            poof.SetActive(true);
            mesh.SetActive(false);

            yield return new WaitForSeconds(0.1f);
            gameManager.PlayHeartParticles();
        }
    }
}