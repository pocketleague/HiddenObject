using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Core.LevelSelection
{
    public class LevelSelectionService : ILevelSelectionService, IState
    {
        public event Action OnLevelSelectionStarted = delegate { };
        public event Action OnLevelSelectionEnded = delegate { };

        public void Begin()
        {
            OnLevelSelectionStarted.Invoke();
        }

        public void End()
        {
            OnLevelSelectionEnded.Invoke();
        }
    }
}