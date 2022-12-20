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
        // �^���b�g��ID
        public System.Guid turretID;

        // �^���b�g�̏��
        public ConstructableData turretData;


        // ���݂̎ˌ��Ԋu���x��
        public float fireRateCurrentLevel;
        // ���݂̎ˌ��Ԋu[s/times]
        public float fireRateCurrent;
        // �ˌ��\
        [HideInInspector]
        public bool canShoot;
        // ���b�N�I������
        [HideInInspector]
        public bool lockon;

        // ���݂̍U�����x��
        public float firePowerCurrentLevel;
        // ���݂̍U����
        public float firePowerCurrent;
        // �X�^������
        public float stanTime;



        // �^���b�g�̃A�j���[�^�[
        protected Animator turretAnimator;
        // �ˌ��A�j���[�V�����̃N���b�v
        [SerializeField]
        AnimationClip turretFireAnimationClip;


        // �e�̃v���n�u
        [SerializeField]
        protected GameObject bullet;
        // �e�̔��ˈʒu
        public Transform[] firePointTransform = null;
        // ���̔��ˈʒu
        protected int nextPoint = 0;


        protected virtual void Start()
        {
            turretID = System.Guid.NewGuid();

            turretAnimator = GetComponent<Animator>();

            fireRateCurrentLevel = 1.0f;
            fireRateCurrent = turretData.fireRateBase;
            canShoot = true;
        }

        protected virtual void Update()
        {

        }


        // ---------------------------------------------------------------------------------
        // �^���b�g����
        // ---------------------------------------------------------------------------------

        GameObject IConstructable.InstantiateConstructable(Transform gridCell)
        {
            return Instantiate(turretData.constructablePrefab, gridCell.position + new Vector3(0, gridCell.localScale.y / 2, 0), gridCell.rotation, gridCell);
        }



        // ---------------------------------------------------------------------------------
        // ���ˊ֌W
        // ---------------------------------------------------------------------------------

        /// <summary>
        /// ����
        /// </summary>
        public void Fire(Collider target)
        {
            // �ˌ��A�j���[�V����
            turretAnimator.SetTrigger("Shoot");

            // �e�̐���
            Transform fPTransform = firePointTransform[nextPoint % firePointTransform.Length];
            nextPoint++;
            GetComponent<TurretFireAnimation>().firePointTransform = fPTransform;

            GameObject bulletInstance = Instantiate(bullet, fPTransform.position, fPTransform.rotation * bullet.transform.rotation);
            bulletInstance.transform.localScale = Vector3.Scale(bulletInstance.transform.localScale, transform.parent.localScale);
            bulletInstance.GetComponent<BulletBehaviourBase>().parentTurret = transform;
            bulletInstance.GetComponent<BulletBehaviourBase>().target = target;

            // �_���[�W��^����
            target.GetComponent<IDamageApplicable>().DamageApplicate(firePowerCurrent, stanTime);

            // �A�ˋ֎~
            StartCoroutine(FireStroke());
        }

        IEnumerator FireStroke()
        {
            // �A�ˋ֎~
            canShoot = false;

            yield return new WaitForSeconds(fireRateCurrent);
            canShoot = true;
        }


        // ---------------------------------------------------------------------------------
        // ��]
        // ---------------------------------------------------------------------------------

        public void LookTarget(Vector3 position)
        {
            Vector3 targetPosition = new Vector3(position.x, transform.position.y, position.z);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetPosition - transform.position, transform.up), 10.0f * Time.deltaTime);
            lockon = Vector3.Angle(transform.forward, targetPosition - transform.position) < 10.0f;
        }



    }
}