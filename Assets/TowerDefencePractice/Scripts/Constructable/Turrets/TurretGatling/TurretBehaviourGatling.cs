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
            // �ˌ��A�j���[�V����
            turretAnimator.SetBool("Shoot", true);
            // �ˌ���
            if (!source.isPlaying)
            {
                source.Play();
            }

            // �e�̐���
            Transform fPTransform = firePointTransform[nextPoint % firePointTransform.Length];
            GetComponent<TurretFireAnimationBase>().firePointTransform = fPTransform;

            // �_���[�W��^����
            target.GetComponent<IDamageApplicable>().DamageApplicate(firePowerCurrent, stanTime);

            // �A�ˋ֎~
            StartCoroutine(FireStroke());
        }

        IEnumerator FireStroke()
        {
            // �A�ˋ֎~
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