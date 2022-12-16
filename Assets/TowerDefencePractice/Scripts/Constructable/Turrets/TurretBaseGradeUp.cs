using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TowerDefencePractice.Constructable.Turrets
{
    public abstract class TurretBaseGradeUp : MonoBehaviour
    {
        // TurretBehaviourの取得
        protected TurretBaseBehaviour turretBehaviour;



        void Start()
        {
            turretBehaviour = GetComponent<TurretBaseBehaviour>();
        }


        private void Update()
        {
            if (Keyboard.current.digit1Key.wasPressedThisFrame)
            {
                FireRateGradeUp();
            }
        }

        // ------------------------------------------------------------------------
        // 発射間隔
        // ------------------------------------------------------------------------

        /// <summary>
        /// 発射間隔のレベルアップ
        /// </summary>
        protected void FireRateGradeUp()
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
            turretBehaviour.fireRateCurrent = FireRateCalculate();

            // ********************コメントアウト予定***************************
            // ---------------------------------------------------------------------------------------------
            Debug.Log($"タレットID[{ turretBehaviour.turretID }]の射撃間隔をレベルアップしました。\n" +
                $"現在の発射間隔レベルは{ turretBehaviour.fireRateCurrentLevel }、射撃間隔は{ turretBehaviour.fireRateCurrent }[/s]です。");
            // ---------------------------------------------------------------------------------------------
        }

        /// <summary>
        /// 発射間隔の計算
        /// </summary>
        protected virtual float FireRateCalculate()
        {
            Debug.Log("デフォルトの計算方法です。");

            // 線形の計算方法
            return turretBehaviour.turretData.fireRateBase -
                (turretBehaviour.turretData.fireRateBase - turretBehaviour.turretData.fireRateMax) * (turretBehaviour.fireRateCurrentLevel / turretBehaviour.turretData.fireRateMaxLevel);
        }



        // ------------------------------------------------------------------------
        // 発射火力
        // ------------------------------------------------------------------------

        /// <summary>
        /// 発射火力のレベルアップ
        /// </summary>
        protected virtual void FirePowerGradeUp()
        {

        }



        // ------------------------------------------------------------------------
        // 攻撃範囲
        // ------------------------------------------------------------------------

        /// <summary>
        /// 攻撃範囲のレベルアップ
        /// </summary>
        protected virtual void FireRangeGradeUp()
        {

        }
    }
}