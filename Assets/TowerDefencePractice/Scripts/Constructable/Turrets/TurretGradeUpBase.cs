using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TowerDefencePractice.Constructable.Turrets
{
    public abstract class TurretGradeUpBase : MonoBehaviour
    {
        // TurretBehaviour�̎擾
        protected TurretBehaviourBase turretBehaviour;

        private void Start()
        {
            turretBehaviour = GetComponent<TurretBehaviourBase>();
        }

        public void Initialize()
        {
            turretBehaviour = GetComponent<TurretBehaviourBase>();

            // ���ˊԊu�̃��x��
            turretBehaviour.fireRateCurrentLevel = 0;
            // ���ˊԊu�̌v�Z
            turretBehaviour.fireRateCurrent = FireRateCalculate(turretBehaviour.fireRateCurrentLevel);
            // ���ˊԊu�A�b�v�O���[�h�̃R�X�g
            turretBehaviour.fireRateUpgradeCoin = FireRateUpgradeCoinCalcurate(turretBehaviour.fireRateCurrentLevel);

            // �ˌ��Η͂̃��x��
            turretBehaviour.firePowerCurrentLevel = 0;
            // �ˌ��Η͂̌v�Z
            turretBehaviour.firePowerCurrent = FirePowerCalculate(turretBehaviour.firePowerCurrentLevel);
            // �ˌ��ἨA�b�v�O���[�h�R�X�g
            turretBehaviour.firePowerUpgradeCoin = FirePowerUpgradeCoinCalcurate(turretBehaviour.firePowerCurrentLevel);

            // �ˌ��͈͂̃��x��
            turretBehaviour.fireRangeCurrentLevel = 0;
            // �ˌ��͈͂̌v�Z
            turretBehaviour.fireRangeCurrent = FireRangeCalculate(turretBehaviour.fireRangeCurrentLevel);
            // �ˌ��͈̓A�b�v�O���[�h�R�X�g
            turretBehaviour.fireRangeUpgradeCoin = FireRangeUpgradeCoinCalcurate(turretBehaviour.fireRangeCurrentLevel);
        }



        // ------------------------------------------------------------------------
        // ���ˊԊu
        // ------------------------------------------------------------------------

        /// <summary>
        /// ���ˊԊu�̃��x���A�b�v
        /// </summary>
        public void FireRateGradeUp()
        {
            // ���x���}�b�N�X�̂Ƃ�
            if (turretBehaviour.turretData.fireRateMaxLevel - 1 < turretBehaviour.fireRateCurrentLevel)
            {
                Debug.LogWarning("���x���}�b�N�X��ԂŃ��x���A�b�v�֐����Ă΂�܂����B");
                return;
            }

            // ���ˊԊu�̃��x���A�b�v
            turretBehaviour.fireRateCurrentLevel++;
            // ���ˊԊu�̌v�Z
            turretBehaviour.fireRateCurrent = FireRateCalculate(turretBehaviour.fireRateCurrentLevel);
            // ���ˊԊu�A�b�v�O���[�h�̃R�X�g�v�Z
            turretBehaviour.fireRateUpgradeCoin = FireRateUpgradeCoinCalcurate(turretBehaviour.fireRateCurrentLevel);

            // ********************�R�����g�A�E�g�\��***************************
            // ---------------------------------------------------------------------------------------------
            //Debug.Log($"�^���b�gID[{ turretBehaviour.turretID }]�̎ˌ��Ԋu�����x���A�b�v���܂����B\n" +
            //    $"���݂̔��ˊԊu���x����{ turretBehaviour.fireRateCurrentLevel }�A�ˌ��Ԋu��{ turretBehaviour.fireRateCurrent }[/s]�ł��B");
            // ---------------------------------------------------------------------------------------------
        }

        /// <summary>
        /// ���ˊԊu�̌v�Z
        /// </summary>
        public virtual float FireRateCalculate(float level)
        {
            //Debug.Log("�f�t�H���g�̌v�Z���@�ł��B");

            // ���` + ����`
            return turretBehaviour.turretData.fireRateBase - (
                turretBehaviour.turretData.fireRateRatio * (turretBehaviour.turretData.fireRateBase - turretBehaviour.turretData.fireRateMax) * (level / turretBehaviour.turretData.fireRateMaxLevel) +
                (1 - turretBehaviour.turretData.fireRateRatio) * (turretBehaviour.turretData.fireRateBase - turretBehaviour.turretData.fireRateMax) *
                    Mathf.Pow((level / turretBehaviour.turretData.fireRateMaxLevel), turretBehaviour.turretData.fireRatePow)
                );
        }

        /// <summary>
        /// �ˌ��Ԋu�A�b�v�O���[�h�R�X�g�̌v�Z
        /// </summary>
        public virtual float FireRateUpgradeCoinCalcurate(float level)
        {
            //Debug.Log("�f�t�H���g�̌v�Z���@�ł��B");

            return turretBehaviour.turretData.fireRateUpgradeCoinBase * Mathf.Pow(1 + level * turretBehaviour.turretData.fireRateUpgradeCoinRatio, turretBehaviour.turretData.fireRateUpgradeCoinPow);
        }



        // ------------------------------------------------------------------------
        // ���ˉΗ�
        // ------------------------------------------------------------------------

        /// <summary>
        /// ���ˉΗ͂̃��x���A�b�v
        /// </summary>
        public void FirePowerGradeUp()
        {
            // ���x���}�b�N�X�̂Ƃ�
            if (turretBehaviour.turretData.fireRangeMaxLevel - 1 < turretBehaviour.fireRangeCurrentLevel)
            {
                Debug.LogWarning("���x���}�b�N�X��ԂŃ��x���A�b�v�֐����Ă΂�܂����B");
                return;
            }

            // ���ˉΗ͂̃��x���A�b�v
            turretBehaviour.firePowerCurrentLevel++;
            // ���ˉΗ͂̌v�Z
            turretBehaviour.firePowerCurrent = FirePowerCalculate(turretBehaviour.firePowerCurrentLevel);
            // �ˌ��ἨA�b�v�O���[�h�R�X�g
            turretBehaviour.firePowerUpgradeCoin = FirePowerUpgradeCoinCalcurate(turretBehaviour.firePowerCurrentLevel);

            // ********************�R�����g�A�E�g�\��***************************
            // ---------------------------------------------------------------------------------------------
            //Debug.Log($"�^���b�gID[{ turretBehaviour.turretID }]�̎ˌ��Η͂����x���A�b�v���܂����B\n" +
            //    $"���݂̔��ˉἨ��x����{ turretBehaviour.firePowerCurrentLevel }�A�ˌ��Η͂�{ turretBehaviour.firePowerCurrent }[dmg]�ł��B");
            // ---------------------------------------------------------------------------------------------
        }

        public virtual float FirePowerCalculate(float level)
        {
            //Debug.Log("�f�t�H���g�̌v�Z���@�ł��B");

            // ����`�̌v�Z���@
            return turretBehaviour.turretData.firePowerBase * Mathf.Pow(1 + level * turretBehaviour.turretData.firePowerRatio, turretBehaviour.turretData.firePowerPow);
        }

        /// <summary>
        /// �ˌ��ἨA�b�v�O���[�h�R�X�g�̌v�Z
        /// </summary>
        public virtual float FirePowerUpgradeCoinCalcurate(float level)
        {
            //Debug.Log("�f�t�H���g�̌v�Z���@�ł��B");

            return turretBehaviour.turretData.firePowerUpgradeCoinBase * Mathf.Pow(1 + level * turretBehaviour.turretData.firePowerUpgradeCoinRatio, turretBehaviour.turretData.firePowerUpgradeCoinPow);
        }



        // ------------------------------------------------------------------------
        // �ˌ��͈�
        // ------------------------------------------------------------------------

        /// <summary>
        /// �ˌ��͈͂̃��x���A�b�v
        /// </summary>
        public void FireRangeGradeUp()
        {
            // �ˌ��͈͂̃��x���A�b�v
            turretBehaviour.fireRangeCurrentLevel++;
            // �ˌ��͈͂̌v�Z
            turretBehaviour.fireRangeCurrent = FireRangeCalculate(turretBehaviour.fireRangeCurrentLevel);
            // �ˌ��͈͂̕ύX
            turretBehaviour.transform.GetComponentInChildren<TurretRangeColliderBase>().RangeChange(turretBehaviour.fireRangeCurrent);
            // �ˌ��͈̓A�b�v�O���[�h�R�X�g
            turretBehaviour.fireRangeUpgradeCoin = FireRangeUpgradeCoinCalcurate(turretBehaviour.fireRangeCurrentLevel);

            // ********************�R�����g�A�E�g�\��***************************
            // ---------------------------------------------------------------------------------------------
            //Debug.Log($"�^���b�gID[{ turretBehaviour.turretID }]�̎ˌ��Η͂����x���A�b�v���܂����B\n" +
            //    $"���݂̔��ˉἨ��x����{ turretBehaviour.firePowerCurrentLevel }�A�ˌ��Η͂�{ turretBehaviour.firePowerCurrent }[dmg]�ł��B");
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
        /// �ˌ��Ԋu�A�b�v�O���[�h�R�X�g�̌v�Z
        /// </summary>
        public virtual float FireRangeUpgradeCoinCalcurate(float level)
        {
            //Debug.Log("�f�t�H���g�̌v�Z���@�ł��B");

            return turretBehaviour.turretData.fireRangeUpgradeCoinBase * Mathf.Pow(1 + level * turretBehaviour.turretData.fireRangeUpgradeCoinRatio, turretBehaviour.turretData.fireRangeUpgradeCoinPow);
        }
    }
}