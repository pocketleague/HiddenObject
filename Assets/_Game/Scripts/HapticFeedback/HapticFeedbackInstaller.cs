using UnityEngine;
using Zenject;

namespace Scripts.HapticFeedback
{
    [CreateAssetMenu( menuName = ( "Installers/HapticFeedbackInstaller" ), fileName = "HapticFeedbackInstaller" )]
    public class HapticFeedbackInstaller : ScriptableObjectInstaller
    {
        public HapticFeedbackConfig config;

        public override void InstallBindings()
        {
            Container.BindInstance( config );
            Container.Bind<IHapticFeedbackService>().To<HapticFeedbackService>().AsSingle();
        }
    }
}