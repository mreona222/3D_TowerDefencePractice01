using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TowerDefencePractice.Damages;

namespace TowerDefencePractice.Bullets
{
    public class BulletBehaviourNormal : BulletBehaviourBase
    {
        public override void BaseMovement()
        {
            GetComponent<Rigidbody>().velocity = -transform.up * 50.0f;
        }

        public override void BulletDestroy()
        {
            StartCoroutine(BulletDestroyCoroutine());
        }

        IEnumerator BulletDestroyCoroutine()
        {
            yield return new WaitForSeconds(0.5f);
            Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            IDamageApplicable damageApp = other.GetComponent<IDamageApplicable>();
            if (damageApp != null)
            {
                damageApp.DamageApplicate(1.0f, 1.0f);
            }
        }
    }
}