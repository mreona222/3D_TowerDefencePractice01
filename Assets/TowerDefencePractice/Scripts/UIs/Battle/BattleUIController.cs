using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefencePractice.UIs
{
    public class BattleUIController : MonoBehaviour
    {
        [SerializeField]
        GameObject purchaseTurretPanel;
        [SerializeField]
        GameObject upgradeTurretPanel;

        public RaycastHit currentGridCell;

        public void PurchaseTurretPanelActivate()
        {
            purchaseTurretPanel.SetActive(true);
        }

        public void UpgradeTurretPanelActivate()
        {
            upgradeTurretPanel.SetActive(true);
        }
    }
}