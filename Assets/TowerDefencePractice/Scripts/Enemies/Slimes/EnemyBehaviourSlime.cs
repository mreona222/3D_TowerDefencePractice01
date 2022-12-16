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
            Damage,
            Die,
            ReachGoal,
        }

        public SlimeState currentSlimeState = SlimeState.Idle;

        protected override void Start()
        {
            base.Start();
            ChangeState(new EnemyBehaviourSlime.Idle(this));
        }


        public override void StartWalkState()
        {
            ChangeState(new EnemyBehaviourSlime.Walk(this));
        }

        public override void StartDamageState()
        {
            ChangeState(new EnemyBehaviourSlime.Damage(this));
        }

        public override void StartDieState()
        {
            ChangeState(new EnemyBehaviourSlime.Die(this));
        }

        public override void StartReachGoal()
        {
            ChangeState(new EnemyBehaviourSlime.ReachGoal(this));
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
                machine.animator.SetInteger("SlimeState", (int)SlimeState.Idle);
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
                machine.animator.SetInteger("SlimeState", (int)SlimeState.Walk);

                machine.navMeshAgent.isStopped = false;
                machine.navMeshAgent.SetDestination(machine.goalPoint.transform.position);
            }

            public override void OnUpdate()
            {
                
            }
        }



        // -----------------------------------------------------
        // Damage
        // -----------------------------------------------------
        private class Damage : StateBase<EnemyBehaviourBase>
        {
            public Damage(EnemyBehaviourBase _machine) : base(_machine)
            {
            }

            public override void OnEnter()
            {
                machine.GetComponent<EnemyBehaviourSlime>().currentSlimeState = SlimeState.Damage;
                machine.animator.SetInteger("SlimeState", (int)SlimeState.Damage);

                machine.StartCoroutine(Stan());
                Debug.Log("a");
            }

            IEnumerator Stan()
            {
                yield return new WaitForSeconds(machine.stanTime);
                machine.ChangeState(new EnemyBehaviourSlime.Walk(machine));
            }
        }



        // -----------------------------------------------------
        // Die
        // -----------------------------------------------------
        private class Die : StateBase<EnemyBehaviourBase>
        {
            public Die(EnemyBehaviourBase _machine) : base(_machine)
            {
            }

            public override void OnEnter()
            {
                machine.GetComponent<EnemyBehaviourSlime>().currentSlimeState = SlimeState.Die;
                machine.animator.SetInteger("SlimeState", (int)SlimeState.Die);

                machine.navMeshAgent.isStopped = true;
                machine.StartCoroutine(DieAnimation());
            }

            IEnumerator DieAnimation()
            {
                yield return new WaitForSeconds(1.0f);
                Destroy(machine.gameObject);
            }
        }



        // -----------------------------------------------------
        // ReachGoal
        // -----------------------------------------------------
        private class ReachGoal : StateBase<EnemyBehaviourBase>
        {
            public ReachGoal(EnemyBehaviourBase _machine) : base(_machine)
            {
            }

            public override void OnEnter()
            {
                machine.GetComponent<EnemyBehaviourSlime>().currentSlimeState = SlimeState.ReachGoal;
                machine.animator.SetInteger("SlimeState", (int)SlimeState.ReachGoal);

                machine.navMeshAgent.isStopped = true;
                machine.StartCoroutine(IntoGoal());
            }

            IEnumerator IntoGoal()
            {
                yield return new WaitForSeconds(1.0f);
                Destroy(machine.gameObject);
            }
        }
    }
}