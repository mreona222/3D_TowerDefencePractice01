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

        // スポーン地点
        public GameObject startPoint;
        // ゴール地点
        public GameObject goalPoint;
        [HideInInspector]
        // 敵の数
        public IntReactiveProperty enemyAmount;
        // 敵がゴールに到達した回数の限界
        public IntReactiveProperty enemyGoalLimit;


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
            for (int i = 0; i < wave.Length - 1; i++)
            {
                if (wave[i + 1].time < wave[i].time)
                {
                    Debug.LogError("敵の出現時間が間違っています。");
                }
            }

            // *****************************************
            UpdateBattleState(BattleState.Battle);
        }

        private void Start()
        {
            enemyAmount.Value = wave.Length;

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
                })
                .AddTo(this);
        }

        private void Update()
        {
            // ***********************************************************************
            if (UnityEngine.InputSystem.Keyboard.current.cKey.wasPressedThisFrame)
            {
                money.Value--;
            }
        }

        public void UpdateBattleState(BattleState newState)
        {
            currentState = newState;

            switch (currentState)
            {
                case BattleState.Initialize:
                    HandleInitilize();
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


        // --------------------------------------------------
        // Initialize
        // --------------------------------------------------
        private void HandleInitilize()
        {

        }


        // --------------------------------------------------
        // Ready
        // --------------------------------------------------
        private void HandleReady()
        {

        }


        // --------------------------------------------------
        // Battle
        // --------------------------------------------------
        private void HandleBattle()
        {
            StartCoroutine(TimeCount());

            IEnumerator TimeCount()
            {
                float currentTime = 0;
                float nextTime = wave[0].time;
                int next = 0;

                while (true)
                {
                    // バトル中のみ
                    if (currentState != BattleState.Battle) break;

                    // タイム更新
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

        }
    }
}