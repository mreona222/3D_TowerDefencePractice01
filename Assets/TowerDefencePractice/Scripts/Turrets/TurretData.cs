using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefencePractice.Turrets
{
    [CreateAssetMenu(menuName = "My Scriptable/Create TurretData")]

    public class TurretData : ScriptableObject
    {
        //---------------------------------------------------------
        // 基本情報
        //---------------------------------------------------------

        // タレットの名前
        public string turretName;
        // タレットのアイコン
        public Sprite turretIcon;

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