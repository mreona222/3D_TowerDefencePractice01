using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using Utilities.States;
using TowerDefencePractice.Damages;
using TowerDefencePractice.Managers;

namespace TowerDefencePractice.Character.Enemies
{
    public abstract class EnemyBehaviourBase : StateMachineBase<EnemyBehaviourBase>, IDamageApplicable
    {
        public enum Enemies
        {
            Slime,
            ShellSilme,

        }


        public CharacterData enemyData;

        public float currentSpeed;
        public float currentHP;
        public float currentLevel;
        public float currentCoin;

        public NavMeshAgent navMeshAgent;

        [HideInInspector]
        public GameObject goalPoint;

        public Animator animator;

        [HideInInspector]
        public float stanTime;

        public BattleSceneManager bsManager;

        protected virtual void Start()
        {

        }

        public abstract void StartDamageState();

        public abstract void StartDieState();

        public abstract void StartReachGoal();

        public virtual void DamageApplicate(float _damage, float _stanTime)
        {
            navMeshAgent.isStopped = true;
            currentHP -= _damage;
            stanTime = _stanTime;

            if (currentHP <= 0)
            {
                StartDieState();
                bsManager.enemyAmount--;
                if (bsManager.enemyAmount <= 0)
                {
                    bsManager.UpdateBattleState(BattleSceneManager.BattleState.BattleEnd);
                }
            }
            else
            {
                StartDamageState();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Finish")
            {
                StartReachGoal();
            }
        }
    }
}