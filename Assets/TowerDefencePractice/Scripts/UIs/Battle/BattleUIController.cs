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
        // 建造可能物購入
        // -----------------------------------------------------------

        /// <summary>
        /// 建造可能物購入画面初期化
        /// </summary>
        void PurchaseConstructablePanelInitialize()
        {
            // 建造可能物一覧の初期化
            for (int i = 0; i < constructableBoss.constructableSctiptbaleObject.Length; i++)
            {
                GameObject newConstructableIconButtonInstance = Instantiate(newConstructableIconButton, newConstructableList);
                newConstructableIconButtonInstance.transform.GetComponentInChildren<Image>().sprite = constructableBoss.constructableSctiptbaleObject[i].constructableIcon;
                newConstructableIconButtonInstance.transform.GetComponentInChildren<Text>().text = constructableBoss.constructableSctiptbaleObject[i].constructableName;
            }
        }

        /// <summary>
        /// 建造可能物購入画面有効化
        /// </summary>
        public void PurchaseConstructablePanelActivate()
        {
            // パネルの有効化
            purchaseConstructablePanel.SetActive(true);

            // 説明文の無効化
            newConstructableStatusPanel.SetActive(false);
            // 購入ボタンの無効化
            purchaseButton.SetActive(false);
        }

        /// <summary>
        /// 建造可能物ステータス画面
        /// </summary>
        public void NewConstructableStatus(int newConstructable)
        {
            // 説明文の有効化
            newConstructableStatusPanel.SetActive(true);
            // 購入ボタンの有効化
            purchaseButton.SetActive(true);

            // 各ステータスの値の更新
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
        /// 建造可能物購入ボタン
        /// </summary>
        public void PurchaseButton()
        {
            currentGridCell.collider.GetComponent<GridCellController>().InstantiateConstructable(purchaseConstructableNumber);
            StartCoroutine(DelayInitialize());

            IEnumerator DelayInitialize()
            {
                yield return null;
                UpgradeConstructablePanelInitialize();
                ConstructablePowerUpgradeButton();
            }
        }



        // -----------------------------------------------------------
        // 建造可能物アップグレード
        // -----------------------------------------------------------

        /// <summary>
        /// アップグレード画面有効化
        /// </summary>
        public void UpgradeConstructablePanelActivate()
        {
            upgradeConstructablePanel.SetActive(true);
            // アップグレード画面初期化
            UpgradeConstructablePanelInitialize();
            ConstructablePowerUpgradeButton();
        }

        private void UpgradeConstructablePanelInitialize()
        {
            // 各ステータスの値の更新
            // *******************************
            TurretBaseBehaviour currentTurretBehaviour = currentGridCell.transform.GetComponentInChildren<TurretBaseBehaviour>();
            TurretBaseGradeUp currentTurretGradeUp = currentGridCell.transform.GetComponentInChildren<TurretBaseGradeUp>();

            upgradeConstructableNameInstance.text = currentTurretBehaviour.turretData.constructableName;

            upgradeConstructableIconInstance.sprite = currentTurretBehaviour.turretData.constructableIcon;

            upgradeConstructableStuffInstance.text = $"{currentTurretBehaviour.turretData.fireRateMax:f2}";

            upgradeConstructableCostInstance.text = $"{currentTurretBehaviour.turretData.fireRateMax:f2}";

            upgradeConstructablePowerInstance.text = $"{currentTurretBehaviour.firePowerCurrent:f2}";
            upgradeNextConstructablePowerInstance.text = $"{currentTurretGradeUp.FirePowerCalculate(91):f2}";

            upgradeConstructableSpeedInstance.text = $"{currentTurretBehaviour.fireRateCurrent:f2}";
            upgradeNextConstructableSpeedInstance.text = $"{currentTurretGradeUp.FireRateCalculate(currentTurretBehaviour.fireRateCurrent + 1):f2}";

            upgradeConstructableDPSInstance.text = $"{currentTurretBehaviour.firePowerCurrent / currentTurretBehaviour.fireRateCurrent:f2}";
            upgradeNextConstructableDPSInstance.text = $"{currentTurretGradeUp.FirePowerCalculate(1) / currentTurretGradeUp.FireRateCalculate(1):f2}";

            upgradeConstructableRangeInstance.text = $"{currentTurretBehaviour.turretData.fireRateMax:f2}";
            upgradeNextConstructableRangeInstance.text = $"{currentTurretBehaviour.fireRateNext:f2}";
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