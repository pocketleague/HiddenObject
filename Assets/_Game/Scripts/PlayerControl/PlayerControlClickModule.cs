using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Scripts.PlayerControl
{
    public class PlayerControlClickModule
    {
        public event Action<Vector3> OnMouseDown = delegate { };


        private readonly PlayerControlConfig _config;

        public PlayerControlClickModule(PlayerControlConfig config)
        {
            _config = config;
        }

        public void Tick()
        {
            if (Input.GetMouseButtonDown(0)) // Check for left mouse button click
            {
                OnMouseDown.Invoke(Input.mousePosition);
            }
        }

      
    }
}