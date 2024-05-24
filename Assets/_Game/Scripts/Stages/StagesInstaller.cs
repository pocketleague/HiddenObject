using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Scripts.Stages
{
    [CreateAssetMenu(menuName = ("Installers/Stages Installer"), fileName = "Stages Installer")]
    public class StagesInstaller : ScriptableObjectInstaller
    {
        public string currentSetId = "Control";

        public List<StagesSet> stagesSets;

        public override void InstallBindings()
        {
            Container.Bind<IStagesService>().To<StagesService>().AsSingle();
            
            if (stagesSets.Count == 0)
                return;

            foreach(var stage in stagesSets.Where(t => t.setId == currentSetId))
            {
                Container.BindInstance(stage);
                return;
            }

            Container.BindInstance(stagesSets[0].stages);
        }
    }
}