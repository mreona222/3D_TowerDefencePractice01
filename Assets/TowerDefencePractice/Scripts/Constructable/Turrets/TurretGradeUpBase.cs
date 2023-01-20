using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TowerDefencePractice.Constructable.Turrets
{
    public abstract class TurretGradeUpBase : MonoBehaviour
    {
        // TurretBehaviourの取得
        protected TurretBehaviourBase turretBehaviour;

        private void Start()
        {
            turretBehaviour = GetComponent<TurretBehaviourBase>();
        }

        public void Initialize()
        {
            turretBehaviour = GetComponent<TurretBehaviourBase>();

            // 発射間隔のレベル
            turretBehaviour.fireRateCurrentLevel = 0;
            // 発射間隔の計算
            turretBehaviour.fireRateCurrent = FireRateCalculate(turretBehaviour.fireRateCurrentLevel);
            // 発射間隔アップグレードのコスト
            turretBehaviour.fireRateUpgradeCoin = FireRateUpgradeCoinCalcurate(turretBehaviour.fireRateCurrentLevel);

            // 射撃火力のレベル
            turretBehaviour.firePowerCurrentLevel = 0;
            // 射撃火力の計算
            turretBehaviour.firePowerCurrent = FirePowerCalculate(turretBehaviour.firePowerCurrentLevel);
            // 射撃火力アップグレードコスト
            turretBehaviour.firePowerUpgradeCoin = FirePowerUpgradeCoinCalcurate(turretBehaviour.firePowerCurrentLevel);

            // 射撃範囲のレベル
            turretBehaviour.fireRangeCurrentLevel = 0;
            // 射撃範囲の計算
            turretBehaviour.fireRangeCurrent = FireRangeCalculate(turretBehaviour.fireRangeCurrentLevel);
            // 射撃範囲アップグレードコスト
            turretBehaviour.fireRangeUpgradeCoin = FireRangeUpgradeCoinCalcurate(turretBehaviour.fireRangeCurrentLevel);
        }



        // ------------------------------------------------------------------------
        // 発射間隔
        // ------------------------------------------------------------------------

        /// <summary>
        /// 発射間隔のレベルアップ
        /// </summary>
        public void FireRateGradeUp()
        {
            // レベルマックスのとき
            if (turretBehaviour.turretData.fireRateMaxLevel - 1 < turretBehaviour.fireRateCurrentLevel)
            {
                Debug.LogWarning("レベルマックス状態でレベルアップ関数が呼ばれました。");
                return;
            }

            // 発射間隔のレベルアップ
            turretBehaviour.fireRateCurrentLevel++;
            // 発射間隔の計算
            turretBehaviour.fireRateCurrent = FireRateCalculate(turretBehaviour.fireRateCurrentLevel);
            // 発射間隔アップグレードのコスト計算
            turretBehaviour.fireRateUpgradeCoin = FireRateUpgradeCoinCalcurate(turretBehaviour.fireRateCurrentLevel);

            // ********************コメントアウト予定***************************
            // ---------------------------------------------------------------------------------------------
            //Debug.Log($"タレットID[{ turretBehaviour.turretID }]の射撃間隔をレベルアップしました。\n" +
            //    $"現在の発射間隔レベルは{ turretBehaviour.fireRateCurrentLevel }、射撃間隔は{ turretBehaviour.fireRateCurrent }[/s]です。");
            // ---------------------------------------------------------------------------------------------
        }

        /// <summary>
        /// 発射間隔の計算
        /// </summary>
        public virtual float FireRateCalculate(float level)
        {
            //Debug.Log("デフォルトの計算方法です。");

            // 線形 + 非線形
            return turretBehaviour.turretData.fireRateBase - (
                turretBehaviour.turretData.fireRateRatio * (turretBehaviour.turretData.fireRateBase - turretBehaviour.turretData.fireRateMax) * (level / turretBehaviour.turretData.fireRateMaxLevel) +
                (1 - turretBehaviour.turretData.fireRateRatio) * (turretBehaviour.turretData.fireRateBase - turretBehaviour.turretData.fireRateMax) *
                    Mathf.Pow((level / turretBehaviour.turretData.fireRateMaxLevel), turretBehaviour.turretData.fireRatePow)
                );
        }

        /// <summary>
        /// 射撃間隔アップグレードコストの計算
        /// </summary>
        public virtual float FireRateUpgradeCoinCalcurate(float level)
        {
            //Debug.Log("デフォルトの計算方法です。");

            return turretBehaviour.turretData.fireRateUpgradeCoinBase * Mathf.Pow(1 + level * turretBehaviour.turretData.fireRateUpgradeCoinRatio, turretBehaviour.turretData.fireRateUpgradeCoinPow);
        }



        // ------------------------------------------------------------------------
        // 発射火力
        // ------------------------------------------------------------------------

        /// <summary>
        /// 発射火力のレベルアップ
        /// </summary>
        public void FirePowerGradeUp()
        {
            // レベルマックスのとき
            if (turretBehaviour.turretData.fireRangeMaxLevel - 1 < turretBehaviour.fireRangeCurrentLevel)
            {
                Debug.LogWarning("レベルマックス状態でレベルアップ関数が呼ばれました。");
                return;
            }

            // 発射火力のレベルアップ
            turretBehaviour.firePowerCurrentLevel++;
            // 発射火力の計算
            turretBehaviour.firePowerCurrent = FirePowerCalculate(turretBehaviour.firePowerCurrentLevel);
            // 射撃火力アップグレードコスト
            turretBehaviour.firePowerUpgradeCoin = FirePowerUpgradeCoinCalcurate(turretBehaviour.firePowerCurrentLevel);

            // ********************コメントアウト予定***************************
            // ---------------------------------------------------------------------------------------------
            //Debug.Log($"タレットID[{ turretBehaviour.turretID }]の射撃火力をレベルアップしました。\n" +
            //    $"現在の発射火力レベルは{ turretBehaviour.firePowerCurrentLevel }、射撃火力は{ turretBehaviour.firePowerCurrent }[dmg]です。");
            // ---------------------------------------------------------------------------------------------
        }

        public virtual float FirePowerCalculate(float level)
        {
            //Debug.Log("デフォルトの計算方法です。");

            // 非線形の計算方法
            return turretBehaviour.turretData.firePowerBase * Mathf.Pow(1 + level * turretBehaviour.turretData.firePowerRatio, turretBehaviour.turretData.firePowerPow);
        }

        /// <summary>
        /// 射撃火力アップグレードコストの計算
        /// </summary>
        public virtual float FirePowerUpgradeCoinCalcurate(float level)
        {
            //Debug.Log("デフォルトの計算方法です。");

            return turretBehaviour.turretData.firePowerUpgradeCoinBase * Mathf.Pow(1 + level * turretBehaviour.turretData.firePowerUpgradeCoinRatio, turretBehaviour.turretData.firePowerUpgradeCoinPow);
        }



        // ------------------------------------------------------------------------
        // 射撃範囲
        // ------------------------------------------------------------------------

        /// <summary>
        /// 射撃範囲のレベルアップ
        /// </summary>
        public void FireRangeGradeUp()
        {
            // 射撃範囲のレベルアップ
            turretBehaviour.fireRangeCurrentLevel++;
            // 射撃範囲の計算
            turretBehaviour.fireRangeCurrent = FireRangeCalculate(turretBehaviour.fireRangeCurrentLevel);
            // 射撃範囲の変更
            turretBehaviour.transform.GetComponentInChildren<TurretRangeColliderBase>().RangeChange(turretBehaviour.fireRangeCurrent);
            // 射撃範囲アップグレードコスト
            turretBehaviour.fireRangeUpgradeCoin = FireRangeUpgradeCoinCalcurate(turretBehaviour.fireRangeCurrentLevel);

            // ********************コメントアウト予定***************************
            // ---------------------------------------------------------------------------------------------
            //Debug.Log($"タレットID[{ turretBehaviour.turretID }]の射撃火力をレベルアップしました。\n" +
            //    $"現在の発射火力レベルは{ turretBehaviour.firePowerCurrentLevel }、射撃火力は{ turretBehaviour.firePowerCurrent }[dmg]です。");
            // ---------------------------------------------------------------------------------------------

        }

        public float FireRangeCalculate(float level)
        {
            return turretBehaviour.turretData.fireRangeBase +
                turretBehaviour.turretData.fireRangeRatio * (turretBehaviour.turretData.fireRangeMax - turretBehaviour.turretData.fireRangeBase) * level / turretBehaviour.turretData.fireRangeMaxLevel +
                (1 - turretBehaviour.turretData.fireRangeRatio) *
                (turretBehaviour.turretData.fireRangeMax - turretBehaviour.turretData.fireRangeBase) * Mathf.Pow(level / turretBehaviour.turretData.fireRangeMaxLevel, turretBehaviour.turretData.fireRangePow);
        }


        /// <summary>
        /// 射撃間隔アップグレードコストの計算
        /// </summary>
        public virtual float FireRangeUpgradeCoinCalcurate(float level)
        {
            //Debug.Log("デフォルトの計算方法です。");

            return turretBehaviour.turretData.fireRangeUpgradeCoinBase * Mathf.Pow(1 + level * turretBehaviour.turretData.fireRangeUpgradeCoinRatio, turretBehaviour.turretData.fireRangeUpgradeCoinPow);
        }
    }
}