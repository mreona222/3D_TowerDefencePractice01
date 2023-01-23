using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UniRx;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif

using Utilities;

using TowerDefencePractice.Character.Enemies;
using TowerDefencePractice.Spawner;
using TowerDefencePractice.UIs;


namespace TowerDefencePractice.Managers
{
    public class BattleSceneManager : MonoBehaviour
    {
        public enum BattleState
        {
            Initialize,
            Ready,
            Battle,
            BattleEnd,
            Result,
        }

        [SerializeField]
        BattleState currentState;

        [SerializeField]
        BattleUIController buCon;

        [SerializeField]
        GameObject playerInput;
        GameObject playerInputInstance;

        // スポーン地点
        public GameObject[] startPoint;
        // ゴール地点
        public GameObject goalPoint;
        [HideInInspector]
        // 敵の数
        public IntReactiveProperty enemyAmount;
        // 敵がゴールに到達した回数の限界
        public IntReactiveProperty enemyGoalLimit;
        // ゴールの体力
        [SerializeField]
        private Slider goalHP;

        [SerializeField]
        Text timeText;

        // 所持金
        public IntReactiveProperty money;
        [SerializeField]
        private Text moneyText;

        // 人員
        public IntReactiveProperty stuff;
        [SerializeField]
        private Text stuffText;

        // ゲームレディ
        [SerializeField]
        GameObject gameReadyPanel;

        // ゲームリザルト
        [SerializeField]
        private GameObject gameResultPanel;
        [SerializeField]
        private Text gameResultText;
        [SerializeField]
        private GameObject gameRank;
        [SerializeField]
        private GameObject nextStage;

        [SerializeField]
        public string nextScene;


        [System.Serializable]
        public class SpawnCharacterInfo
        {
            public float time;
            public float level;
            public EnemyBehaviourBase.Enemies character;
        }

        [System.Serializable]
        public class Spawner
        {
            [SerializeField]
            public List<SpawnCharacterInfo> info = new List<SpawnCharacterInfo> { };
        }

        [SerializeField]
        public List<Spawner> spawner = new List<Spawner> { };

#if UNITY_EDITOR
        [CustomEditor(typeof(BattleSceneManager))]
        public class BattleSceneManagerInspector : Editor
        {
            int spawnerSize = 0;
            int[] infoSize = { };

            public override void OnInspectorGUI()
            {
                base.OnInspectorGUI();

                serializedObject.Update();

                EditorGUI.BeginChangeCheck();

                BattleSceneManager bsManager = target as BattleSceneManager;

                EditorGUILayout.Space();

                // スポーンの数を取得
                spawnerSize = bsManager.spawner.Count;
                spawnerSize = EditorGUILayout.DelayedIntField("SpawnerSize", spawnerSize);

                if (spawnerSize != bsManager.spawner.Count)
                {
                    
                    // スポーンの数だけspawner配列を用意
                    while (spawnerSize != bsManager.spawner.Count)
                    {
                        if (spawnerSize > bsManager.spawner.Count)
                        {
                            bsManager.spawner.Add(new Spawner { });
                        }
                        else if (spawnerSize < bsManager.spawner.Count)
                        {
                            bsManager.spawner.RemoveAt(bsManager.spawner.Count - 1);
                        }
                    }
                }
                else
                {
                    Array.Resize<int>(ref infoSize, spawnerSize);

                    for (int i = 0; i < spawnerSize; i++)
                    {
                        EditorGUIUtility.labelWidth = 0;

                        EditorGUILayout.Space();

                        infoSize[i] = bsManager.spawner[i].info.Count;
                        infoSize[i] = EditorGUILayout.DelayedIntField("InfoSize", infoSize[i]);

                        if (infoSize[i] != bsManager.spawner[i].info.Count)
                        {
                            while (infoSize[i] != bsManager.spawner[i].info.Count)
                            {
                                if (infoSize[i] > bsManager.spawner[i].info.Count)
                                {
                                    bsManager.spawner[i].info.Add(new SpawnCharacterInfo { });
                                }
                                else
                                {
                                    bsManager.spawner[i].info.RemoveAt(bsManager.spawner[i].info.Count - 1);
                                }
                            }
                        }
                        else
                        {
                            EditorGUIUtility.labelWidth = 50;

                            for (int j = 0; j < infoSize[i]; j++)
                            {
                                using (new EditorGUILayout.HorizontalScope())
                                {
                                    bsManager.spawner[i].info[j].time = EditorGUILayout.DelayedFloatField("Time", bsManager.spawner[i].info[j].time, GUILayout.Width(120));
                                    GUILayout.FlexibleSpace();
                                    bsManager.spawner[i].info[j].level = EditorGUILayout.DelayedFloatField("Level", bsManager.spawner[i].info[j].level, GUILayout.Width(120));
                                    GUILayout.FlexibleSpace();
                                    bsManager.spawner[i].info[j].character
                                        = (EnemyBehaviourBase.Enemies)EditorGUILayout.EnumPopup("Chara", (EnemyBehaviourBase.Enemies)bsManager.spawner[i].info[j].character, GUILayout.Width(200));
                                    GUILayout.FlexibleSpace();
                                }
                            }
                        }
                    }
                }

                if (EditorGUI.EndChangeCheck())
                {
                    var scene = SceneManager.GetActiveScene();
                    EditorSceneManager.MarkSceneDirty(scene);
                    EditorUtility.SetDirty(target);
                }
                serializedObject.ApplyModifiedProperties();
            }
        }
#endif


