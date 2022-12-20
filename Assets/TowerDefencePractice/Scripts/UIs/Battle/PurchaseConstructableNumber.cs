using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefencePractice.UIs
{
    public class PurchaseConstructableNumber : MonoBehaviour
    {
        public void SelectConstructableNumber()
        {
            transform.root.gameObject.GetComponent<BattleUIController>().purchaseConstructableNumber = transform.GetSiblingIndex();
            transform.root.gameObject.GetComponent<BattleUIController>().NewConstructableStatus(transform.GetSiblingIndex());
        }
    }
}