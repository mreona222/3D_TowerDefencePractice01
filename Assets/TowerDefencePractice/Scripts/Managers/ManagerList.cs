using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefencePractice.Managers
{
    [CreateAssetMenu(menuName = "My Scriptable/Create ManagerList")]
    public class ManagerList : ScriptableObject
    {
        public GameObject[] managerList;
    }
}