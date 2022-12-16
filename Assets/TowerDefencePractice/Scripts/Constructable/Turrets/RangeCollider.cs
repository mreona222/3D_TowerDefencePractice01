using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefencePractice.Constructable.Turrets
{
    public class RangeCollider : MonoBehaviour
    {
        TurretBaseBehaviour turretBaseBehaviour;

        // Start is called before the first frame update
        void Start()
        {
            turretBaseBehaviour = GetComponentInParent<TurretBaseBehaviour>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerStay(Collider other)
        {
            if (other.GetComponent<TowerDefencePractice.Damages.IDamageApplicable>() != null)
            {
                turretBaseBehaviour.LookTarget(other.transform.position);
            }
        }
    }
}