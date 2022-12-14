using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefencePractice.Constructable.Turrets
{
    public class TurretFireAnimation : MonoBehaviour
    {
        [SerializeField]
        ParticleSystem fireEffect = null;

        [HideInInspector]
        public Transform firePointTransform;
        
        void FireAnimation()
        {
            ParticleSystem fireEffectInstance = Instantiate(fireEffect, firePointTransform.position, firePointTransform.rotation);
            if (transform.parent != null)
            {
                fireEffectInstance.transform.localScale = Vector3.Scale(fireEffectInstance.transform.localScale, transform.parent.localScale);
            }
        }
    }
}