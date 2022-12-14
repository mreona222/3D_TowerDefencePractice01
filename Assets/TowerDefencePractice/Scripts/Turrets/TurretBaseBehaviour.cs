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
        // タレットのID
        public System.Guid turretID;

        // タレットの情報
        public TurretData turretData;


        // 現在の射撃間隔レベル
        public float fireRateCurrentLevel;
        // 現在の射撃間隔[s/times]
        public float fireRateCurrent;
        // 射撃可能
        protected bool canShoot;


        // タレットのアニメーター
        protected Animator turretAnimator;
        // 射撃アニメーションのクリップ
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