using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefencePractice.Constructable
{
    [CreateAssetMenu(menuName = "My Scriptable/Create ConstructableBoss")]
    public class ConstructableBoss : ScriptableObject
    {
        // �����\����ScriptableObject���
        public ConstructableData[] constructableSctiptbaleObject;
    }
}