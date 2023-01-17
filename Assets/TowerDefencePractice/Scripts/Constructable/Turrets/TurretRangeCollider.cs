using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TowerDefencePractice.Damages;
using TowerDefencePractice.Character.Enemies;

namespace TowerDefencePractice.Constructable.Turrets
{
    public class TurretRangeCollider : MonoBehaviour
    {
        TurretBehaviourBase turretBaseBehaviour;

        public List<Collider> targetList = new List<Collider>();

        [SerializeField]
        private Canvas rangeCanvas;

        void Start()
        {
            turretBaseBehaviour = GetComponentInParent<TurretBehaviourBase>();
        }

        void Update()
        {
            // �^�[�Q�b�g���X�g�̍X�V
            for(int i = 0; i < targetList.Count; i++)
            {
                // �V�[����ɂ��Ȃ��G�̓^�[�Q�b�g����O��
                if (targetList[i] == null)
                {
                    targetList.Remove(targetList[i]);
                }
                else
                {
                    // HP��0�̓G�̓^�[�Q�b�g����O��
                    if (targetList[i].GetComponent<EnemyBehaviourBase>().currentHP <= 0)
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

        public void RangeChange(float range)
        {
            transform.localScale = new Vector3(range, range, range);
        }
    }
}