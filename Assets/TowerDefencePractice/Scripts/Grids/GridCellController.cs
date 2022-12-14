using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

using TowerDefencePractice.Turrets;

namespace TowerDefencePractice.Grids
{
    public class GridCellController : MonoBehaviour
    {
        // Grid�̃C���^���N�g�ݒ������
        [SerializeField]
        public bool interactable;

        [SerializeField]
        ParticleSystem interactableParticle, uninteractableParticle;

        public bool turretExist;

        [SerializeField]
        GameObject[] TurretObj;



        /// <summary>
        /// �O���b�h���n�C���C�g
        /// </summary>
        /// <param name="hit">Grid��RaycastHit���</param>
        public void GridBrighten()
        {
            // �C���^���N�g�\�ȂƂ�
            if (interactable)
            {
                interactableParticle.gameObject.SetActive(true);
            }
            // �C���^���N�g�\�łȂ��Ƃ�
            else
            {
                uninteractableParticle.gameObject.SetActive(true);
            }
        }

        /// <summary>
        /// �O���b�h�̃n�C���C�g������
        /// </summary>
        public void GridLightOff()
        {
            if (interactable)
            {
                interactableParticle.gameObject.SetActive(false);
            }
            else
            {
                uninteractableParticle.gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// �^���b�g�𐶐�����
        /// </summary>
        public void InstantiateTurret(Turret turret)
        {
            GameObject TurretInstance = Instantiate(TurretObj[(int)turret], transform.position + new Vector3(0, transform.localScale.y / 2, 0), transform.rotation, transform);
            turretExist = true;
        }
    }
}