        private void Awake()
        {

        }

        private void Start()
        {
            for(int i = 0; i < spawner.Count; i++)
            {
                for (int j = 0; j < spawner[i].info.Count - 1; j++)
                {
                    if (spawner[i].info[j + 1].time < spawner[i].info[j].time)
                    {
                        Debug.LogError("敵の出現時間が間違っています。");
                    }
                }
            }

            GameManager.Instance.UpdateGameState(GameManager.GameState.Battle);
            UpdateBattleState(BattleState.Initialize);

            for(int i = 0; i < spawner.Count; i++)
            {
                for(int j = 0; j < spawner[i].info.Count; j++)
                {
                    enemyAmount.Value++;
                }
            }

            // 所持金に変更があった場合
            money
                .Subscribe((x) =>
                {
                    moneyText.text = $"{x}";
                    buCon.PurchaseButtonInteractable();
                    if (buCon.currentGridCell.collider != null)
                    {
                        buCon.UpgradeButtonInteractable();
                    }
                })
                .AddTo(this);

            // 人員に変更があった場合
            stuff
                .Subscribe((x) =>
                {
                    stuffText.text = $"{x}";
                    buCon.PurchaseButtonInteractable();
                    if (buCon.currentGridCell.collider != null)
                    {
                        buCon.UpgradeButtonInteractable();
                    }
                })
                .AddTo(this);

            // ウェーブが終わったら
            enemyAmount
                .Where((x) => x <= 0 && currentState == BattleState.Battle)
                .Subscribe((x) =>
                {
                    UpdateBattleState(BattleState.BattleEnd);
                })
                .AddTo(this);

            // 敵のゴール到達回数が上限を超えたら
            enemyGoalLimit
                .Where((x) => x <= 0 && currentState == BattleState.Battle)
                .Subscribe((x) =>
                {
                    UpdateBattleState(BattleState.BattleEnd);
                    goalPoint.GetComponent<AudioSource>().PlayOneShot(goalPoint.GetComponent<AudioSource>().clip);
                })
                .AddTo(this);

            goalHP.maxValue = enemyGoalLimit.Value;
            goalHP.transform.rotation = Camera.main.transform.rotation;
            
            // 敵がゴールに到達したら
            enemyGoalLimit
                .Subscribe((x) =>
                {
                    goalHP.value = x;
                })
                .AddTo(this);
        }


        private void OnEnterBattleState(BattleState newState)
        {
            switch (newState)
            {
                case BattleState.Initialize:
                    HandleInitialize();
                    break;
                case BattleState.Ready:
                    HandleReady();
                    break;
                case BattleState.Battle:
                    HandleBattle();
                    break;
                case BattleState.BattleEnd:
                    HandleBattleEnd();
                    break;
                case BattleState.Result:
                    HandleResult();
                    break;
            }
        }

        private void OnExitBattleState(BattleState prevState)
        {
            switch (prevState)
            {
                case BattleState.Initialize:
                    break;
                case BattleState.Ready:
                    break;
                case BattleState.Battle:
                    OnExitBattle();
                    break;
                case BattleState.BattleEnd:
                    break;
                case BattleState.Result:
                    break;
            }
        }

        public void UpdateBattleState(BattleState newState)
        {
            OnExitBattleState(currentState);
            currentState = newState;
            OnEnterBattleState(currentState);
        }


        // --------------------------------------------------
        // Initialize
        // --------------------------------------------------
        private void HandleInitialize()
        {
            StartCoroutine(WaitInitialize());
        }

        IEnumerator WaitInitialize()
        {
            Time.timeScale = 1.0f;
            yield return new WaitForSeconds(1.0f);
            UpdateBattleState(BattleState.Ready);
        }


        // --------------------------------------------------
        // Ready
        // --------------------------------------------------
        private void HandleReady()
        {
            gameReadyPanel.SetActive(true);
            gameReadyPanel.GetComponentsInChildren<Text>()[0].text = "Ready?";
            StartCoroutine(CountDown());
        }

