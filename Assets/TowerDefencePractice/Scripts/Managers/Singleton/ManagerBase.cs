using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Utilities;

namespace TowerDefencePractice.Managers
{
    public class ManagerBase<T> : SingletonMonoBehaviour<T> where T : MonoBehaviour
    {
        protected override void Init()
        {
            base.Init();
            DontDestroyOnLoad(this.gameObject);
        }
    }
}