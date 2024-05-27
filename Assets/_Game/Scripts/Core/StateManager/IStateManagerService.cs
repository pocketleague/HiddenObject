using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Core
{
    public interface IStateManagerService
    {
        void SpawnStateCenter();
        void ChangeState(EState state);
    }
}