        IEnumerator CountDown()
        {
            float time = 0;
            while (true)
            {
                if (time > 3.0f)
                {
                    UpdateBattleState(BattleState.Battle);
                    gameReadyPanel.GetComponentsInChildren<Text>()[0].text = "GO!!";
                    StartCoroutine(HideReady());
                    break;
                }
                yield return null;
                time += Time.deltaTime;
            }
        }

        IEnumerator HideReady()
        {
            yield return new WaitForSeconds(1.0f);
            gameReadyPanel.SetActive(false);
        }

        // --------------------------------------------------
        // Battle
        // --------------------------------------------------
        private void HandleBattle()
        {
            PlayerInputProvider();

            StartCoroutine(TimeCount());

            IEnumerator TimeCount()
            {
                float currentTime = 0;
                float[] nextTime = new float[spawner.Count];
                int[] next = new int[spawner.Count];

                for (int i = 0; i < spawner.Count; i++)
                {
                    if (spawner[i].info != null)
                    {
                        nextTime[i] = spawner[i].info[0].time;
                    }
                }

                while (true)
                {
                    // バトル中のみ
                    if (currentState != BattleState.Battle) break;

                    // タイム更新
                    timeText.text = $"{Mathf.FloorToInt(currentTime / 60.0f):D2}:{ Mathf.FloorToInt(currentTime % 60.0f):D2}";

                    for(int i = 0; i < spawner.Count; i++)
                    {
                        if (next[i] < spawner[i].info.Count)
                        {
                            if (currentTime > nextTime[i])
                            {
                                startPoint[i].GetComponent<SpawnerBehaviour>().SpawnCharacter(spawner[i].info[next[i]].level, spawner[i].info[next[i]].character, goalPoint);
                                next[i]++;
                                if (next[i] < spawner[i].info.Count)
                                {
                                    nextTime[i] = spawner[i].info[next[i]].time;
                                }
                            }
                        }

                    }

                    yield return null;
                    currentTime += Time.deltaTime;
                }
            }
        }

        private void OnExitBattle()
        {
            Destroy(playerInputInstance);
        }

        private void PlayerInputProvider()
        {
            playerInputInstance = Instantiate(playerInput);
            playerInputInstance.GetComponent<TowerDefencePractice.Inputs.GridChoose>().battleUICon = buCon;
        }



        // --------------------------------------------------
        // BattleEnd
        // --------------------------------------------------
        private void HandleBattleEnd()
        {
            StartCoroutine(TimeSlowly());

            IEnumerator TimeSlowly()
            {
                while (true)
                {
                    Time.timeScale *= 0.90f;
                    yield return new WaitForSeconds(0.1f);
                    if (Time.timeScale < 0.3f)
                    {
                        Time.timeScale = 0;
                        break;
                    }
                }

                UpdateBattleState(BattleState.Result);
            }
        }




        // --------------------------------------------------
        // Result
        // --------------------------------------------------
        private void HandleResult()
        {
            // Battleシーンの音を止める
            AudioSource[] sound = GameObject.FindObjectsOfType<AudioSource>();
            for(int i = 0; i < sound.Length; i++)
            {
                sound[i].Stop();
            }

            // ゲームリザルト表示
            gameResultPanel.SetActive(true);

            // 勝利
            if (enemyGoalLimit.Value > 0)
            {
                AudioManager.Instance.GameClear();
                gameResultText.text = "Game Clear";
                gameRank.SetActive(true);


                string rank = (enemyGoalLimit.Value / goalHP.maxValue) switch
                {
                    float n when (n > 1.0f) => "",
                    float n when (n <= 0.3f) => "Rank : C",
                    float n when (n <= 0.5f) => "Rank : B",
                    float n when (n <= 0.9f) => "Rank : A",
                    float n when (n <= 1.0f) => "Rank : S",
                    _ => "",
                };

                if (PlayerPrefs.GetFloat(((MenuInstanceManager.BattleScene)Enum.Parse(typeof(MenuInstanceManager.BattleScene), SceneManager.GetActiveScene().name, true)).ToString(), 0f)
                    < enemyGoalLimit.Value / goalHP.maxValue)
                {
                    PlayerPrefs.SetFloat(((MenuInstanceManager.BattleScene)Enum.Parse(typeof(MenuInstanceManager.BattleScene), SceneManager.GetActiveScene().name, true)).ToString(),
                        enemyGoalLimit.Value / goalHP.maxValue);
                }


                gameRank.GetComponentInChildren<Text>().text = rank;
                nextStage.SetActive(true);
                if (nextScene == "")
                {
                    nextStage.GetComponent<Button>().interactable = false;
                }
            }
            // 敗北
            else
            {
                AudioManager.Instance.GameFailed();
                gameResultText.text = "Game Over";
                gameRank.SetActive(false);
                nextStage.SetActive(false);
            }
        }
    }
}