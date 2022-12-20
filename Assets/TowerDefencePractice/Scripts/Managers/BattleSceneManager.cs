using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

using Utilities;

using TowerDefencePractice.Character.Enemies;
using TowerDefencePractice.Spawner;


namespace TowerDefencePractice.Managers
{
    public class BattleSceneManager : SingletonMonoBehaviour<BattleSceneManager>
    {
        public enum BattleState
        {
            Initialize,
            Ready,
            Battle,
            BattleEnd,
            Result,
        }

        static BattleState currentState;

        public GameObject startPoint;

        public GameObject goalPoint;

        [System.Serializable]
        class SpawnTimeTable
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
            public override void OnInspectorGUI()
            {
                base.OnInspectorGUI();

                BattleSceneManager bsManager = target as BattleSceneManager;

                EditorGUIUtility.labelWidth = 50;
                for (int i = 0; i < bsManager.wave.Length; i++)
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
            }
        }
#endif

        protected override void Awake()
        {
            base.Awake();

            for (int i = 0; i < wave.Length - 1; i++)
            {
                if (wave[i + 1].time < wave[i].time)
                {
                    Debug.LogError("“G‚ÌoŒ»ŽžŠÔ‚ªŠÔˆá‚Á‚Ä‚¢‚Ü‚·B");
                }
            }

            // *****************************************
            UpdateBattleState(BattleState.Battle);
        }

        private void Start()
        {

        }

        private void Update()
        {
            // ***********************************************************************
            if (UnityEngine.InputSystem.Keyboard.current.cKey.wasPressedThisFrame)
            {
                UpdateBattleState(BattleState.BattleEnd);
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
                    if (currentState != BattleState.Battle) break;

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
            //StartCoroutine(TimeSlowly());

            //IEnumerator TimeSlowly()
            //{
            //    while (true)
            //    {
            //        Time.timeScale *= 0.75f;
            //        yield return new WaitForSeconds(0.1f);
            //        if (Time.timeScale < 0.3f)
            //        {
            //            Time.timeScale = 0;
            //            break;
            //        }
            //    }

            //    UpdateBattleState(BattleState.Result);
            //}
        }



        // --------------------------------------------------
        // Result
        // --------------------------------------------------
        private void HandleResult()
        {

        }
    }
}