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

        struct SpawnTimeTable
        {
            public float time;
            public float level;
            public EnemyBehaviourBase.Enemies character;
        }

        static SpawnTimeTable[] wave ={
                new SpawnTimeTable { time = 1, level = 10, character = EnemyBehaviourBase.Enemies.Slime },
                new SpawnTimeTable { time = 5, level = 11, character = EnemyBehaviourBase.Enemies.Slime },
                new SpawnTimeTable { time = 10, level = 12, character = EnemyBehaviourBase.Enemies.Slime },
            };


        [SerializeField]
        GameObject startPoint;
        [SerializeField]
        GameObject goalPoint;

        protected override void Awake()
        {
            base.Awake();
            UpdateBattleState(BattleState.Battle);
        }

        private void Start()
        {

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

        private void HandleResult()
        {
            throw new NotImplementedException();
        }

        private void HandleBattleEnd()
        {
            throw new NotImplementedException();
        }

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

        private void HandleReady()
        {
            throw new NotImplementedException();
        }

        private void HandleInitilize()
        {
            throw new NotImplementedException();
        }
    }
}