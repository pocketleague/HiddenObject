using Scripts.Stages;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Scripts.UI
{
    public class NextStageLabelBackground : MonoBehaviour
    {
        [Inject]
        private IStagesService _stagesService;
    
        private Image _image;

        private void Awake()
        {
            _image = GetComponent<Image>();

            _stagesService.OnStageProgressChanged += OnProgressChanged;
        }

        private void OnDestroy()
        {
            _stagesService.OnStageProgressChanged -= OnProgressChanged;
        }

        private void OnProgressChanged( float progress )
        {
            _image.enabled = progress >= 1f;
        }
    }
}