using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TowerDefencePractice.Character.Enemies;

namespace TowerDefencePractice.Constructable.Turrets
{
    public abstract class TurretRangeColliderBase : MonoBehaviour
    {
        [SerializeField]
        protected TurretBehaviourBase turretBaseBehaviour;

        public List<Collider> targetList = new List<Collider>();

        [SerializeField]
        private Canvas rangeCanvas;

        protected virtual void Start()
        {

        }

        protected abstract void Update();

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