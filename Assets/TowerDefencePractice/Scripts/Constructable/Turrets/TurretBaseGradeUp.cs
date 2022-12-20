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

        private void Start()
        {
            turretBehaviour = GetComponent<TurretBaseBehaviour>();
        }

        public void Initialize()
        {
            turretBehaviour = GetComponent<TurretBaseBehaviour>();

            // 発射間隔のレベル
            turretBehaviour.fireRateCurrentLevel = 0;
            // 発射間隔の計算
            turretBehaviour.fireRateCurrent = FireRateCalculate(turretBehaviour.fireRateCurrentLevel);
            // 次の発射間隔の計算
            turretBehaviour.fireRateNext = FireRateCalculate(turretBehaviour.fireRateCurrentLevel + 1);

        }


        private void Update()
        {
            // ***************************
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
            turretBehaviour.fireRateCurrent = FireRateCalculate(turretBehaviour.fireRateCurrentLevel);
            // 次の発射間隔の計算
            turretBehaviour.fireRateNext = FireRateCalculate(turretBehaviour.fireRateCurrentLevel + 1);

            // ********************コメントアウト予定***************************
            // ---------------------------------------------------------------------------------------------
            Debug.Log($"タレットID[{ turretBehaviour.turretID }]の射撃間隔をレベルアップしました。\n" +
                $"現在の発射間隔レベルは{ turretBehaviour.fireRateCurrentLevel }、射撃間隔は{ turretBehaviour.fireRateCurrent }[/s]です。");
            // ---------------------------------------------------------------------------------------------
        }

        /// <summary>
        /// 発射間隔の計算
        /// </summary>
        public virtual float FireRateCalculate(float level)
        {
            Debug.Log("デフォルトの計算方法です。");

            // 線形の計算方法
            return turretBehaviour.turretData.fireRateBase -
                (turretBehaviour.turretData.fireRateBase - turretBehaviour.turretData.fireRateMax) * (level / turretBehaviour.turretData.fireRateMaxLevel);
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