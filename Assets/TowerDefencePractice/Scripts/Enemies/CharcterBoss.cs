using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefencePractice.Character
{
    [CreateAssetMenu(menuName = "My Scriptable/Create CharacterBoss")]
    public class CharcterBoss : ScriptableObject
    {
        public CharacterData[] characterScriptableObject;
    }
}