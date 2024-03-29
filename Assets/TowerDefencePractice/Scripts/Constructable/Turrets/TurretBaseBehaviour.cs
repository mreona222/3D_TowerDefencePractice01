using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TowerDefencePractice.Damages;
using TowerDefencePractice.Bullets;

namespace TowerDefencePractice.Constructable.Turrets
{
    public enum Turret
    {
        Turret01 = 0,
        Turret02,
        Turret03,
        Turret04,
    }

    public abstract class TurretBaseBehaviour : MonoBehaviour, IConstructable
    {
        // ^bgÌID
        public System.Guid turretID;

        // ^bgÌîñ
        public ConstructableData turretData;


        // »ÝÌËÔux
        public float fireRateCurrentLevel;
        // »ÝÌËÔu[s/times]
        public float fireRateCurrent;
        // ÌËÔu
        public float fireRateNext;
        // ËÂ\
        [HideInInspector]
        public bool canShoot;
        // bNI®¹
        [HideInInspector]
        public bool lockon;

        // »ÝÌUx
        public float firePowerCurrentLevel;
        // »ÝÌUÍ
        public float firePowerCurrent;
        // X^Ô
        public float stanTime;

        // »ÝÌËöx
        public float fireRangeCurrentLevel;
        // »ÝÌËö
        public float fireRangeCurrent;


        // ^bgÌAj[^[
        protected Animator turretAnimator;
        // ËAj[VÌNbv
        [SerializeField]
        AnimationClip turretFireAnimationClip;


        // eÌvnu
        [SerializeField]
        protected GameObject bullet;
        // eÌ­ËÊu
        public Transform[] firePointTransform = null;
        // Ì­ËÊu
        protected int nextPoint = 0;


        protected virtual void Start()
        {
            turretID = System.Guid.NewGuid();

            turretAnimator = GetComponent<Animator>();

            GetComponent<TurretBaseGradeUp>().Initialize();

            canShoot = true;
        }

        protected virtual void Update()
        {

        }


        // ---------------------------------------------------------------------------------
        // ^bg¶¬
        // ---------------------------------------------------------------------------------

        GameObject IConstructable.InstantiateConstructable(Transform gridCell)
        {
            return Instantiate(turretData.constructablePrefab, gridCell.position + new Vector3(0, gridCell.localScale.y / 2, 0), gridCell.rotation, gridCell);
        }



        // ---------------------------------------------------------------------------------
        // ­ËÖW
        // ---------------------------------------------------------------------------------

        /// <summary>
        /// ­Ë
        /// </summary>
        public void Fire(Collider target)
        {
            // ËAj[V
            turretAnimator.SetTrigger("Shoot");

            // eÌ¶¬
            Transform fPTransform = firePointTransform[nextPoint % firePointTransform.Length];
            nextPoint++;
            GetComponent<TurretFireAnimation>().firePointTransform = fPTransform;

            GameObject bulletInstance = Instantiate(bullet, fPTransform.position, fPTransform.rotation * bullet.transform.rotation);
            bulletInstance.transform.localScale = Vector3.Scale(bulletInstance.transform.localScale, transform.parent.localScale);
            bulletInstance.GetComponent<BulletBehaviourBase>().parentTurret = transform;
            bulletInstance.GetComponent<BulletBehaviourBase>().target = target;

            // _[Wð^¦é
            target.GetComponent<IDamageApplicable>().DamageApplicate(firePowerCurrent, stanTime);

            // AËÖ~
            StartCoroutine(FireStroke());
        }

        IEnumerator FireStroke()
        {
            // AËÖ~
            canShoot = false;

            yield return new WaitForSeconds(fireRateCurrent);
            canShoot = true;
        }


        // ---------------------------------------------------------------------------------
        // ñ]
        // ---------------------------------------------------------------------------------

        public void LookTarget(Vector3 position)
        {
            Vector3 targetPosition = new Vector3(position.x, transform.position.y, position.z);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetPosition - transform.position, transform.up), 10.0f * Time.deltaTime);
            lockon = Vector3.Angle(transform.forward, targetPosition - transform.position) < 10.0f;
        }



        // ---------------------------------------------------------------------------------
        // p
        // ---------------------------------------------------------------------------------

        public void CellTurret()
        {
            Destroy(gameObject);
        }



    }
}