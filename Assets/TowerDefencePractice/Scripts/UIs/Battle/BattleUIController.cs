using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TowerDefencePractice.Constructable;
using TowerDefencePractice.Constructable.Turrets;
using TowerDefencePractice.Grids;
using TowerDefencePractice.Managers;

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
        [SerializeField]
        Text purchaseStuff;
        [SerializeField]
        Text purchaseCost;

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
        [SerializeField]
        Text upgradeStuff;
        [SerializeField]
        Text upgradeCoin;
        [SerializeField]
        Text upgradeText;
        [SerializeField]
        ScrollRect upgradeScroll;

        private enum UpgradeNumber
        {
            Power,
            Speed,
            Range
        }
        UpgradeNumber upgradeNumber;


        private void Start()
        {
            // ???????\???w????????????
            PurchaseConstructablePanelInitialize();
        }

        // -----------------------------------------------------------
        // ???????\???w??
        // -----------------------------------------------------------

        /// <summary>
        /// ???????\???w????????????
        /// </summary>
        void PurchaseConstructablePanelInitialize()
        {
            // ???????\??????????????
            for (int i = 0; i < constructableBoss.constructableSctiptbaleObject.Length; i++)
            {
                GameObject newConstructableIconButtonInstance = Instantiate(newConstructableIconButton, newConstructableList);
                newConstructableIconButtonInstance.transform.GetComponentInChildren<Image>().sprite = constructableBoss.constructableSctiptbaleObject[i].constructableIcon;
                newConstructableIconButtonInstance.transform.GetComponentInChildren<Text>().text = constructableBoss.constructableSctiptbaleObject[i].constructableName;
            }
        }

        /// <summary>
        /// ???????\???w???????L????
        /// </summary>
        public void PurchaseConstructablePanelActivate()
        {
            // ?p?l?????L????
            purchaseConstructablePanel.SetActive(true);

            // ??????????????
            newConstructableStatusPanel.SetActive(false);
            // ?w???{?^??????????
            purchaseButton.SetActive(false);
        }

        /// <summary>
        /// ???????\???X?e?[?^?X????
        /// </summary>
        public void NewConstructableStatus(int newConstructable)
        {
            // ?????????L????
            newConstructableStatusPanel.SetActive(true);
            // ?w???{?^?????L????
            purchaseButton.SetActive(true);

            // ?e?X?e?[?^?X???l???X?V
            // *******************************
            newConstructableNameInstance.text = constructableBoss.constructableSctiptbaleObject[purchaseConstructableNumber].constructableName;

            newConstructableIconInstance.sprite = constructableBoss.constructableSctiptbaleObject[purchaseConstructableNumber].constructableIcon;

            newConstructableStuffInstance.text = $"{constructableBoss.constructableSctiptbaleObject[purchaseConstructableNumber].requireStuffBase:f2}";

            newConstructableCostInstance.text = $"{constructableBoss.constructableSctiptbaleObject[purchaseConstructableNumber].requireCoinBase:f2}";

            newConstructablePowerInstance.text = $"{constructableBoss.constructableSctiptbaleObject[purchaseConstructableNumber].firePowerBase:f2}";

            newConstructableSpeedInstance.text = $"{constructableBoss.constructableSctiptbaleObject[purchaseConstructableNumber].fireRateBase:f2}";

            newConstructableDPSInstance.text =
                $"{constructableBoss.constructableSctiptbaleObject[purchaseConstructableNumber].firePowerBase / constructableBoss.constructableSctiptbaleObject[purchaseConstructableNumber].fireRateBase:f2}";

            newConstructableRangeInstance.text = $"{constructableBoss.constructableSctiptbaleObject[purchaseConstructableNumber].fireRangeBase:f2}";

            purchaseStuff.text = $"{constructableBoss.constructableSctiptbaleObject[purchaseConstructableNumber].requireStuffBase}";
            purchaseCost.text = $"{constructableBoss.constructableSctiptbaleObject[purchaseConstructableNumber].requireCoinBase}";
        }

        /// <summary>
        /// ???????\???w???{?^??
        /// </summary>
        public void PurchaseButton()
        {
            currentGridCell.collider.GetComponent<GridCellController>().InstantiateConstructable(purchaseConstructableNumber);
            // ?????W???\??
            currentGridCell.collider.GetComponentInChildren<RangeCollider>().RangeEnable();

            StartCoroutine(DelayInitialize());

            IEnumerator DelayInitialize()
            {
                yield return null;
                UpgradeConstructablePanelInitialize();
                ConstructablePowerUpgradeButton();
            }
        }



        // -----------------------------------------------------------
        // ???????\???A?b?v?O???[?h
        // -----------------------------------------------------------

        /// <summary>
        /// ?A?b?v?O???[?h?????L????
        /// </summary>
        public void UpgradeConstructablePanelActivate()
        {
            upgradeConstructablePanel.SetActive(true);
            // ?A?b?v?O???[?h??????????
            UpgradeConstructablePanelInitialize();
            ConstructablePowerUpgradeButton();
        }

        private void UpgradeConstructablePanelInitialize()
        {
            // ?e?X?e?[?^?X???l???X?V
            // *******************************
            TurretBaseBehaviour currentTurretBehaviour = currentGridCell.transform.GetComponentInChildren<TurretBaseBehaviour>();
            TurretBaseGradeUp currentTurretGradeUp = currentGridCell.transform.GetComponentInChildren<TurretBaseGradeUp>();

            upgradeConstructableNameInstance.text = currentTurretBehaviour.turretData.constructableName;

            upgradeConstructableIconInstance.sprite = currentTurretBehaviour.turretData.constructableIcon;

            upgradeConstructableStuffInstance.text = $"{currentTurretBehaviour.turretData.fireRateMax:f2}";

            upgradeConstructableCostInstance.text = $"{currentTurretBehaviour.turretData.fireRateMax:f2}";

            upgradeConstructablePowerInstance.text = $"{currentTurretBehaviour.firePowerCurrent:f2}";
            upgradeNextConstructablePowerInstance.text = $"{currentTurretGradeUp.FirePowerCalculate(currentTurretBehaviour.firePowerCurrentLevel + 1):f2}";

            upgradeConstructableSpeedInstance.text = $"{currentTurretBehaviour.fireRateCurrent:f2}";
            upgradeNextConstructableSpeedInstance.text = $"{currentTurretGradeUp.FireRateCalculate(currentTurretBehaviour.fireRateCurrent + 1):f2}";

            upgradeConstructableDPSInstance.text = $"{currentTurretBehaviour.firePowerCurrent / currentTurretBehaviour.fireRateCurrent:f2}";
            upgradeNextConstructableDPSInstance.text =
                $"{currentTurretGradeUp.FirePowerCalculate(currentTurretBehaviour.firePowerCurrentLevel + 1) / currentTurretGradeUp.FireRateCalculate(currentTurretBehaviour.fireRateCurrentLevel + 1):f2}";

            upgradeConstructableRangeInstance.text = $"{currentTurretBehaviour.fireRangeCurrent:f2}";
            upgradeNextConstructableRangeInstance.text = $"{currentTurretGradeUp.FireRangeCalculate(currentTurretBehaviour.fireRangeCurrentLevel + 1):f2}";
        }

        public void ConstructablePowerUpgradeButton()
        {
            // *******************************
            upgradeNumber = UpgradeNumber.Power;
            upgradeStuff.text = $"{currentGridCell.transform.GetComponentInChildren<TurretBaseBehaviour>().name}";
            upgradeCoin.text = $"{currentGridCell.transform.GetComponentInChildren<TurretBaseBehaviour>().name}";
            upgradeText.text = "Power Upgrade";
        }

        public void ConstructableSpeedUpgradeButton()
        {
            // *******************************
            upgradeNumber = UpgradeNumber.Speed;
            upgradeStuff.text = $"{currentGridCell.transform.GetComponentInChildren<TurretBaseBehaviour>().name}";
            upgradeCoin.text = $"{currentGridCell.transform.GetComponentInChildren<TurretBaseBehaviour>().name}";
            upgradeText.text = "Speed Upgrade";
        }

        public void ConstructableRangeUpgradeButton()
        {
            // *******************************
            upgradeNumber = UpgradeNumber.Range;
            upgradeStuff.text = $"{currentGridCell.transform.GetComponentInChildren<TurretBaseBehaviour>().name}";
            upgradeCoin.text = $"{currentGridCell.transform.GetComponentInChildren<TurretBaseBehaviour>().name}";
            upgradeText.text = "Range Upgrade";
        }

        public void UpgradeButton()
        {
            Vector2 scrollPosition = upgradeScroll.normalizedPosition;

            // *******************************
            switch (upgradeNumber)
            {
                case UpgradeNumber.Power:
                    currentGridCell.transform.GetComponentInChildren<TurretBaseGradeUp>().FirePowerGradeUp();
                    break;
                case UpgradeNumber.Speed:
                    currentGridCell.transform.GetComponentInChildren<TurretBaseGradeUp>().FireRateGradeUp();
                    break;
                case UpgradeNumber.Range:
                    currentGridCell.transform.GetComponentInChildren<TurretBaseGradeUp>().FireRangeGradeUp();
                    break;
            }

            UpgradeConstructablePanelInitialize();

            StartCoroutine(ScrollPosition(scrollPosition));
        }

        private IEnumerator ScrollPosition(Vector2 position)
        {
            yield return null;
            upgradeScroll.normalizedPosition = position;
        }

        public void CellButton()
        {

        }
    }
}