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

        [SerializeField]
        AudioSource uiSource;
        [SerializeField]
        AudioClip moneyClip;
        [SerializeField]
        AudioClip constructClip;


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
                // ���z���ƍw����
                if (!uiSource.isPlaying)
                {
                    uiSource.PlayOneShot(constructClip);
                    uiSource.PlayOneShot(moneyClip);
                }

                // �����W�̕\��
                currentGridCell.collider.GetComponentInChildren<TurretRangeColliderBase>().RangeEnable();

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
            TurretBehaviourBase currentTurretBehaviour = currentGridCell.transform.GetComponentInChildren<TurretBehaviourBase>();
            TurretGradeUpBase currentTurretGradeUp = currentGridCell.transform.GetComponentInChildren<TurretGradeUpBase>();

            upgradeConstructableNameInstance.text = currentTurretBehaviour.turretData.constructableName;

            upgradeConstructableIconInstance.sprite = currentTurretBehaviour.turretData.constructableIcon;

            //upgradeConstructableStuffInstance.text = $"{currentTurretBehaviour.turretData.fireRateMax:f2}";

            //upgradeConstructableCostInstance.text = $"{currentTurretBehaviour.turretData.fireRateMax:f2}";

            upgradeConstructablePowerInstance.text = $"{currentTurretBehaviour.firePowerCurrent:f2}";
            upgradeNextConstructablePowerInstance.text = $"{currentTurretGradeUp.FirePowerCalculate(currentTurretBehaviour.firePowerCurrentLevel + 1):f2}";

            upgradeConstructableSpeedInstance.text = $"{currentTurretBehaviour.fireRateCurrent:f2}";
            upgradeNextConstructableSpeedInstance.text = $"{currentTurretGradeUp.FireRateCalculate(currentTurretBehaviour.fireRateCurrent + 1):f2}";

            
            upgradeConstructableDPSInstance.text = $"{currentTurretBehaviour.firePowerCurrent / currentTurretBehaviour.fireRateCurrent:f2}";
            switch (upgradeNumber)
            {
                case UpgradeNumber.Power:
                    upgradeNextConstructableDPSInstance.text =
                        $"{currentTurretGradeUp.FirePowerCalculate(currentTurretBehaviour.firePowerCurrentLevel + 1) / currentTurretGradeUp.FireRateCalculate(currentTurretBehaviour.fireRateCurrentLevel):f2}";
                    break;
                case UpgradeNumber.Speed:
                    upgradeNextConstructableDPSInstance.text =
                        $"{currentTurretGradeUp.FirePowerCalculate(currentTurretBehaviour.firePowerCurrentLevel) / currentTurretGradeUp.FireRateCalculate(currentTurretBehaviour.fireRateCurrentLevel + 1):f2}";
                    break;
                case UpgradeNumber.Range:
                    upgradeNextConstructableDPSInstance.text =
                        $"{currentTurretGradeUp.FirePowerCalculate(currentTurretBehaviour.firePowerCurrentLevel) / currentTurretGradeUp.FireRateCalculate(currentTurretBehaviour.fireRateCurrentLevel):f2}";
                    break;
            }

            upgradeConstructableRangeInstance.text = $"{currentTurretBehaviour.fireRangeCurrent:f2}";
            upgradeNextConstructableRangeInstance.text = $"{currentTurretGradeUp.FireRangeCalculate(currentTurretBehaviour.fireRangeCurrentLevel + 1):f2}";
        }

        /// <summary>
        /// �����\���̉ἨA�b�v�O���[�h�{�^���̉������̓���
        /// </summary>
        public void ConstructablePowerUpgradeButton()
        {
            upgradeNumber = UpgradeNumber.Power;
            upgradeStuff.text = $"{0}";
            upgradeCoin.text = $"{Mathf.CeilToInt(currentGridCell.transform.GetComponentInChildren<TurretBehaviourBase>().firePowerUpgradeCoin)}";
            upgradeText.text = "Power Upgrade";
            UpgradeButtonInteractable();
            Vector2 scrollPosition = upgradeScroll.normalizedPosition;
            UpgradeConstructablePanelInitialize();
            StartCoroutine(ScrollPosition(scrollPosition));
        }

        /// <summary>
        /// �����\���̎ˌ��Ԋu�A�b�v�O���[�h�{�^���̉������̓���
        /// </summary>
        public void ConstructableSpeedUpgradeButton()
        {
            upgradeNumber = UpgradeNumber.Speed;
            upgradeStuff.text = $"{0}";
            upgradeCoin.text = $"{Mathf.CeilToInt(currentGridCell.transform.GetComponentInChildren<TurretBehaviourBase>().fireRateUpgradeCoin)}";
            upgradeText.text = "Speed Upgrade";
            UpgradeButtonInteractable();
            Vector2 scrollPosition = upgradeScroll.normalizedPosition;
            UpgradeConstructablePanelInitialize();
            StartCoroutine(ScrollPosition(scrollPosition));
        }

        /// <summary>
        /// �����\���̎ˌ��͈̓A�b�v�O���[�h�{�^���������̓���
        /// </summary>
        public void ConstructableRangeUpgradeButton()
        {
            upgradeNumber = UpgradeNumber.Range;
            upgradeStuff.text = $"{0}";
            upgradeCoin.text = $"{Mathf.CeilToInt(currentGridCell.transform.GetComponentInChildren<TurretBehaviourBase>().fireRangeUpgradeCoin)}";
            upgradeText.text = "Range Upgrade";
            UpgradeButtonInteractable();
            Vector2 scrollPosition = upgradeScroll.normalizedPosition;
            UpgradeConstructablePanelInitialize();
            StartCoroutine(ScrollPosition(scrollPosition));
        }

        /// <summary>
        /// �A�b�v�O���[�h�{�^��
        /// </summary>
        public void UpgradeButton()
        {
            Vector2 scrollPosition = upgradeScroll.normalizedPosition;

            switch (upgradeNumber)
            {
                case UpgradeNumber.Power:
                    bsManager.money.Value -= Mathf.CeilToInt(currentGridCell.transform.GetComponentInChildren<TurretBehaviourBase>().firePowerUpgradeCoin);
                    bsManager.stuff.Value -= 0;
                    currentGridCell.transform.GetComponentInChildren<TurretGradeUpBase>().FirePowerGradeUp();
                    upgradeStuff.text = $"{0}";
                    upgradeCoin.text = $"{Mathf.CeilToInt(currentGridCell.transform.GetComponentInChildren<TurretBehaviourBase>().firePowerUpgradeCoin)}";
                    break;
                case UpgradeNumber.Speed:
                    bsManager.money.Value -= Mathf.CeilToInt(currentGridCell.transform.GetComponentInChildren<TurretBehaviourBase>().fireRateUpgradeCoin);
                    bsManager.stuff.Value -= 0;
                    currentGridCell.transform.GetComponentInChildren<TurretGradeUpBase>().FireRateGradeUp();
                    upgradeStuff.text = $"{0}";
                    upgradeCoin.text = $"{Mathf.CeilToInt(currentGridCell.transform.GetComponentInChildren<TurretBehaviourBase>().fireRateUpgradeCoin)}";
                    break;
                case UpgradeNumber.Range:
                    bsManager.money.Value -= Mathf.CeilToInt(currentGridCell.transform.GetComponentInChildren<TurretBehaviourBase>().fireRangeUpgradeCoin);
                    bsManager.stuff.Value -= 0;
                    currentGridCell.transform.GetComponentInChildren<TurretGradeUpBase>().FireRangeGradeUp();
                    upgradeStuff.text = $"{0}";
                    upgradeCoin.text = $"{Mathf.CeilToInt(currentGridCell.transform.GetComponentInChildren<TurretBehaviourBase>().fireRangeUpgradeCoin)}";
                    break;
            }

            // ���z���ƍw����
            if (!uiSource.isPlaying)
            {
                uiSource.PlayOneShot(constructClip);
                uiSource.PlayOneShot(moneyClip);
            }

            UpgradeConstructablePanelInitialize();
            UpgradeButtonInteractable();

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
            if (currentGridCell.transform.GetComponentInChildren<TurretBehaviourBase>() != null)
            {
                TurretBehaviourBase currentGridTurret = currentGridCell.transform.GetComponentInChildren<TurretBehaviourBase>();
                int nextCoin = 0;
                int nextStuff = 0;
                bool maxLevel = false;
                switch (upgradeNumber)
                {
                    case UpgradeNumber.Power:
                        nextCoin = bsManager.money.Value - Mathf.CeilToInt(currentGridTurret.firePowerUpgradeCoin);
                        nextStuff = bsManager.stuff.Value - 0;
                        if (currentGridTurret.turretData.firePowerMaxLevel <= currentGridTurret.firePowerCurrentLevel)
                        {
                            maxLevel = true;
                        }
                        break;

                    case UpgradeNumber.Speed:
                        nextCoin = bsManager.money.Value - Mathf.CeilToInt(currentGridTurret.fireRateUpgradeCoin);
                        nextStuff = bsManager.stuff.Value - 0;
                        if (currentGridTurret.turretData.fireRateMaxLevel <= currentGridTurret.fireRateCurrentLevel)
                        {
                            maxLevel = true;
                        }
                        break;

                    case UpgradeNumber.Range:
                        nextCoin = bsManager.money.Value - Mathf.CeilToInt(currentGridTurret.fireRangeUpgradeCoin);
                        nextStuff = bsManager.stuff.Value - 0;
                        if (currentGridTurret.turretData.fireRangeMaxLevel <= currentGridTurret.fireRangeCurrentLevel)
                        {
                            maxLevel = true;
                        }
                        break;
                }
                if (nextCoin >= 0 && nextStuff >= 0 && !maxLevel)
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
            TurretBehaviourBase tbb = currentGridCell.transform.GetComponentInChildren<TurretBehaviourBase>();
            TurretGradeUpBase tgb = currentGridCell.transform.GetComponentInChildren<TurretGradeUpBase>();
            float cellMoney = 0;
            for (int i = 0; i < tbb.firePowerCurrentLevel; i++)
            {
                cellMoney += tgb.FirePowerUpgradeCoinCalcurate(i);
            }
            for(int i = 0; i < tbb.fireRateCurrentLevel; i++)
            {
                cellMoney += tgb.FireRateUpgradeCoinCalcurate(i);
            }
            for(int i = 0; i < tbb.fireRangeCurrentLevel; i++)
            {
                cellMoney += tgb.FireRangeUpgradeCoinCalcurate(i);
            }
            bsManager.money.Value += Mathf.FloorToInt((tbb.turretData.requireCoinBase + cellMoney) * 0.6f);
            bsManager.stuff.Value += tbb.turretData.requireStuffBase;
            currentGridCell.collider.GetComponent<GridCellController>().constructableExist = false;
            tbb.CellTurret();
        }


        // -----------------------------------------------------------
        // �Q�[���I����
        // -----------------------------------------------------------

        public void OnClickBack2Menu()
        {
            SceneTransitionManager.Instance.SceneTrnasitionNormal("Menu");
        }

        public void OnClickRetry()
        {
            SceneTransitionManager.Instance.ReLolad();
        }

        public void OnClickNextStage()
        {
            SceneTransitionManager.Instance.SceneTrnasitionNormal(bsManager.nextScene);
        }
    }
}