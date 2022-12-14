using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefencePractice.Constructable.Turrets
{
    public class Turret01Behavoiur : TurretBaseBehaviour
    {
        protected override void Start()
        {
            base.Start();
        }

        private void Update()
        {
            if (canShoot)
            {
                Fire();
            }
        }

        // ---------------------------------------------------------------------------------
        // 発射関係
        // ---------------------------------------------------------------------------------

        /// <summary>
        /// 発射
        /// </summary>
        protected override void Fire()
        {
            // 射撃アニメーション
            turretAnimator.SetTrigger("Shoot");

            // 弾の生成


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
        // ターゲット
        // ---------------------------------------------------------------------------------

        protected void ChangeTarget()
        {

        }
    }
}