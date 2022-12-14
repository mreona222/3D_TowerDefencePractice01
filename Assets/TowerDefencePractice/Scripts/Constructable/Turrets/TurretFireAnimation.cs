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
            Instantiate(fireEffect, firePointTransform[nextFirePoint % firePointTransform.Length].position, firePointTransform[nextFirePoint % firePointTransform.Length].rotation);
            nextFirePoint++;
        }
    }
}