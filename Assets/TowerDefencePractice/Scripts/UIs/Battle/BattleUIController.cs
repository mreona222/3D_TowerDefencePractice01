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
        [SerializeField]
        GameObject newConstructableStatusPanel;
        [SerializeField]
        GameObject purchaseButton;

        [SerializeField]
        Text newConstructableNameInstance;
        [SerializeField]
        Image newConstructableIconInstance;
        [SerializeField]
        Text newConstructableStuffInstance;
        [SerializeField]
        Text newConstructableCostInstance;
        [SerializeField]
        Text newConstructablePowerInstance;
        [SerializeField]
        Text newConstructableSpeedInstance;
        [SerializeField]
        Text newConstructableRangeInstance;

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
            // �����\���ꗗ�̏�����
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
            // �p�l���̗L����
            purchaseConstructablePanel.SetActive(true);

            // �������̖�����
            newConstructableStatusPanel.SetActive(false);
            // �w���{�^���̖�����
            purchaseButton.SetActive(false);
        }

        /// <summary>
        /// �����\���X�e�[�^�X���
        /// </summary>
        public void NewConstructableStatus(int newConstructable)
        {
            // �������̗L����
            newConstructableStatusPanel.SetActive(true);
            // �w���{�^���̗L����
            purchaseButton.SetActive(true);

            //newConstructableNameInstance.text = currentGridCell.transform.GetComponentInChildren<TurretBaseBehaviour>().turretData.constructableName;
            //newConstructableIconInstance.sprite = currentGridCell.transform.GetComponentInChildren<TurretBaseBehaviour>().turretData.constructableIcon;
            //newConstructableStuffInstance.text = $"{ currentGridCell.transform.GetComponentInChildren<TurretBaseBehaviour>().turretData.fireRateMax }";
            //newConstructableCostInstance.text=$"{ currentGridCell.transform.GetComponentInChildren<TurretBaseBehaviour>().turretData.fireRateMax }";
            //newConstructablePowerInstance.text=$"{ currentGridCell.transform.GetComponentInChildren<TurretBaseBehaviour>().turretData.fireRateMax }";
            //newConstructableSpeedInstance.text=$"{ currentGridCell.transform.GetComponentInChildren<TurretBaseBehaviour>().turretData.fireRateMax }";
            //newConstructableRangeInstance.text=$"{ currentGridCell.transform.GetComponentInChildren<TurretBaseBehaviour>().turretData.fireRateMax }";


            newConstructableNameInstance.text = constructableBoss.constructableSctiptbaleObject[purchaseConstructableNumber].constructableName;
            newConstructableIconInstance.sprite = constructableBoss.constructableSctiptbaleObject[purchaseConstructableNumber].constructableIcon;
            newConstructableStuffInstance.text = $"{ constructableBoss.constructableSctiptbaleObject[purchaseConstructableNumber].fireRateMax }";
            newConstructableCostInstance.text = $"{ constructableBoss.constructableSctiptbaleObject[purchaseConstructableNumber].fireRateMax }";
            newConstructablePowerInstance.text = $"{ constructableBoss.constructableSctiptbaleObject[purchaseConstructableNumber].firePowerBase }";
            newConstructableSpeedInstance.text = $"{ constructableBoss.constructableSctiptbaleObject[purchaseConstructableNumber].fireRateBase }";
            newConstructableRangeInstance.text = $"{ constructableBoss.constructableSctiptbaleObject[purchaseConstructableNumber].fireRateMax }";

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
            UpgradeConstructablePanleInitialize();
        }

        private void UpgradeConstructablePanleInitialize()
        {

        }
    }
}