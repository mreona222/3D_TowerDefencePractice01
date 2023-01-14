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
        BattleSceneManager bsManager;

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
        Button upgradeButton;
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
            // �����\���w����ʏ�����
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
                newConstructableIconButtonInstance.transform.GetComponentInChildren<Image>().sprite = constructableBoss.constructableSctiptbaleObject[i].constructableIcon;
                newConstructableIconButtonInstance.transform.GetComponentInChildren<Text>().text = constructableBoss.constructableSctiptbaleObject[i].constructableName;
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
            // �w���{�^���̃C���^���N�g
            PurchaseButtonInteractable();

            // �e�X�e�[�^�X�̒l�̍X�V
            newConstructableNameInstance.text = constructableBoss.constructableSctiptbaleObject[purchaseConstructableNumber].constructableName;

            newConstructableIconInstance.sprite = constructableBoss.constructableSctiptbaleObject[purchaseConstructableNumber].constructableIcon;

            newConstructableStuffInstance.text = $"{Mathf.CeilToInt(constructableBoss.constructableSctiptbaleObject[purchaseConstructableNumber].requireStuffBase)}";

            newConstructableCostInstance.text = $"{Mathf.CeilToInt(constructableBoss.constructableSctiptbaleObject[purchaseConstructableNumber].requireCoinBase)}";

            newConstructablePowerInstance.text = $"{constructableBoss.constructableSctiptbaleObject[purchaseConstructableNumber].firePowerBase:f2}";

            newConstructableSpeedInstance.text = $"{constructableBoss.constructableSctiptbaleObject[purchaseConstructableNumber].fireRateBase:f2}";

            newConstructableDPSInstance.text =
                $"{constructableBoss.constructableSctiptbaleObject[purchaseConstructableNumber].firePowerBase / constructableBoss.constructableSctiptbaleObject[purchaseConstructableNumber].fireRateBase:f2}";

            newConstructableRangeInstance.text = $"{constructableBoss.constructableSctiptbaleObject[purchaseConstructableNumber].fireRangeBase:f2}";

            purchaseStuff.text = $"{Mathf.CeilToInt(constructableBoss.constructableSctiptbaleObject[purchaseConstructableNumber].requireStuffBase)}";
            purchaseCost.text = $"{Mathf.CeilToInt(constructableBoss.constructableSctiptbaleObject[purchaseConstructableNumber].requireCoinBase)}";
        }

        /// <summary>
        /// �����\���w���{�^��
        /// </summary>
        public void PurchaseButton()
        {
            int nextCoin = bsManager.money.Value - Mathf.CeilToInt(constructableBoss.constructableSctiptbaleObject[purchaseConstructableNumber].requireCoinBase);
            int nextStuff = bsManager.stuff.Value - Mathf.CeilToInt(constructableBoss.constructableSctiptbaleObject[purchaseConstructableNumber].requireStuffBase);

            // �����Ɛl�����\���ł���Ƃ�
            if (nextCoin >= 0 && nextStuff >= 0)
            {
                // �R�X�g
                bsManager.money.Value = nextCoin;
                bsManager.stuff.Value = nextStuff;

                // ����
                currentGridCell.collider.GetComponent<GridCellController>().InstantiateConstructable(purchaseConstructableNumber);

                // �����W�̕\��
                currentGridCell.collider.GetComponentInChildren<RangeCollider>().RangeEnable();

                StartCoroutine(DelayInitialize());

                IEnumerator DelayInitialize()
                {
                    yield return null;
                    UpgradeConstructablePanelActivate();
                }
            }
        }

        /// <summary>
        /// �w���{�^���̃C���^���N�g�𐧌�
        /// </summary>
        public void PurchaseButtonInteractable()
        {
            int nextCoin = bsManager.money.Value - Mathf.CeilToInt(constructableBoss.constructableSctiptbaleObject[purchaseConstructableNumber].requireCoinBase);
            int nextStuff = bsManager.stuff.Value - Mathf.CeilToInt(constructableBoss.constructableSctiptbaleObject[purchaseConstructableNumber].requireStuffBase);

            if (nextCoin >= 0 && nextStuff >= 0)
            {
                purchaseButton.GetComponentInChildren<Button>().interactable = true;
            }
            else
            {
                purchaseButton.GetComponentInChildren<Button>().interactable = false;
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
            purchaseConstructablePanel.SetActive(false);
            upgradeConstructablePanel.SetActive(true);
            // �A�b�v�O���[�h��ʏ�����
            UpgradeConstructablePanelInitialize();
            ConstructablePowerUpgradeButton();
            UpgradeButtonInteractable();
        }

        /// <summary>
        /// �A�b�v�O���[�h��ʂ̏�����
        /// </summary>
        private void UpgradeConstructablePanelInitialize()
        {
            // �e�X�e�[�^�X�̒l�̍X�V
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

        /// <summary>
        /// �����\���̉ἨA�b�v�O���[�h�{�^���̉������̓���
        /// </summary>
        public void ConstructablePowerUpgradeButton()
        {
            // *******************************
            upgradeNumber = UpgradeNumber.Power;
            upgradeStuff.text = $"{currentGridCell.transform.GetComponentInChildren<TurretBaseBehaviour>().name}";
            upgradeCoin.text = $"{currentGridCell.transform.GetComponentInChildren<TurretBaseBehaviour>().name}";
            upgradeText.text = "Power Upgrade";
            UpgradeButtonInteractable();
        }

        /// <summary>
        /// �����\���̎ˌ��Ԋu�A�b�v�O���[�h�{�^���̉������̓���
        /// </summary>
        public void ConstructableSpeedUpgradeButton()
        {
            // *******************************
            upgradeNumber = UpgradeNumber.Speed;
            upgradeStuff.text = $"{currentGridCell.transform.GetComponentInChildren<TurretBaseBehaviour>().name}";
            upgradeCoin.text = $"{currentGridCell.transform.GetComponentInChildren<TurretBaseBehaviour>().name}";
            upgradeText.text = "Speed Upgrade";
            UpgradeButtonInteractable();
        }

        /// <summary>
        /// �����\���̎ˌ��͈̓A�b�v�O���[�h�{�^���������̓���
        /// </summary>
        public void ConstructableRangeUpgradeButton()
        {
            // *******************************
            upgradeNumber = UpgradeNumber.Range;
            upgradeStuff.text = $"{currentGridCell.transform.GetComponentInChildren<TurretBaseBehaviour>().name}";
            upgradeCoin.text = $"{currentGridCell.transform.GetComponentInChildren<TurretBaseBehaviour>().name}";
            upgradeText.text = "Range Upgrade";
            UpgradeButtonInteractable();
        }

        /// <summary>
        /// �A�b�v�O���[�h�{�^��
        /// </summary>
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
            yield return new WaitUntil(() => upgradeScroll.verticalNormalizedPosition >= 1);
            upgradeScroll.normalizedPosition = position;
        }

        /// <summary>
        /// �A�b�v�O���[�h�{�^���̃C���^���N�g�𐧌�
        /// </summary>
        public void UpgradeButtonInteractable()
        {
            if (currentGridCell.transform.GetComponentInChildren<TurretBaseBehaviour>() != null)
            {
                int nextCoin = 0;
                int nextStuff = 0;
                switch (upgradeNumber)
                {
                    case UpgradeNumber.Power:
                        nextCoin = bsManager.money.Value - Mathf.CeilToInt(currentGridCell.transform.GetComponentInChildren<TurretBaseBehaviour>().firePowerCurrent);
                        nextStuff = bsManager.stuff.Value - Mathf.CeilToInt(currentGridCell.transform.GetComponentInChildren<TurretBaseBehaviour>().turretData.requireStuffBase);
                        break;
                    case UpgradeNumber.Speed:
                        nextCoin = bsManager.money.Value - Mathf.CeilToInt(currentGridCell.transform.GetComponentInChildren<TurretBaseBehaviour>().firePowerCurrent);
                        nextStuff = bsManager.stuff.Value - Mathf.CeilToInt(currentGridCell.transform.GetComponentInChildren<TurretBaseBehaviour>().firePowerCurrent);
                        break;
                    case UpgradeNumber.Range:
                        nextCoin = bsManager.money.Value - Mathf.CeilToInt(currentGridCell.transform.GetComponentInChildren<TurretBaseBehaviour>().firePowerCurrent);
                        nextStuff = bsManager.stuff.Value - Mathf.CeilToInt(currentGridCell.transform.GetComponentInChildren<TurretBaseBehaviour>().firePowerCurrent);
                        break;
                }
                if (nextCoin >= 0 && nextStuff >= 0)
                {
                    upgradeButton.interactable = true;
                }
                else
                {
                    upgradeButton.interactable = false;
                }
            }
        }

        /// <summary>
        /// ���p�{�^��
        /// </summary>
        public void CellButton()
        {

        }
    }
}