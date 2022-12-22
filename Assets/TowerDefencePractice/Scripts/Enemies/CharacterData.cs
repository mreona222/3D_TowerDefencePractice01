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
        // キャラクターのHPの上昇比率
        public float characterHPRatio;
        // キャラクターのHPの累乗
        public float characterHPPow;

        // キャラクターの基本移動速度
        public float characterSpeedBase;
        // キャラクターの最高速度
        public float characterSpeedMax;
        // キャラクターのHPMaxLevel
        public float characterSpeedMaxLevel;
        // kキャラクターの速度の累乗
        public float characterSpeedPow;

        // キャラクターの落とす基本金額
        public float characterCoinBase;
        // キャラクターの金額の上昇比率
        public float characterCoinRatio;
        // キャラクターの金額の累乗
        public float characterCoinPow;
    }
}