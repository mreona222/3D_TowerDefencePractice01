using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefencePractice.UIs
{
    public class BattleWave : MonoBehaviour
    {
        [HideInInspector]
        public Managers.MenuInstanceManager menuInstanceManager;

        // Start is called before the first frame update
        void Start()
        {
            GetComponentInChildren<UnityEngine.UI.Text>().text =
                ((Managers.MenuInstanceManager.BattleScene)System.Enum.ToObject(typeof(Managers.MenuInstanceManager.BattleScene), transform.GetSiblingIndex())).ToString(); ;
            Debug.Log(transform.GetSiblingIndex());
        }

        public void OnClickBattleWave()
        {
            menuInstanceManager.challengeWave = transform.GetSiblingIndex();
        }
    }
}