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

        [SerializeField]
        private Canvas rangeCanvas;

        void Start()
        {
            turretBaseBehaviour = GetComponentInParent<TurretBaseBehaviour>();
        }

        void Update()
        {
            // ターゲットリストの更新
            for(int i = 0; i < targetList.Count; i++)
            {
                // シーン上にいない敵はターゲットから外す
                if (targetList[i] == null)
                {
                    targetList.Remove(targetList[i]);
                }
                else
                {
                    // HPが0の敵はターゲットから外す
                    if (targetList[i].GetComponent<EnemyBehaviourBase>().currentHP <= 0)
                    {
                        targetList.Remove(targetList[i]);
                    }
                }
            }

            // ターゲットリストに残った敵たち
            if (targetList.Count > 0)
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

        public void RangeEnable()
        {
            rangeCanvas.enabled = true;
        }

        public void RangeDisenable()
        {
            rangeCanvas.enabled = false;
        }
    }
}