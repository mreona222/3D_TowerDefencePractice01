using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TowerDefencePractice.Bullets
{
    public class BulletBehaviourNormal : BulletBehaviourBase
    {
        /// <summary>
        /// ��{�̓���
        /// </summary>
        public override void BaseMovement()
        {
            GetComponent<Rigidbody>().velocity = -transform.up * 500.0f;
        }


        // �߂��܂ōs������폜
        public override IEnumerator BulletDestroy()
        {
            yield return new WaitUntil(() => Vector3.Distance(target.transform.position, transform.position) <= 1.0f);
            Destroy(gameObject);
        }

        private void OnTriggerExit(Collider other)
        {
            // �˒��͈͂𒴂�����폜
            if (other.transform.parent == parentTurret)
            {
                Destroy(gameObject);
            }
        }
    }
}