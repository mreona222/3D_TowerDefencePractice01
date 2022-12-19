using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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

        [System.Serializable]
        class SpawnTimeTable
        {
            public float time;
            public float level;
            public EnemyBehaviourBase.Enemies character;
        }

        [SerializeField]
        SpawnTimeTable[] wave ={
                new SpawnTimeTable { time = 1, level = 1, character = EnemyBehaviourBase.Enemies.Slime },
                new SpawnTimeTable { time = 5, level = 30, character = EnemyBehaviourBase.Enemies.Slime },
                new SpawnTimeTable { time = 10, level = 100, character = EnemyBehaviourBase.Enemies.Slime },
            };


        public GameObject startPoint;

        public GameObject goalPoint;

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