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
                newConstructableIconButtonInstance.GetComponentInChildren<Image>().sprite = constructableBoss.constructableSctiptbaleObject[i].constructableIcon;
                newConstructableIconButtonInstance.GetComponentInChildren<Text>().text = constructableBoss.constructableSctiptbaleObject[i].constructableName;
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

            newConstructableStuffInstance.text = $"{constructableBoss.constructableSctiptbaleObject[purchaseConstructableNumber].fireRateMax:f2}";

            newConstructableCostInstance.text = $"{constructableBoss.constructableSctiptbaleObject[purchaseConstructableNumber].fireRateMax:f2}";

            newConstructablePowerInstance.text = $"{constructableBoss.constructableSctiptbaleObject[purchaseConstructableNumber].firePowerBase:f2}";

            newConstructableSpeedInstance.text = $"{constructableBoss.constructableSctiptbaleObject[purchaseConstructableNumber].fireRateBase:f2}";

            newConstructableDPSInstance.text =
                $"{constructableBoss.constructableSctiptbaleObject[purchaseConstructableNumber].firePowerBase / constructableBoss.constructableSctiptbaleObject[purchaseConstructableNumber].fireRateBase:f2}";

            newConstructableRangeInstance.text = $"{constructableBoss.constructableSctiptbaleObject[purchaseConstructableNumber].fireRateMax:f2}";
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
            UpgradeConstructablePanelInitialize();
        }

        private void UpgradeConstructablePanelInitialize()
        {
            // 各ステータスの値の更新
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