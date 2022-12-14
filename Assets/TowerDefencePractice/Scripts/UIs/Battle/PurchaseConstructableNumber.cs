using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefencePractice.UIs
{
    public class PurchaseConstructableNumber : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        public void SelectConstructableNumber()
        {
            transform.root.gameObject.GetComponent<BattleUIController>().purchaseConstructableNumber = transform.GetSiblingIndex();
        }
    }
}