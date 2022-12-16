using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefencePractice.Damages
{
    public interface IDamageApplicable
    {
        public void DamageApplicate(float damage, float stanTime);
    }
}