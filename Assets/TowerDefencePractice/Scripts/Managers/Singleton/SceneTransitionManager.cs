using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TowerDefencePractice.Managers
{
    public class SceneTransitionManager : ManagerBase<SceneTransitionManager>
    {
        public void SceneTrnasitionNormal(string sceneName)
        {
            SceneManager.LoadSceneAsync(sceneName);
        }

        public void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
		Application.OpenURL("http://www.yahoo.co.jp/");
#else
		Application.Quit();
#endif
        }

        public void ReLolad()
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        }
    }
}