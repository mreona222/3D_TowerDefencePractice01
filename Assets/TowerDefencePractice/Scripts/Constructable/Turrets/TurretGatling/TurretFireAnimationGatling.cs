using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TowerDefencePractice.Constructable.Turrets
{
    public class TurretFireAnimationGatling : TurretFireAnimationBase
    {
        public override void FireAnimation()
        {
            ParticleSystem fireEffectInstance = Instantiate(fireEffect, firePointTransform.position, firePointTransform.rotation);
            if (transform.parent != null)
            {
                fireEffectInstance.transform.localScale = Vector3.Scale(fireEffectInstance.transform.localScale, transform.parent.localScale);
            }
            turretBehaviourBase.nextPoint++;
        }
    }
}