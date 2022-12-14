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
        /// �^���b�g�w����ʏ�����
        /// </summary>
        void PurchaseTurretPanelInitialize()
        {
            if(turretData.Length!= System.Enum.GetValues(typeof(Turret)).Length)
            {
                Debug.LogError("�^���b�g���̐��ɊԈႢ������܂��B�iBattleUIController�j");
            }

            for(int i = 0; i < System.Enum.GetValues(typeof(Turret)).Length; i++)
            {
                GameObject newTurretIconButtonInstance = Instantiate(newTurretIconButton, newTurretList);
                newTurretIconButtonInstance.GetComponentInChildren<Image>().sprite = turretData[i].turretIcon;
                newTurretIconButtonInstance.GetComponentInChildren<Text>().text = turretData[i].turretName;
            }
        }

        /// <summary>
        /// �^���b�g�w����ʗL����
        /// </summary>
        public void PurchaseTurretPanelActivate()
        {
            purchaseTurretPanel.SetActive(true);

        }

        /// <summary>
        /// �^���b�g�w���{�^��
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