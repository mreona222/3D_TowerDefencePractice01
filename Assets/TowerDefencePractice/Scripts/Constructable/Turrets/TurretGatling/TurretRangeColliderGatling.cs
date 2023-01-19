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
            // �^�[�Q�b�g���X�g�̍X�V
            for (int i = 0; i < targetList.Count; i++)
            {
                // �V�[����ɂ��Ȃ��G�̓^�[�Q�b�g����O��
                if (targetList[i] == null)
                {
                    targetList.Remove(targetList[i]);
                }
                else
                {
                    // HP��0�̓G�̓^�[�Q�b�g����O��
                    if (targetList[i].GetComponent<EnemyBehaviourBase>().currentHP.Value <= 0)
                    {
                        targetList.Remove(targetList[i]);
                    }
                }
            }

            // �^�[�Q�b�g���X�g�Ɏc�����G����
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