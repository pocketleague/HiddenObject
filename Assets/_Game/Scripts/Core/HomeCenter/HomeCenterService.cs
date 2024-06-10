using UnityEngine;
using Zenject;
using Scripts.Stages;
using System;
using Scripts.Core.StateManager;

namespace Scripts.Core.HomeCenter
{
    public class HomeCenterService : IHomeCenterService, IState
    {
        private HomeCenterConfig _config;

        public event Action OnHomeCenterStarted   = delegate { };
        public event Action OnHomeCenterEnded     = delegate { };

        [Inject]
        private void Construct(HomeCenterConfig config, IStagesService stagesService)
        {
            _config = config;
        }

        public void Begin()
        {
            OnHomeCenterStarted.Invoke();
        }

        public void End()
        {
            OnHomeCenterEnded.Invoke();
        }
    }
}
