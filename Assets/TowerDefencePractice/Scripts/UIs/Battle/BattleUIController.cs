using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TowerDefencePractice.Turrets;

namespace TowerDefencePractice.UIs
{
    public class BattleUIController : MonoBehaviour
    {
        [SerializeField]
        GameObject purchaseTurretPanel;
        [SerializeField]
        GameObject upgradeTurretPanel;

        [SerializeField]
        TurretData[] turretData;

        [SerializeField]
        Transform newTurretList;
        [SerializeField]
        GameObject newTurretIconButton;

        public RaycastHit currentGridCell;

        private void Start()
        {
            PurchaseTurretPanelInitialize();
        }

        /// <summary>
        /// タレット購入画面初期化
        /// </summary>
        void PurchaseTurretPanelInitialize()
        {
            if(turretData.Length!= System.Enum.GetValues(typeof(Turret)).Length)
            {
                Debug.LogError("タレット情報の数に間違いがあります。（BattleUIController）");
            }

            for(int i = 0; i < System.Enum.GetValues(typeof(Turret)).Length; i++)
            {
                GameObject newTurretIconButtonInstance = Instantiate(newTurretIconButton, newTurretList);
                newTurretIconButtonInstance.GetComponentInChildren<Image>().sprite = turretData[i].turretIcon;
                newTurretIconButtonInstance.GetComponentInChildren<Text>().text = turretData[i].turretName;
            }
        }

        /// <summary>
        /// タレット購入画面有効化
        /// </summary>
        public void PurchaseTurretPanelActivate()
        {
            purchaseTurretPanel.SetActive(true);

        }

        /// <summary>
        /// タレット購入ボタン
        /// </summary>
        public void PurchaseButton()
        {

        }


        public void UpgradeTurretPanelActivate()
        {
            upgradeTurretPanel.SetActive(true);
        }
    }
}