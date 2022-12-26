using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefencePractice.Constructable
{
    [CreateAssetMenu(menuName = "My Scriptable/Create ConstructableData")]

    public class ConstructableData : ScriptableObject
    {
        //---------------------------------------------------------
        // 基本情報
        //---------------------------------------------------------

        // 建造可能物の名前
        public string constructableName;
        // 建造可能物のアイコン
        public Sprite constructableIcon;
        // 建造可能物のプレハブ
        public GameObject constructablePrefab;

        //---------------------------------------------------------
        // 射撃間隔
        //---------------------------------------------------------

        // 基本射撃間隔
        public float fireRateBase;
        // 射撃間隔の最大の速さ
        public float fireRateMax;
        // 射撃間隔の最大のレベル
        public float fireRateMaxLevel;
        // 射撃間隔の上昇比率
        public float fireRateRatio;
        // 射撃間隔の累乗
        public float fireRatePow;

        //---------------------------------------------------------
        // 射撃火力
        //---------------------------------------------------------

        // 基本火力
        public float firePowerBase;
        // 最大火力
        public float firePowerMax;
        // 最大火力レベル
        public float firePowerMaxLevel;

        //---------------------------------------------------------
        // 射程
        //---------------------------------------------------------

        // 基本射程
        public float fireRangeBase;
        // 最大射程
        public float fireRangeMax;
        // 最大射程レベル
        public float fireRangeMaxLevel;

        //---------------------------------------------------------
        // コスト
        //---------------------------------------------------------

        // 基本必要人員
        public float requireStuffBase;
        // 基本必要経費
        public float requireCoinBase;
    }
}