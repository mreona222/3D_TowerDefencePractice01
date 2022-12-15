using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using Utilities.States; 

namespace TowerDefencePractice.Character.Enemies
{
    public abstract class EnemyBehaviourBase : StateMachineBase<EnemyBehaviourBase>
    {
        public enum Enemies
        {
            slime,

        }

        [SerializeField]
        public CharcterData enemyData;

        public NavMeshAgent navMeshAgent;

        public GameObject goalPoint;


        protected virtual void Start()
        {
            navMeshAgent.speed = enemyData.charcterSpeedBase;
        }

        public abstract void StartWalk();
    }
}