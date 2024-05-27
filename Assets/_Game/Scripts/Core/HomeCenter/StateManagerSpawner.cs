using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Core
{
    public class StateManagerSpawner : MonoBehaviour
    {
        private IStateManagerService _stateService;

        public void Initialize(IStateManagerService stateService)
        {
            _stateService = stateService;
        }

        private void Start()
        {
            _stateService.SpawnStateCenter();
        }
    }
}
