using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TowerDefencePractice.Managers;

namespace TowerDefencePractice.UIs
{
    public class BattleWave : MonoBehaviour
    {
        [HideInInspector]
        public MenuInstanceManager menuInstanceManager;

        [SerializeField]
        Text battleNameText;
        [SerializeField]
        Text rankText;

        // Start is called before the first frame update
        void Start()
        {
            battleNameText.text =
                ((MenuInstanceManager.BattleScene)System.Enum.ToObject(typeof(MenuInstanceManager.BattleScene), transform.GetSiblingIndex())).ToString();
            switch(PlayerPrefs.GetFloat(((MenuInstanceManager.BattleScene)System.Enum.ToObject(typeof(MenuInstanceManager.BattleScene), transform.GetSiblingIndex())).ToString(), 0))
            {
                case float n when n > 1.0f:
                    rankText.text = "";
                    break;
                case float n when n < 0.01f:
                    rankText.text = "";
                    break;
                case float n when n <= 0.3f:
                    rankText.text = "C";
                    break;
                case float n when n <= 0.5f:
                    rankText.text = "B";
                    break;
                case float n when n <= 0.9f:
                    rankText.text = "A";
                    break;
                case float n when n <= 1.0f:
                    rankText.text = "S";
                    break;
            }
        }

        public void OnClickBattleWave()
        {
            menuInstanceManager.challengeWave = transform.GetSiblingIndex();
            SceneTransitionManager.Instance.SceneTrnasitionNormal(((MenuInstanceManager.BattleScene)System.Enum.ToObject(typeof(MenuInstanceManager.BattleScene), transform.GetSiblingIndex())).ToString());
        }
    }
}