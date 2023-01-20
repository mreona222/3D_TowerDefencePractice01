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

        // �X�|�[���n�_
        public GameObject startPoint;
        // �S�[���n�_
        public GameObject goalPoint;
        [HideInInspector]
        // �G�̐�
        public IntReactiveProperty enemyAmount;
        // �G���S�[���ɓ��B�����񐔂̌��E
        public IntReactiveProperty enemyGoalLimit;
        // �S�[���̗̑�
        [SerializeField]
        private Slider goalHP;

        [SerializeField]
        Text timeText;

        // ������
        public IntReactiveProperty money;
        [SerializeField]
        private Text moneyText;

        // �l��
        public IntReactiveProperty stuff;
        [SerializeField]
        private Text stuffText;

        // �Q�[�����f�B
        [SerializeField]
        GameObject gameReadyPanel;

        // �Q�[�����U���g
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
        public class SpawnTimeTable
        {
            public float time;
            public float level;
            public EnemyBehaviourBase.Enemies character;
        }

        [SerializeField]
        SpawnTimeTable[] wave;

#if UNITY_EDITOR
        [CustomEditor(typeof(BattleSceneManager))]
        public class BattleSceneManagerInspector : Editor
        {
            int size = 0;

            public override void OnInspectorGUI()
            {
                base.OnInspectorGUI();

                serializedObject.Update();

                var wave = serializedObject.FindProperty("wave");

                BattleSceneManager bsManager = target as BattleSceneManager;

                EditorGUILayout.Space();

                size = wave.arraySize;

                size = EditorGUILayout.DelayedIntField("Size", size);

                if (size != wave.arraySize)
                {
                    wave.arraySize = size;

                    serializedObject.ApplyModifiedProperties();
                    serializedObject.Update();
                }
                else
                {
                    EditorGUIUtility.labelWidth = 50;

                    EditorGUI.BeginChangeCheck();
                    for (int i = 0; i < wave.arraySize; i++)
                    {
                        using (new EditorGUILayout.HorizontalScope())
                        {
                            bsManager.wave[i].time = EditorGUILayout.DelayedFloatField("Time", bsManager.wave[i].time, GUILayout.Width(120));
                            GUILayout.FlexibleSpace();
                            bsManager.wave[i].level = EditorGUILayout.DelayedFloatField("Level", bsManager.wave[i].level, GUILayout.Width(120));
                            GUILayout.FlexibleSpace();
                            bsManager.wave[i].character = (EnemyBehaviourBase.Enemies)EditorGUILayout.EnumPopup("Chara", (EnemyBehaviourBase.Enemies)bsManager.wave[i].character, GUILayout.Width(200));
                            GUILayout.FlexibleSpace();
                        }
                    }
                    if (EditorGUI.EndChangeCheck())
                    {
                        var scene = SceneManager.GetActiveScene();
                        EditorSceneManager.MarkSceneDirty(scene);
                        EditorUtility.SetDirty(target);
                    }

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
            for (int i = 0; i < wave.Length - 1; i++)
            {
                if (wave[i + 1].time < wave[i].time)
                {
                    Debug.LogError("�G�̏o�����Ԃ��Ԉ���Ă��܂��B");
                }
            }

            GameManager.Instance.UpdateGameState(GameManager.GameState.Battle);
            UpdateBattleState(BattleState.Initialize);

            enemyAmount.Value = wave.Length;

            // �������ɕύX���������ꍇ
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

            // �l���ɕύX���������ꍇ
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

            // �E�F�[�u���I�������
            enemyAmount
                .Where((x) => x <= 0 && currentState == BattleState.Battle)
                .Subscribe((x) =>
                {
                    UpdateBattleState(BattleState.BattleEnd);
                })
                .AddTo(this);

            // �G�̃S�[�����B�񐔂�����𒴂�����
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
            
            // �G���S�[���ɓ��B������
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


        private void Update()
        {
            // *************
            if (UnityEngine.InputSystem.Keyboard.current.cKey.wasPressedThisFrame)
            {
                UpdateBattleState(BattleState.BattleEnd);
            }
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
                float nextTime = wave[0].time;
                int next = 0;

                while (true)
                {
                    // �o�g�����̂�
                    if (currentState != BattleState.Battle) break;

                    // �^�C���X�V
                    timeText.text = $"{Mathf.FloorToInt(currentTime / 60.0f):D2}:{ Mathf.FloorToInt(currentTime % 60.0f):D2}";

                    if (next < wave.Length)
                    {
                        if (currentTime > nextTime)
                        {
                            startPoint.GetComponent<SpawnerBehaviour>().SpawnCharacter(wave[next].level, wave[next].character, goalPoint);
                            next++;
                            if (next < wave.Length)
                            {
                                nextTime = wave[next].time;
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
            // Battle�V�[���̉����~�߂�
            AudioSource[] sound = GameObject.FindObjectsOfType<AudioSource>();
            for(int i = 0; i < sound.Length; i++)
            {
                sound[i].Stop();
            }

            // �Q�[�����U���g�\��
            gameResultPanel.SetActive(true);

            // ����
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

                gameRank.GetComponentInChildren<Text>().text = rank;
                nextStage.SetActive(true);
            }
            // �s�k
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