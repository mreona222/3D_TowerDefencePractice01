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
        // タレットのID
        public System.Guid turretID;

        // タレットの情報
        public ConstructableData turretData;


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

        GameObject IConstructable.InstantiateConstructable(Transform gridCell)
        {
            return Instantiate(turretData.constructablePrefab, gridCell.position + new Vector3(0, gridCell.localScale.y / 2, 0), gridCell.rotation, gridCell);
        }

        protected abstract void Fire();
    }
}