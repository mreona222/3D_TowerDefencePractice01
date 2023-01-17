using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TowerDefencePractice.Damages;
using TowerDefencePractice.Bullets;

namespace TowerDefencePractice.Constructable.Turrets
{
    public enum Turret
    {
        Turret01 = 0,
        Turret02,
        Turret03,
        Turret04,
    }

    public abstract class TurretBehaviourBase : MonoBehaviour, IConstructable
    {
        // タレットのID
        public System.Guid turretID { get; private set; }

        // タレットの情報
        public ConstructableData turretData;


        // 現在の射撃間隔レベル
        public float fireRateCurrentLevel;
        // 現在の射撃間隔[s/times]
        public float fireRateCurrent;
        // 次の射撃間隔
        public float fireRateNext;
        // 射撃間隔アップグレードに必要なコスト
        public float fireRateUpgradeCoin;
        // 射撃可能
        [HideInInspector]
        public bool canShoot;
        // ロックオン完了
        [HideInInspector]
        public bool lockon;

        // 現在の攻撃レベル
        public float firePowerCurrentLevel;
        // 現在の攻撃力
        public float firePowerCurrent;
        // 攻撃アップグレードに必要なコスト
        public float firePowerUpgradeCoin;
        // スタン時間
        public float stanTime;

        // 現在の射程レベル
        public float fireRangeCurrentLevel;
        // 現在の射程
        public float fireRangeCurrent;
        // 射程アップグレードに必要なコスト
        public float fireRangeUpgradeCoin;


        // タレットのアニメーター
        protected Animator turretAnimator;
        // 射撃アニメーションのクリップ**********
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

            GetComponent<TurretGradeUpBase>().Initialize();

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
        //public void Fire(Collider target)
        //{
        //    // 射撃アニメーション
        //    turretAnimator.SetTrigger("Shoot");

        //    // 弾の生成
        //    Transform fPTransform = firePointTransform[nextPoint % firePointTransform.Length];
        //    nextPoint++;
        //    GetComponent<TurretFireAnimation>().firePointTransform = fPTransform;

        //    GameObject bulletInstance = Instantiate(bullet, fPTransform.position, fPTransform.rotation * bullet.transform.rotation);
        //    bulletInstance.transform.localScale = Vector3.Scale(bulletInstance.transform.localScale, transform.parent.localScale);
        //    bulletInstance.GetComponent<BulletBehaviourBase>().parentTurret = transform;
        //    bulletInstance.GetComponent<BulletBehaviourBase>().target = target;

        //    // ダメージを与える
        //    target.GetComponent<IDamageApplicable>().DamageApplicate(firePowerCurrent, stanTime);

        //    // 連射禁止
        //    StartCoroutine(FireStroke());
        //}

        //IEnumerator FireStroke()
        //{
        //    // 連射禁止
        //    canShoot = false;

        //    yield return new WaitForSeconds(fireRateCurrent);
        //    canShoot = true;
        //}

        public abstract void Fire(Collider target);

        // ---------------------------------------------------------------------------------
        // 回転
        // ---------------------------------------------------------------------------------

        public void LookTarget(Vector3 position)
        {
            Vector3 targetPosition = new Vector3(position.x, transform.position.y, position.z);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetPosition - transform.position, transform.up), 10.0f * Time.deltaTime);
            lockon = Vector3.Angle(transform.forward, targetPosition - transform.position) < 10.0f;
        }



        // ---------------------------------------------------------------------------------
        // 売却
        // ---------------------------------------------------------------------------------

        public void CellTurret()
        {
            Destroy(gameObject);
        }



    }
}