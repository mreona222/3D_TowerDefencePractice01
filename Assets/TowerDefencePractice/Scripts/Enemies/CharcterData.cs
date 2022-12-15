using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefencePractice.Character.Enemies
{
    [CreateAssetMenu(menuName = "My Scriptable/Create CharacterData")]

    public class CharcterData : ScriptableObject
    {
        // ------------------------------------------------------
        // 基本情報
        // ------------------------------------------------------

        // 敵の名前
        public string characterName;


        // ------------------------------------------------------
        // 生体情報
        // ------------------------------------------------------

        // 敵のHP
        public float charcterHPBase;

        // 移動速度
        public float charcterSpeedBase;


    }
}