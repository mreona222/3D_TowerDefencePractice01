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
        // 建造可能物のタイプ
        public enum ConstructableType
        {
            Turret,

        }
        public ConstructableType constructableType;

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
        // 射撃間隔アップグレードの初期費用
        public float fireRateUpgradeCoinBase;
        // 射撃間隔アップグレードコストの上昇比率
        public float fireRateUpgradeCoinRatio;
        // 射撃間隔アップグレードコストの急峻さ
        public float fireRateUpgradeCoinPow;

        //---------------------------------------------------------
        // 射撃火力
        //---------------------------------------------------------

        // 基本火力
        public float firePowerBase;
        // 最大火力
        public float firePowerMax;
        // 最大火力レベル
        public float firePowerMaxLevel;
        // 射撃火力の上昇比率
        public float firePowerRatio;
        // 射撃火力の累乗
        public float firePowerPow;
        // 射撃火力アップグレードの初期費用
        public float firePowerUpgradeCoinBase;
        // 射撃火力アップグレードコストの上昇比率
        public float firePowerUpgradeCoinRatio;
        // 射撃火力アップグレードコストの急峻さ
        public float firePowerUpgradeCoinPow;

        //---------------------------------------------------------
        // 射程
        //---------------------------------------------------------

        // 基本射程
        public float fireRangeBase;
        // 最大射程
        public float fireRangeMax;
        // 射程レベル
        public float fireRangeMaxLevel;
        // 射程の上昇比率
        public float fireRangeRatio;
        // 射程の累乗
        public float fireRangePow;
        // 射程アップグレードの初期費用
        public float fireRangeUpgradeCoinBase;
        // 射程アップグレードコストの上昇比率
        public float fireRangeUpgradeCoinRatio;
        // 射撃間隔アップグレードコストの急峻さ
        public float fireRangeUpgradeCoinPow;

        //---------------------------------------------------------
        // コスト
        //---------------------------------------------------------

        // 基本必要人員
        public int requireStuffBase;
        // 基本必要経費
        public int requireCoinBase;
    }
}