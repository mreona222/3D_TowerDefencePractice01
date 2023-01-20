using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TowerDefencePractice.Damages;

namespace TowerDefencePractice.Constructable.Turrets
{
    public class TurretBehaviourGatling : TurretBehaviourBase
    {
        public override void Fire(Collider target)
        {
            // 射撃アニメーション
            turretAnimator.SetBool("Shoot", true);
            // 射撃音
            if (!source.isPlaying)
            {
                source.Play();
            }

            // 弾の生成
            Transform fPTransform = firePointTransform[nextPoint % firePointTransform.Length];
            GetComponent<TurretFireAnimationBase>().firePointTransform = fPTransform;

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


        public void StopFireAnimation()
        {
            turretAnimator.SetBool("Shoot", false);
            source.Stop();
        }
    }
}