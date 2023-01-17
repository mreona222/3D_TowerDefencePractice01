using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TowerDefencePractice.Bullets;
using TowerDefencePractice.Damages;

namespace TowerDefencePractice.Constructable.Turrets
{
    public class TurretBehavoiurNormal : TurretBehaviourBase
    {
        protected override void Start()
        {
            base.Start();
        }

        protected override void Update()
        {
            base.Update();
        }


        // ---------------------------------------------------------------------------------
        // 発射関係
        // ---------------------------------------------------------------------------------

        /// <summary>
        /// 発射
        /// </summary>
        public override void Fire(Collider target)
        {
            // 射撃アニメーション
            turretAnimator.SetTrigger("Shoot");

            // 弾の生成
            Transform fPTransform = firePointTransform[nextPoint % firePointTransform.Length];
            nextPoint++;
            GetComponent<TurretFireAnimation>().firePointTransform = fPTransform;

            GameObject bulletInstance = Instantiate(bullet, fPTransform.position, fPTransform.rotation * bullet.transform.rotation);
            bulletInstance.transform.localScale = Vector3.Scale(bulletInstance.transform.localScale, transform.parent.localScale);
            bulletInstance.GetComponent<BulletBehaviourBase>().parentTurret = transform;
            bulletInstance.GetComponent<BulletBehaviourBase>().target = target;

            // ダメージを与える
            target.GetComponent<IDamageApplicable>().DamageApplicate(firePowerCurrent, stanTime);

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

    }
}