using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefencePractice.Managers
{
    public class MenuInstanceManager : MonoBehaviour
    {
        [SerializeField]
        GameObject battleWave;
        [SerializeField]
        Transform contextTransform;

        public int challengeWave;

        public enum BattleScene
        {
            Battle01,
        }

        private void Start()
        {
            GameManager.Instance.UpdateGameState(GameManager.GameState.Menu);

            for(int i = 0; i < System.Enum.GetValues(typeof(BattleScene)).Length; i++)
            {
                GameObject wave = Instantiate(battleWave, contextTransform);
                wave.GetComponent<UIs.BattleWave>().menuInstanceManager = this;
            }
        }

        public void OnClickBeginWave()
        {
            SceneTransitionManager.Instance.SceneTrnasitionNormal(((BattleScene)System.Enum.ToObject(typeof(BattleScene), challengeWave)).ToString());
        }

        public void OnClickReturn2Title()
        {
            SceneTransitionManager.Instance.SceneTrnasitionNormal("Title");
        }
    }
}