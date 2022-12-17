using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TowerDefencePractice.Damages;
using TowerDefencePractice.Character.Enemies;

namespace TowerDefencePractice.Constructable.Turrets
{
    public class RangeCollider : MonoBehaviour
    {
        TurretBaseBehaviour turretBaseBehaviour;

        public List<Collider> targetList = new List<Collider>();

        void Start()
        {
            turretBaseBehaviour = GetComponentInParent<TurretBaseBehaviour>();
        }

        void Update()
        {
            // 範囲内に敵がいるとき
            if (targetList.Count != 0)
            {
                // HPが0以下の敵は無視する
                if (targetList[0].GetComponent<EnemyBehaviourBase>().currentHP <= 0)
                {
                    targetList.Remove(targetList[0]);
                }
                else
                {
                    if (turretBaseBehaviour.lockon)
                    {
                        if (turretBaseBehaviour.canShoot)
                        {
                            turretBaseBehaviour.Fire(targetList[0]);
                        }
                    }
                    turretBaseBehaviour.LookTarget(targetList[0].transform.position);
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<EnemyBehaviourBase>() != null)
            {
                targetList.Add(other);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<EnemyBehaviourBase>() != null)
            {
                targetList.Remove(other);
            }
        }
    }
}