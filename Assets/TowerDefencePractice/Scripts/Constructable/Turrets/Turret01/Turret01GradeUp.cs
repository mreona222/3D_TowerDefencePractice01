using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefencePractice.Constructable.Turrets
{
    public class Turret01GradeUp : TurretBaseGradeUp
    {
        [SerializeField]
        private float fireRateLinearRatio;
        [SerializeField]
        private float fireRatePow;

        protected override float FireRateCalculate()
        {
            // üŒ` + ”ñüŒ`
            return turretBehaviour.turretData.fireRateBase - (
                fireRateLinearRatio * (turretBehaviour.turretData.fireRateBase - turretBehaviour.turretData.fireRateMax) * (turretBehaviour.fireRateCurrentLevel / turretBehaviour.turretData.fireRateMaxLevel) +
                (1 - fireRateLinearRatio) * (turretBehaviour.turretData.fireRateBase - turretBehaviour.turretData.fireRateMax) *
                    Mathf.Pow((turretBehaviour.fireRateCurrentLevel / turretBehaviour.turretData.fireRateMaxLevel), fireRatePow)
                );
        }
    }
}