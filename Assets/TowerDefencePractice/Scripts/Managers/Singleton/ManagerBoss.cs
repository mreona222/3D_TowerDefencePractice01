using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TowerDefencePractice.Managers
{
    public class ManagerBoss : ManagerBase<ManagerBoss>
    {
        [SerializeField]
        static GameObject[] managerList;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void ManagerInstantiate()
        {
            ManagerList m_list = (ManagerList)Resources.Load("ScriptableObjects/Managers/Manager List");
            foreach (GameObject manager in m_list.managerList)
            {
                Instantiate(manager);
            }
        }
    }
}