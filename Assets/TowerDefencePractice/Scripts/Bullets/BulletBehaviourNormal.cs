using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TowerDefencePractice.Bullets
{
    public class BulletBehaviourNormal : BulletBehaviourBase
    {
        public override void BaseMovement()
        {
            GetComponent<Rigidbody>().velocity = -transform.up * 50.0f;
        }
    }
}