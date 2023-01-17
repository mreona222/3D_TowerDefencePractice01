using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefencePractice.Constructable.Turrets
{
    public class TurretGradeUpNormal : TurretGradeUpBase
    {
        [SerializeField]
        private float fireRateLinearRatio;
        [SerializeField]
        private float fireRatePow;

        public override float FireRateCalculate(float level)
        {
            // üŒ` + ”ñüŒ`
            return turretBehaviour.turretData.fireRateBase - (
                turretBehaviour.turretData.fireRateRatio * (turretBehaviour.turretData.fireRateBase - turretBehaviour.turretData.fireRateMax) * (level / turretBehaviour.turretData.fireRateMaxLevel) +
                (1 - turretBehaviour.turretData.fireRateRatio) * (turretBehaviour.turretData.fireRateBase - turretBehaviour.turretData.fireRateMax) *
                    Mathf.Pow((level / turretBehaviour.turretData.fireRateMaxLevel), turretBehaviour.turretData.fireRatePow)
                );
        }
    }
}