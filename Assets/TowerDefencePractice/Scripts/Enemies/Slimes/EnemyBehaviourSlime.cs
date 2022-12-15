using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.States;

namespace TowerDefencePractice.Character.Enemies
{
    public class EnemyBehaviourSlime : EnemyBehaviourBase
    {
        public enum SlimeState
        {
            Idle,
            Walk,
        }

        public SlimeState currentSlimeState = SlimeState.Idle;

        protected override void Start()
        {
            base.Start();
            ChangeState(new EnemyBehaviourSlime.Idle(this));
        }


        public override void StartWalk()
        {
            ChangeState(new EnemyBehaviourSlime.Walk(this));
        }


        // -----------------------------------------------------
        // Idle
        // -----------------------------------------------------
        private class Idle : StateBase<EnemyBehaviourBase>
        {
            public Idle(EnemyBehaviourBase _machine) : base(_machine)
            {
            }

            public override void OnEnter()
            {
                machine.GetComponent<EnemyBehaviourSlime>().currentSlimeState = SlimeState.Idle;
            }

            public override void OnUpdate()
            {

            }
        }



        // -----------------------------------------------------
        // Walk
        // -----------------------------------------------------
        private class Walk : StateBase<EnemyBehaviourBase>
        {
            public Walk(EnemyBehaviourBase _machine) : base(_machine)
            {
            }

            public override void OnEnter()
            {
                machine.GetComponent<EnemyBehaviourSlime>().currentSlimeState = SlimeState.Walk;
                machine.navMeshAgent.SetDestination(machine.goalPoint.transform.position);
            }

            public override void OnUpdate()
            {

            }
        }
    }
}