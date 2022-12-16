using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefencePractice.Constructable.Turrets
{
    public enum Turret
    {
        Turret01 = 0,
        Turret02,
        Turret03,
        Turret04,
    }

    public abstract class TurretBaseBehaviour : MonoBehaviour, IConstructable
    {
        // �^���b�g��ID
        public System.Guid turretID;

        // �^���b�g�̏��
        public ConstructableData turretData;


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

        GameObject IConstructable.InstantiateConstructable(Transform gridCell)
        {
            return Instantiate(turretData.constructablePrefab, gridCell.position + new Vector3(0, gridCell.localScale.y / 2, 0), gridCell.rotation, gridCell);
        }

        protected abstract void Fire();
    }
}