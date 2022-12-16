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
        [HideInInspector]
        public bool canShoot;


        // タレットのアニメーター
        protected Animator turretAnimator;
        // 射撃アニメーションのクリップ
        [SerializeField]
        AnimationClip turretFireAnimationClip;


        // 弾のプレハブ
        [SerializeField]
        protected GameObject bullet;
        // 弾の発射位置
        public Transform[] firePointTransform = null;
        // 次の発射位置
        protected int nextPoint = 0;


        protected virtual void Start()
        {
            turretID = System.Guid.NewGuid();

            turretAnimator = GetComponent<Animator>();

            fireRateCurrentLevel = 1.0f;
            fireRateCurrent = turretData.fireRateBase;
            canShoot = true;
        }

        protected virtual void Update()
        {

        }


        // ---------------------------------------------------------------------------------
        // タレット生成
        // ---------------------------------------------------------------------------------

        GameObject IConstructable.InstantiateConstructable(Transform gridCell)
        {
            return Instantiate(turretData.constructablePrefab, gridCell.position + new Vector3(0, gridCell.localScale.y / 2, 0), gridCell.rotation, gridCell);
        }



        // ---------------------------------------------------------------------------------
        // 発射関係
        // ---------------------------------------------------------------------------------

        /// <summary>
        /// 発射
        /// </summary>
        public void Fire()
        {
            // 射撃アニメーション
            turretAnimator.SetTrigger("Shoot");

            // 弾の生成
            Transform fPTransform = firePointTransform[nextPoint % firePointTransform.Length];
            nextPoint++;
            GetComponent<TurretFireAnimation>().firePointTransform = fPTransform;

            GameObject bulletInstance = Instantiate(bullet, fPTransform.position, fPTransform.rotation * bullet.transform.rotation);
            bulletInstance.GetComponent<Bullets.BulletBehaviourBase>().parentTurret = transform;

            // 連射禁止
            StartCoroutine(FireStroke());
        }

        IEnumerator FireStroke()
        {
            // 連射禁止
            canShoot = false;

            yield return new WaitForSeconds(fireRateCurrent);
            canShoot = true;
        }



        // ---------------------------------------------------------------------------------
        // 回転
        // ---------------------------------------------------------------------------------

        public void LookTarget(Vector3 position)
        {
            Vector3 targetPosition = new Vector3(position.x, transform.position.y, position.z);
            transform.rotation = Quaternion.LookRotation(targetPosition - transform.position, transform.up);
        }



    }
}