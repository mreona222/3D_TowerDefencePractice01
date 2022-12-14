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
        // Gridのインタラクト設定をする
        [SerializeField]
        public bool interactable;

        [SerializeField]
        ParticleSystem interactableParticle, uninteractableParticle;

        public bool constructableExist;

        [SerializeField]
        ConstructableBoss constructableBoss;

        /// <summary>
        /// グリッドをハイライト
        /// </summary>
        public void GridBrighten()
        {
            // インタラクト可能なとき
            if (interactable)
            {
                interactableParticle.gameObject.SetActive(true);
            }
            // インタラクト可能でないとき
            else
            {
                uninteractableParticle.gameObject.SetActive(true);
            }
        }

        /// <summary>
        /// グリッドのハイライトを消す
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
        /// 建造可能物を生成する
        /// </summary>
        public void InstantiateConstructable(int constructable)
        {
            constructableBoss.constructableSctiptbaleObject[constructable].constructablePrefab.GetComponent<IConstructable>().InstantiateConstructable(transform);
            constructableExist = true;
        }
    }
}