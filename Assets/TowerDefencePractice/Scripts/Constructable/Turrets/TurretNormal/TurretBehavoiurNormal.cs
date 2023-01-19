using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TowerDefencePractice.Bullets;
using TowerDefencePractice.Damages;

namespace TowerDefencePractice.Constructable.Turrets
{
    public class TurretBehavoiurNormal : TurretBehaviourBase
    {
        // ---------------------------------------------------------------------------------
        // ���ˊ֌W
        // ---------------------------------------------------------------------------------

        /// <summary>
        /// ����
        /// </summary>
        public override void Fire(Collider target)
        {
            // �ˌ��A�j���[�V����
            turretAnimator.SetTrigger("Shoot");
            // �ˌ���
            source.PlayOneShot(source.clip);

            // �e�̐���
            Transform fPTransform = firePointTransform[nextPoint % firePointTransform.Length];
            nextPoint++;
            GetComponent<TurretFireAnimationBase>().firePointTransform = fPTransform;

            GameObject bulletInstance = Instantiate(bullet, fPTransform.position, fPTransform.rotation * bullet.transform.rotation);
            bulletInstance.transform.localScale = Vector3.Scale(bulletInstance.transform.localScale, transform.parent.localScale);
            bulletInstance.GetComponent<BulletBehaviourBase>().parentTurret = transform;
            bulletInstance.GetComponent<BulletBehaviourBase>().target = target;

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

    }
}