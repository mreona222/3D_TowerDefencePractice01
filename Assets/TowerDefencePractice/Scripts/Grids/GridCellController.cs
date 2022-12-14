using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

using TowerDefencePractice.Constructable;
using TowerDefencePractice.Constructable.Turrets;

namespace TowerDefencePractice.Grids
{
    public class GridCellController : MonoBehaviour
    {
        // Grid�̃C���^���N�g�ݒ������
        [SerializeField]
        public bool interactable;

        [SerializeField]
        ParticleSystem interactableParticle, uninteractableParticle;

        public bool constructableExist;

        [SerializeField]
        ConstructableBoss constructableBoss;

        /// <summary>
        /// �O���b�h���n�C���C�g
        /// </summary>
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
        /// �����\���𐶐�����
        /// </summary>
        public void InstantiateConstructable(int constructable)
        {
            constructableBoss.constructableSctiptbaleObject[constructable].constructablePrefab.GetComponent<IConstructable>().InstantiateConstructable(transform);
            constructableExist = true;
        }
    }
}