using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Camera;
using UnityEngine.EventSystems;

namespace Video
{
    public class Opener : MonoBehaviour
    {
        public GameManager gameManager;

        public CameraStateConfig camStateConfig;

        public Animator _animator;

        public bool IsOpen;

        public ItemView itemView;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void Toggle()
        {
            if (!IsOpen)
            {
                _animator.SetBool("open", true);
                IsOpen = true;

                gameManager.ChangeCam(camStateConfig);

                if(itemView)
                    itemView.HandleClickable(true);
            }
            else
            {
                _animator.SetBool("open", false);
                IsOpen = false;

                gameManager.ChangeCam();

                if(itemView)
                    itemView.HandleClickable(false);
            }
        }
    }
}
