using UnityEngine;
using Zenject;

namespace Scripts.Translation
{
    [CreateAssetMenu( menuName = ( "Installers/TranslationInstaller" ), fileName = "TranslationInstaller" )]
    public class TranslationInstaller : ScriptableObjectInstaller
    {
        public TranslationConfig config;

        public override void InstallBindings()
        {
            Container.BindInstance( config );
            Container.Bind<ITranslationService>().To<TranslationService>().AsSingle();
        }
    }
}