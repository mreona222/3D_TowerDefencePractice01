using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefencePractice.Constructable.Turrets
{
    public abstract class TurretFireAnimationBase : MonoBehaviour
    {
        [SerializeField]
        protected ParticleSystem fireEffect = null;

        [HideInInspector]
        public Transform firePointTransform;

        [SerializeField]
        protected TurretBehaviourBase turretBehaviourBase;

        public abstract void FireAnimation();
    }
}