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
        Text newConstructableDPSInstance;
        [SerializeField]
        Text newConstructableRangeInstance;

        [HideInInspector]
        public int purchaseConstructableNumber = 0;

        public RaycastHit currentGridCell;

        [SerializeField]
        Text upgradeConstructableNameInstance;
        [SerializeField]
        Image upgradeConstructableIconInstance;
        [SerializeField]
        Text upgradeConstructableStuffInstance;
        [SerializeField]
        Text upgradeConstructableCostInstance;
        [SerializeField]
        Text upgradeConstructablePowerInstance;
        [SerializeField]
        Text upgradeNextConstructablePowerInstance;
        [SerializeField]
        Text upgradeConstructableSpeedInstance;
        [SerializeField]
        Text upgradeNextConstructableSpeedInstance;
        [SerializeField]
        Text upgradeConstructableDPSInstance;
        [SerializeField]
        Text upgradeNextConstructableDPSInstance;
        [SerializeField]
        Text upgradeConstructableRangeInstance;
        [SerializeField]
        Text upgradeNextConstructableRangeInstance;

        private enum UpgradeNumber
        {
            Power,
            Speed,
            Range
        }
        UpgradeNumber upgradeNumber;


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

            // �e�X�e�[�^�X�̒l�̍X�V
            // *******************************
            newConstructableNameInstance.text = constructableBoss.constructableSctiptbaleObject[purchaseConstructableNumber].constructableName;

            newConstructableIconInstance.sprite = constructableBoss.constructableSctiptbaleObject[purchaseConstructableNumber].constructableIcon;

            newConstructableStuffInstance.text = $"{constructableBoss.constructableSctiptbaleObject[purchaseConstructableNumber].fireRateMax:f2}";

            newConstructableCostInstance.text = $"{constructableBoss.constructableSctiptbaleObject[purchaseConstructableNumber].fireRateMax:f2}";

            newConstructablePowerInstance.text = $"{constructableBoss.constructableSctiptbaleObject[purchaseConstructableNumber].firePowerBase:f2}";

            newConstructableSpeedInstance.text = $"{constructableBoss.constructableSctiptbaleObject[purchaseConstructableNumber].fireRateBase:f2}";

            newConstructableDPSInstance.text =
                $"{constructableBoss.constructableSctiptbaleObject[purchaseConstructableNumber].firePowerBase / constructableBoss.constructableSctiptbaleObject[purchaseConstructableNumber].fireRateBase:f2}";

            newConstructableRangeInstance.text = $"{constructableBoss.constructableSctiptbaleObject[purchaseConstructableNumber].fireRateMax:f2}";
        }

        /// <summary>
        /// �����\���w���{�^��
        /// </summary>
        public void PurchaseButton()
        {
            currentGridCell.collider.GetComponent<GridCellController>().InstantiateConstructable(purchaseConstructableNumber);
            StartCoroutine(DelayInitialize());

            IEnumerator DelayInitialize()
            {
                yield return null;
                UpgradeConstructablePanelInitialize();
            }
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
            UpgradeConstructablePanelInitialize();
        }

        private void UpgradeConstructablePanelInitialize()
        {
            // �e�X�e�[�^�X�̒l�̍X�V
            // *******************************
            upgradeConstructableNameInstance.text = currentGridCell.transform.GetComponentInChildren<TurretBaseBehaviour>().turretData.constructableName;

            upgradeConstructableIconInstance.sprite = currentGridCell.transform.GetComponentInChildren<TurretBaseBehaviour>().turretData.constructableIcon;

            upgradeConstructableStuffInstance.text = $"{currentGridCell.transform.GetComponentInChildren<TurretBaseBehaviour>().turretData.fireRateMax:f2}";

            upgradeConstructableCostInstance.text = $"{currentGridCell.transform.GetComponentInChildren<TurretBaseBehaviour>().turretData.fireRateMax:f2}";

            upgradeConstructablePowerInstance.text = $"{currentGridCell.transform.GetComponentInChildren<TurretBaseBehaviour>().firePowerCurrent:f2}";
            upgradeNextConstructablePowerInstance.text = $"{currentGridCell.transform.GetComponentInChildren<TurretBaseBehaviour>().firePowerNext:f2}";

            upgradeConstructableSpeedInstance.text = $"{currentGridCell.transform.GetComponentInChildren<TurretBaseBehaviour>().fireRateCurrent:f2}";
            upgradeNextConstructableSpeedInstance.text = $"{currentGridCell.transform.GetComponentInChildren<TurretBaseBehaviour>().fireRateNext:f2}";

            upgradeConstructableDPSInstance.text =
                $"{currentGridCell.transform.GetComponentInChildren<TurretBaseBehaviour>().firePowerCurrent / currentGridCell.transform.GetComponentInChildren<TurretBaseBehaviour>().fireRateCurrent:f2}";
            upgradeNextConstructableDPSInstance.text =
                $"{currentGridCell.transform.GetComponentInChildren<TurretBaseBehaviour>().firePowerNext / currentGridCell.transform.GetComponentInChildren<TurretBaseBehaviour>().fireRateNext:f2}";

            upgradeConstructableRangeInstance.text = $"{currentGridCell.transform.GetComponentInChildren<TurretBaseBehaviour>().turretData.fireRateMax:f2}";
            upgradeNextConstructableRangeInstance.text = $"{currentGridCell.transform.GetComponentInChildren<TurretBaseBehaviour>().fireRateNext:f2}";
        }

        public void ConstructablePowerUpgradeButton()
        {
            // *******************************
            upgradeNumber = UpgradeNumber.Power;
        }

        public void ConstructableSpeedUpgradeButton()
        {
            // *******************************
            upgradeNumber = UpgradeNumber.Speed;
        }

        public void ConstructableRangeUpgradeButton()
        {
            // *******************************
            upgradeNumber = UpgradeNumber.Range;
        }

        public void ButtonHilightoff()
        {

        }

        public void UpgradeButton()
        {
            // *******************************
            switch (upgradeNumber)
            {
                case UpgradeNumber.Power:
                    Debug.Log("Power");
                    break;
                case UpgradeNumber.Speed:
                    Debug.Log("Speed");
                    break;
                case UpgradeNumber.Range:
                    Debug.Log("Range");
                    break;
            }
        }
    }
}