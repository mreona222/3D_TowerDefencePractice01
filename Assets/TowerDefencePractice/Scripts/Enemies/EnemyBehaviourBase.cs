using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using Utilities.States;
using TowerDefencePractice.Damages;

namespace TowerDefencePractice.Character.Enemies
{
    public abstract class EnemyBehaviourBase : StateMachineBase<EnemyBehaviourBase>, IDamageApplicable
    {
        public enum Enemies
        {
            slime,

        }

        public CharacterData enemyData;

        public float currentSpeed;
        public float currentHP;
        public float currentLevel;

        public NavMeshAgent navMeshAgent;

        public GameObject goalPoint;

        public Animator animator;

        [HideInInspector]
        public float stanTime;

        protected virtual void Start()
        {

        }

        public abstract void StartWalkState();

        public abstract void StartDamageState();

        public abstract void StartDieState();

        public abstract void StartReachGoal();

        public virtual void DamageApplicate(float _damage, float _stanTime)
        {
            navMeshAgent.isStopped = true;
            currentHP -= _damage;
            stanTime = _stanTime;

            if (currentHP < 0)
            {
                StartDieState();
            }
            else
            {
                StartDamageState();
            }
        }
    }
}