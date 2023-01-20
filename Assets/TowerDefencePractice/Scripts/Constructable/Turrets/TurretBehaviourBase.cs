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


        // 弾のプレハブ
        [SerializeField]
        protected GameObject bullet;
        // 弾の発射位置
        public Transform[] firePointTransform = null;
        // 次の発射位置
        public int nextPoint = 0;

        // タレットの射撃音
        [SerializeField]
        protected AudioSource source;


        protected virtual void Start()
        {
            turretID = System.Guid.NewGuid();

            turretAnimator = GetComponent<Animator>();

            GetComponent<TurretGradeUpBase>().Initialize();

            // 射程範囲の初期化
            GetComponentInChildren<TurretRangeColliderBase>().RangeChange(fireRangeCurrent);

            canShoot = true;
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