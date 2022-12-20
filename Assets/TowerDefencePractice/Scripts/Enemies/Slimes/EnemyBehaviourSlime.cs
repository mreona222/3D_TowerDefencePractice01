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

        public override void StartDamageState()
        {
            if (currentSlimeState != SlimeState.Die || currentSlimeState != SlimeState.Damage)
            {
                ChangeState(new EnemyBehaviourSlime.Damage(this));
            }
        }

        public override void StartDieState()
        {
            if (currentSlimeState != SlimeState.Die)
            {
                ChangeState(new EnemyBehaviourSlime.Die(this));
            }
        }

        public override void StartReachGoal()
        {
            if (currentSlimeState != SlimeState.Die || currentSlimeState != SlimeState.ReachGoal)
            {
                ChangeState(new EnemyBehaviourSlime.ReachGoal(this));
            }
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
                machine.animator.speed = 1.0f;

                machine.StartCoroutine(Ready(0.2f));
            }

            IEnumerator Ready(float seconds)
            {
                float time = 0;
                while (true)
                {
                    if (((EnemyBehaviourSlime)machine).currentSlimeState != SlimeState.Idle) break;
                    yield return null;
                    time += Time.deltaTime;
                    if (time > seconds)
                    {
                        machine.ChangeState(new EnemyBehaviourSlime.Walk(machine));
                        break;
                    }
                }
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
                if (machine.currentLevel <= machine.enemyData.characterSpeedMaxLevel)
                {
                    machine.animator.speed = 2 * machine.currentLevel / (machine.enemyData.characterSpeedMaxLevel - 1)
                        + (machine.enemyData.characterSpeedMaxLevel - 3) / (machine.enemyData.characterSpeedMaxLevel - 1);
                }
                else
                {
                    machine.animator.speed = 2 * machine.enemyData.characterSpeedMaxLevel / (machine.enemyData.characterSpeedMaxLevel - 1)
                        + (machine.enemyData.characterSpeedMaxLevel - 3) / (machine.enemyData.characterSpeedMaxLevel - 1);
                }
            }

            public override void OnUpdate()
            {
                Vector3 toword = machine.navMeshAgent.steeringTarget - machine.transform.position;
                if (toword.magnitude > 0.1f)
                {
                    machine.navMeshAgent.velocity = toword.normalized * machine.navMeshAgent.speed;
                    toword = new Vector3(toword.x, 0, toword.z);
                    machine.transform.rotation = Quaternion.Slerp(machine.transform.rotation, Quaternion.LookRotation(toword, machine.transform.up), 10.0f * Time.deltaTime);
                }
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
                SlimeState prevState = ((EnemyBehaviourSlime)machine).currentSlimeState;
                machine.GetComponent<EnemyBehaviourSlime>().currentSlimeState = SlimeState.Damage;
                machine.animator.SetInteger("SlimeState", (int)SlimeState.Damage);
                machine.animator.speed = 1.0f;

                machine.StartCoroutine(Stan(prevState));
            }

            IEnumerator Stan(SlimeState _prevState)
            {
                yield return new WaitForSeconds(machine.stanTime);
                // 元のステートに戻る（Idleステートには戻らない）
                switch (_prevState)
                {
                    case SlimeState.Idle:
                        machine.ChangeState(new EnemyBehaviourSlime.Walk(machine));
                        break;
                    case SlimeState.Walk:
                        machine.ChangeState(new EnemyBehaviourSlime.Walk(machine));
                        break;
                    case SlimeState.Damage:
                        machine.ChangeState(new EnemyBehaviourSlime.Walk(machine));
                        break;
                    case SlimeState.Die:
                        machine.ChangeState(new EnemyBehaviourSlime.Die(machine));
                        break;
                    case SlimeState.ReachGoal:
                        machine.ChangeState(new EnemyBehaviourSlime.ReachGoal(machine));
                        break;
                }
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
                machine.animator.speed = 1.0f;

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
                machine.animator.speed = 1.0f;

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