using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TowerDefencePractice.Constructable.Turrets
{
    public abstract class TurretBaseGradeUp : MonoBehaviour
    {
        // TurretBehaviour�̎擾
        protected TurretBaseBehaviour turretBehaviour;

        private void Start()
        {
            turretBehaviour = GetComponent<TurretBaseBehaviour>();
        }

        public void Initialize()
        {
            turretBehaviour = GetComponent<TurretBaseBehaviour>();

            // ���ˊԊu�̃��x��
            turretBehaviour.fireRateCurrentLevel = 0;
            // ���ˊԊu�̌v�Z
            turretBehaviour.fireRateCurrent = FireRateCalculate(turretBehaviour.fireRateCurrentLevel);
            // ���̔��ˊԊu�̌v�Z
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
        // ���ˊԊu
        // ------------------------------------------------------------------------

        /// <summary>
        /// ���ˊԊu�̃��x���A�b�v
        /// </summary>
        protected void FireRateGradeUp()
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
            // ���̔��ˊԊu�̌v�Z
            turretBehaviour.fireRateNext = FireRateCalculate(turretBehaviour.fireRateCurrentLevel + 1);

            // ********************�R�����g�A�E�g�\��***************************
            // ---------------------------------------------------------------------------------------------
            Debug.Log($"�^���b�gID[{ turretBehaviour.turretID }]�̎ˌ��Ԋu�����x���A�b�v���܂����B\n" +
                $"���݂̔��ˊԊu���x����{ turretBehaviour.fireRateCurrentLevel }�A�ˌ��Ԋu��{ turretBehaviour.fireRateCurrent }[/s]�ł��B");
            // ---------------------------------------------------------------------------------------------
        }

        /// <summary>
        /// ���ˊԊu�̌v�Z
        /// </summary>
        public virtual float FireRateCalculate(float level)
        {
            Debug.Log("�f�t�H���g�̌v�Z���@�ł��B");

            // ���`�̌v�Z���@
            return turretBehaviour.turretData.fireRateBase -
                (turretBehaviour.turretData.fireRateBase - turretBehaviour.turretData.fireRateMax) * (level / turretBehaviour.turretData.fireRateMaxLevel);
        }



        // ------------------------------------------------------------------------
        // ���ˉΗ�
        // ------------------------------------------------------------------------

        /// <summary>
        /// ���ˉΗ͂̃��x���A�b�v
        /// </summary>
        protected virtual void FirePowerGradeUp()
        {

        }



        // ------------------------------------------------------------------------
        // �U���͈�
        // ------------------------------------------------------------------------

        /// <summary>
        /// �U���͈͂̃��x���A�b�v
        /// </summary>
        protected virtual void FireRangeGradeUp()
        {

        }
    }
}