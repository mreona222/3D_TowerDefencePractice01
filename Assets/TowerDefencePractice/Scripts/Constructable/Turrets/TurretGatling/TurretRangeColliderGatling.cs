using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TowerDefencePractice.Character.Enemies;

namespace TowerDefencePractice.Constructable.Turrets
{
    public class TurretRangeColliderGatling : TurretRangeColliderBase
    {
        [SerializeField]
        TurretBehaviourGatling turretBehaviourGatling;

        protected override void Update()
        {
            // ターゲットリストの更新
            for (int i = 0; i < targetList.Count; i++)
            {
                // シーン上にいない敵はターゲットから外す
                if (targetList[i] == null)
                {
                    targetList.Remove(targetList[i]);
                }
                else
                {
                    // HPが0の敵はターゲットから外す
                    if (targetList[i].GetComponent<EnemyBehaviourBase>().currentHP.Value <= 0)
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
                else
                {
                    turretBehaviourGatling.StopFireAnimation();
                }
                turretBaseBehaviour.LookTarget(targetList[0].transform.position);
            }
            else
            {
                turretBehaviourGatling.StopFireAnimation();
            }
        }
    }
}