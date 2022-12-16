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
        // ���ˊ֌W
        // ---------------------------------------------------------------------------------

        /// <summary>
        /// ����
        /// </summary>
        protected override void Fire()
        {
            // �ˌ��A�j���[�V����
            turretAnimator.SetTrigger("Shoot");

            // �e�̐���
            Transform fPTransform = firePointTransform[nextPoint % firePointTransform.Length];
            nextPoint++;
            GetComponent<TurretFireAnimation>().firePointTransform = fPTransform;
            Instantiate(bullet, fPTransform.position, fPTransform.rotation * bullet.transform.rotation);

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

        // ---------------------------------------------------------------------------------
        // �^�[�Q�b�g
        // ---------------------------------------------------------------------------------

        protected void ChangeTarget()
        {

        }
    }
}