using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

using TowerDefencePractice.Turrets;

namespace TowerDefencePractice.Grids
{
    public class GridCellController : MonoBehaviour
    {
        // Gridのインタラクト設定をする
        [SerializeField]
        public bool interactable;

        [SerializeField]
        ParticleSystem interactableParticle, uninteractableParticle;

        public bool turretExist;

        [SerializeField]
        GameObject[] TurretObj;



        /// <summary>
        /// グリッドをハイライト
        /// </summary>
        /// <param name="hit">GridのRaycastHit情報</param>
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
        /// タレットを生成する
        /// </summary>
        public void InstantiateTurret(Turret turret)
        {
            GameObject TurretInstance = Instantiate(TurretObj[(int)turret], transform.position + new Vector3(0, transform.localScale.y / 2, 0), transform.rotation, transform);
            turretExist = true;
        }
    }
}