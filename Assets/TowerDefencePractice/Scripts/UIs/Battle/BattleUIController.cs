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
        /// 建造可能物購入ボタン
        /// </summary>
        public void PurchaseButton()
        {
            currentGridCell.collider.GetComponent<GridCellController>().InstantiateConstructable(purchaseConstructableNumber);
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
            UpgradeConstructablePanleInitialize();
        }

        private void UpgradeConstructablePanleInitialize()
        {

        }
    }
}