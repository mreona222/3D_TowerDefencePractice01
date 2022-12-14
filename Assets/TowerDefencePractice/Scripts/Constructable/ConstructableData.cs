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

        // 元の射撃間隔
        public float fireRateBase;
        // 射撃間隔の最大の速さ
        public float fireRateMax;
        // 射撃間隔の最大のレベル
        public float fireRateMaxLevel;

        //---------------------------------------------------------
        // 射撃火力
        //---------------------------------------------------------

        // タレットの基本火力
        public float firePowerBase;

    }
}