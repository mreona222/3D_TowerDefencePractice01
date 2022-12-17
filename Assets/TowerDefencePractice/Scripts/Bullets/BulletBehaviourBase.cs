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

        [HideInInspector]
        public Collider target;

        // Start is called before the first frame update
        void Start()
        {
            BaseMovement();
            StartCoroutine(BulletDestroy());
        }

        public abstract void BaseMovement();

        public abstract IEnumerator BulletDestroy();
    }
}