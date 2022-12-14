using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TowerDefencePractice.Constructable;
using TowerDefencePractice.Constructable.Turrets;
using TowerDefencePractice.Grids;

namespace TowerDefencePractice.UIs
{
    public class BattleUIController : MonoBehaviour
    {
        [SerializeField]
        GameObject purchaseConstructablePanel;
        [SerializeField]
        GameObject upgradeConstructablePanel;

        [SerializeField]
        ConstructableBoss constructableBoss;

        [SerializeField]
        Transform newConstructableList;
        [SerializeField]
        GameObject newConstructableIconButton;

        [HideInInspector]
        public int purchaseConstructableNumber = 0;

        public RaycastHit currentGridCell;

        private void Start()
        {
            PurchaseConstructablePanelInitialize();
        }

        // -----------------------------------------------------------
        // �����\���w��
        // -----------------------------------------------------------

        /// <summary>
        /// �����\���w����ʏ�����
        /// </summary>
        void PurchaseConstructablePanelInitialize()
        {
            for (int i = 0; i < constructableBoss.constructableSctiptbaleObject.Length; i++)
            {
                GameObject newConstructableIconButtonInstance = Instantiate(newConstructableIconButton, newConstructableList);
                newConstructableIconButtonInstance.GetComponentInChildren<Image>().sprite = constructableBoss.constructableSctiptbaleObject[i].constructableIcon;
                newConstructableIconButtonInstance.GetComponentInChildren<Text>().text = constructableBoss.constructableSctiptbaleObject[i].constructableName;
            }
        }

        /// <summary>
        /// �����\���w����ʗL����
        /// </summary>
        public void PurchaseConstructablePanelActivate()
        {
            purchaseConstructablePanel.SetActive(true);

        }

        /// <summary>
        /// �����\���w���{�^��
        /// </summary>
        public void PurchaseButton()
        {
            currentGridCell.collider.GetComponent<GridCellController>().InstantiateConstructable(purchaseConstructableNumber);
        }

        // -----------------------------------------------------------
        // �����\���A�b�v�O���[�h
        // -----------------------------------------------------------

        /// <summary>
        /// �A�b�v�O���[�h��ʗL����
        /// </summary>
        public void UpgradeConstructablePanelActivate()
        {
            upgradeConstructablePanel.SetActive(true);
        }
    }
}