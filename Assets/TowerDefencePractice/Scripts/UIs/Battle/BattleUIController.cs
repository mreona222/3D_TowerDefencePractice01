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
        // 建造可能物購入
        // -----------------------------------------------------------

        /// <summary>
        /// 建造可能物購入画面初期化
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
        /// 建造可能物購入画面有効化
        /// </summary>
        public void PurchaseConstructablePanelActivate()
        {
            purchaseConstructablePanel.SetActive(true);

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
        }
    }
}