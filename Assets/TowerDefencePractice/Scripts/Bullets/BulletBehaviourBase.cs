using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TowerDefencePractice.Damages;

namespace TowerDefencePractice.Bullets
{
    public abstract class BulletBehaviourBase : MonoBehaviour
    {
        [SerializeField]
        protected float bulletDestroyTime;

        [HideInInspector]
        public Transform parentTurret;

        // Start is called before the first frame update
        void Start()
        {
            BaseMovement();
        }

        public abstract void BaseMovement();

        public void BulletDestroy()
        {
            Destroy(gameObject);
        }


        private void OnTriggerEnter(Collider other)
        {
            IDamageApplicable damageApp = other.GetComponent<IDamageApplicable>();
            if (damageApp != null)
            {
                damageApp.DamageApplicate(1.0f, 1.0f);
                BulletDestroy();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.transform.parent == parentTurret)
            {
                BulletDestroy();
            }
        }
    }
}