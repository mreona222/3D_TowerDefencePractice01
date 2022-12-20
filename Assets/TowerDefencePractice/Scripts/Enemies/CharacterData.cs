using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefencePractice.Character
{
    [CreateAssetMenu(menuName = "My Scriptable/Create CharacterData")]

    public class CharacterData : ScriptableObject
    {
        // ------------------------------------------------------
        // 基本情報
        // ------------------------------------------------------

        // キャラクターの名前
        public string characterName;
        // キャラクターのプレハブ
        public GameObject characterPrefab;
        // キャラクターのタイプ
        public enum CharacterType
        {
            Enemies,

        }
        public CharacterType characterType;


        // ------------------------------------------------------
        // 生体情報
        // ------------------------------------------------------

        // キャラクターのMaxLevel
        public float characterLevelMax;

        // キャラクターの基本HP
        public float characterHPBase;
        // キャラクターのMaxHP
        public float characterHPMax;
        // キャラクターのHPMaxLevel
        public float characterHPMaxLevel;

        // キャラクターの基本移動速度
        public float characterSpeedBase;
        // キャラクターの最高速度
        public float characterSpeedMax;
        // キャラクターのHPMaxLevel
        public float characterSpeedMaxLevel;

        // キャラクターの落とす金額
        public float characterMoneyBase;

    }
}