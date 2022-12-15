using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefencePractice.Constructable.Turrets
{
    public class TurretFireAnimation : MonoBehaviour
    {
        [SerializeField]
        ParticleSystem fireEffect = null;

        [SerializeField]
        Transform[] firePointTransform = null;

        private int nextFirePoint = 0;

        void FireAnimation()
        {
            ParticleSystem fireEffectInstance =
                Instantiate(fireEffect, firePointTransform[nextFirePoint % firePointTransform.Length].position, firePointTransform[nextFirePoint % firePointTransform.Length].rotation);
            fireEffectInstance.transform.localScale = Vector3.Scale(fireEffectInstance.transform.localScale, transform.parent.localScale);
            nextFirePoint++;
        }
    }
}