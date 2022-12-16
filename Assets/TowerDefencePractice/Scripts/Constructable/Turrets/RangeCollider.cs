using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TowerDefencePractice.Damages;

namespace TowerDefencePractice.Constructable.Turrets
{
    public class RangeCollider : MonoBehaviour
    {
        TurretBaseBehaviour turretBaseBehaviour;

        void Start()
        {
            turretBaseBehaviour = GetComponentInParent<TurretBaseBehaviour>();
        }

        // Update is called once per frame
        void Update()
        {

        }


        // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        // ���˂̃X�N���v�g������
        // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!




        private void OnTriggerStay(Collider other)
        {
            if (turretBaseBehaviour.canShoot)
            {
                turretBaseBehaviour.Fire();
            }
            turretBaseBehaviour.LookTarget(other.transform.position);
        }
    }
}