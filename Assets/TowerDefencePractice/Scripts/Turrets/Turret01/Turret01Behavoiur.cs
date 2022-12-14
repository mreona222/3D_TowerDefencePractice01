using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefencePractice.Turrets
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

        /// <summary>
        /// ����
        /// </summary>
        protected override void Fire()
        {
            StartCoroutine(FireStroke());
        }

        IEnumerator FireStroke()
        {
            // �ˌ��A�j���[�V����
            turretAnimator.SetTrigger("Shoot");

            // �e�̐���
            

            // �A�ˋ֎~
            canShoot = false;

            yield return new WaitForSeconds(fireRateCurrent);
            canShoot = true;
        }
    }
}