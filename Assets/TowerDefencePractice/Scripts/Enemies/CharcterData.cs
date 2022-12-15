using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefencePractice.Character.Enemies
{
    [CreateAssetMenu(menuName = "My Scriptable/Create CharacterData")]

    public class CharcterData : ScriptableObject
    {
        // ------------------------------------------------------
        // Šî–{î•ñ
        // ------------------------------------------------------

        // “G‚Ì–¼‘O
        public string characterName;


        // ------------------------------------------------------
        // ¶‘Ìî•ñ
        // ------------------------------------------------------

        // “G‚ÌHP
        public float charcterHPBase;

        // ˆÚ“®‘¬“x
        public float charcterSpeedBase;


    }
}