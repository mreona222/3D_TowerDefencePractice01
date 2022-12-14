using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefencePractice.Constructable
{
    [CreateAssetMenu(menuName = "My Scriptable/Create ConstructableBoss")]
    public class ConstructableBoss : ScriptableObject
    {
        // 建造可能物のScriptableObject情報
        public ConstructableData[] constructableSctiptbaleObject;
    }
}