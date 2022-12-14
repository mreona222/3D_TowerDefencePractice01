using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace TowerDefencePractice.Turrets
{
    public enum Turret
    {
        Turret01 = 0,
        Turret02,
        Turret03,
        Turret04,
    }

    public abstract class TurretBaseBehaviour : MonoBehaviour
    {
        // �^���b�g��ID
        public System.Guid turretID;

        // �^���b�g�̏��
        public TurretData turretData;


        // ���݂̎ˌ��Ԋu���x��
        public float fireRateCurrentLevel;
        // ���݂̎ˌ��Ԋu[s/times]
        public float fireRateCurrent;
        // �ˌ��\
        protected bool canShoot;


        // �^���b�g�̃A�j���[�^�[
        protected Animator turretAnimator;
        // �ˌ��A�j���[�V�����̃N���b�v
        [SerializeField]
        AnimationClip turretFireAnimationClip;


        protected virtual void Start()
        {
            turretID = System.Guid.NewGuid();

            turretAnimator = GetComponent<Animator>();


            fireRateCurrentLevel = 1.0f;
            fireRateCurrent = turretData.fireRateBase;
            canShoot = true;
        }

        protected abstract void Fire();
    }
}