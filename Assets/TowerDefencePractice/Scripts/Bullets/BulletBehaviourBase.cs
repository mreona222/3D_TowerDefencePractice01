using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefencePractice.Bullets
{
    public abstract class BulletBehaviourBase : MonoBehaviour
    {
        [SerializeField]
        protected float bulletDestroyTime;


        // Start is called before the first frame update
        void Start()
        {
            BaseMovement();
            BulletDestroy();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public abstract void BaseMovement();

        public abstract void BulletDestroy();
    }
}