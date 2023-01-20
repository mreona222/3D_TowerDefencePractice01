using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefencePractice.Managers
{
    public class TitleInstanceManager : MonoBehaviour
    {
        private void Start()
        {
            GameManager.Instance.UpdateGameState(GameManager.GameState.Title);
        }

        public void OnClickGameStart()
        {
            SceneTransitionManager.Instance.SceneTrnasitionNormal("Menu");
        }

        public void OnClickQuitGame()
        {
            SceneTransitionManager.Instance.QuitGame();
        }
    }
}