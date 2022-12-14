using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TowerDefencePractice.Bullets
{
    public class BulletBehaviourNormal : BulletBehaviourBase
    {
        /// <summary>
        /// 基本の動き
        /// </summary>
        public override void BaseMovement()
        {
            GetComponent<Rigidbody>().velocity = -transform.up * 500.0f;
        }


        // 近くまで行ったら削除
        public override IEnumerator BulletDestroy()
        {
            yield return new WaitUntil(() => Vector3.Distance(target.transform.position, transform.position) <= 1.0f);
            Destroy(gameObject);
        }

        private void OnTriggerExit(Collider other)
        {
            // 射程範囲を超えたら削除
            if (other.transform.parent == parentTurret)
            {
                Destroy(gameObject);
            }
        }
    }
}