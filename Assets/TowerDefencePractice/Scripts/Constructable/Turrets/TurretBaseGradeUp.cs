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
        public void FirePowerGradeUp()
        {
            // 発射火力のレベルアップ
            turretBehaviour.firePowerCurrentLevel++;
            // 発射火力の計算
            turretBehaviour.firePowerCurrent = FirePowerCalculate(turretBehaviour.firePowerCurrentLevel);

            // ********************コメントアウト予定***************************
            // ---------------------------------------------------------------------------------------------
            Debug.Log($"タレットID[{ turretBehaviour.turretID }]の射撃火力をレベルアップしました。\n" +
                $"現在の発射火力レベルは{ turretBehaviour.firePowerCurrentLevel }、射撃火力は{ turretBehaviour.firePowerCurrent }[dmg]です。");
            // ---------------------------------------------------------------------------------------------
        }

        public virtual float FirePowerCalculate(float level)
        {
            Debug.Log("デフォルトの計算方法です。");

            // 線形の計算方法
            return turretBehaviour.turretData.firePowerBase * (1 + level * turretBehaviour.turretData.firePowerRatio);
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

            // ********************コメントアウト予定***************************
            // ---------------------------------------------------------------------------------------------
            Debug.Log($"タレットID[{ turretBehaviour.turretID }]の射撃火力をレベルアップしました。\n" +
                $"現在の発射火力レベルは{ turretBehaviour.firePowerCurrentLevel }、射撃火力は{ turretBehaviour.firePowerCurrent }[dmg]です。");
            // ---------------------------------------------------------------------------------------------

        }

        public float FireRangeCalculate(float level)
        {
            return level;
        }
    }
}