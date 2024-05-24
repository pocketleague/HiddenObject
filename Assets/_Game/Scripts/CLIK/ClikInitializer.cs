using UnityEngine;

namespace Scripts.CLIK
{
    [DefaultExecutionOrder(-1000)]
    public class ClikInitializer : MonoBehaviour
    {
        private void Awake()
        {
#if TTP_CORE
        Tabtale.TTPlugins.TTPCore.Setup();
#endif
        }
    }
}
