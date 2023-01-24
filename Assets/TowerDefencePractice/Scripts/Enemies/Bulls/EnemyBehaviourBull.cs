using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.States;

using TowerDefencePractice.Managers;

namespace TowerDefencePractice.Character.Enemies
{
    public class EnemyBehaviourBull : EnemyBehaviourBase
    {
        public enum BullState
        {
            Idle,
            Walk,
            Run,
            Damage,
            Die,
            ReachGoal,
        }

        public BullState currentBullState = BullState.Idle;

        protected override void Start()
        {
            base.Start();
            ChangeState(new EnemyBehaviourBull.Idle(this));
        }

        public override void StartDamageState()
        {
            if (currentBullState != BullState.Die && currentBullState != BullState.Damage && currentBullState != BullState.ReachGoal && currentBullState != BullState.Run)
            {
                ChangeState(new EnemyBehaviourBull.Damage(this));
            }
        }

        public override void StartDieState()
        {
            if (currentBullState != BullState.Die)
            {
                ChangeState(new EnemyBehaviourBull.Die(this));
            }
        }

        public override void StartReachGoal()
        {
            if (currentBullState != BullState.Die && currentBullState != BullState.ReachGoal)
            {
                ChangeState(new EnemyBehaviourBull.ReachGoal(this));
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
                ((EnemyBehaviourBull)machine).currentBullState = BullState.Idle;
                machine.animator.SetInteger("BullState", (int)BullState.Idle);
                machine.animator.speed = 1.0f;

                machine.StartCoroutine(Ready(0.2f));
            }

            IEnumerator Ready(float seconds)
            {
                float time = 0;
                while (true)
                {
                    if (((EnemyBehaviourBull)machine).currentBullState != BullState.Idle) break;
                    yield return null;
                    time += Time.deltaTime;
                    if (time > seconds)
                    {
                        machine.ChangeState(new EnemyBehaviourBull.Walk(machine));
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
                if(((EnemyBehaviourBull)machine).currentBullState != BullState.Damage)
                {
                    machine.StartCoroutine(ChangeState2Run());
                }

                ((EnemyBehaviourBull)machine).currentBullState = BullState.Walk;
                machine.animator.SetInteger("BullState", (int)BullState.Walk);


                machine.navMeshAgent.isStopped = false;
                machine.navMeshAgent.SetDestination(machine.goalPoint.transform.position);
                if (machine.currentLevel <= machine.enemyData.characterSpeedMaxLevel)
                {
                    machine.animator.speed = 2 * machine.currentSpeed / (machine.enemyData.characterSpeedMaxLevel - 1)
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

            IEnumerator ChangeState2Run()
            {
                float time = 0;
                while (true)
                {
                    if (time > 5.0f)
                    {
                        if (((EnemyBehaviourBull)machine).currentBullState == BullState.Walk)
                        {
                            machine.ChangeState(new EnemyBehaviourBull.Run(machine));
                        }
                        else if (((EnemyBehaviourBull)machine).currentBullState == BullState.Die || ((EnemyBehaviourBull)machine).currentBullState == BullState.ReachGoal)
                        {
                            yield break;
                        }

                        if (((EnemyBehaviourBull)machine).currentBullState == BullState.Run)
                        {
                            yield break;
                        }
                    }
                    yield return null;
                    time += Time.deltaTime;
                }
            }
        }




        // -----------------------------------------------------
        // Run
        // -----------------------------------------------------
        private class Run : StateBase<EnemyBehaviourBase>
        {
            public Run(EnemyBehaviourBase _machine) : base(_machine)
            {
            }

            public override void OnEnter()
            {
                ((EnemyBehaviourBull)machine).currentBullState = BullState.Run;
                machine.animator.SetInteger("BullState", (int)BullState.Run);


                machine.navMeshAgent.isStopped = false;
                machine.navMeshAgent.speed = machine.currentSpeed * 2.0f;
                machine.navMeshAgent.SetDestination(machine.goalPoint.transform.position);
                if (machine.currentLevel <= machine.enemyData.characterSpeedMaxLevel)
                {
                    machine.animator.speed = 2 * machine.currentSpeed / (machine.enemyData.characterSpeedMaxLevel - 1)
                        + (machine.enemyData.characterSpeedMaxLevel - 3) / (machine.enemyData.characterSpeedMaxLevel - 1);
                }
                else
                {
                    machine.animator.speed = 2 * machine.enemyData.characterSpeedMaxLevel / (machine.enemyData.characterSpeedMaxLevel - 1)
                        + (machine.enemyData.characterSpeedMaxLevel - 3) / (machine.enemyData.characterSpeedMaxLevel - 1);
                }
            }

            float time = 0;

            public override void OnUpdate()
            {
                if (time > 3.0f)
                {
                    machine.ChangeState(new EnemyBehaviourBull.Walk(machine));
                }
                time += Time.deltaTime;
            }

            public override void OnExit()
            {
                machine.navMeshAgent.speed = machine.currentSpeed;
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
                BullState prevState = ((EnemyBehaviourBull)machine).currentBullState;
                ((EnemyBehaviourBull)machine).currentBullState = BullState.Damage;
                machine.animator.SetInteger("BullState", (int)BullState.Damage);
                machine.navMeshAgent.isStopped = true;
                machine.animator.speed = 1.0f;

                machine.StartCoroutine(Stan(prevState));
            }

            IEnumerator Stan(BullState _prevState)
            {
                yield return new WaitForSeconds(machine.stanTime);
                // 元のステートに戻る（Idleステートには戻らない）
                //switch (_prevState)
                //{
                //    case BullState.Idle:
                //        machine.ChangeState(new EnemyBehaviourBull.Walk(machine));
                //        break;
                //    case BullState.Walk:
                //        machine.ChangeState(new EnemyBehaviourBull.Walk(machine));
                //        break;
                //    case BullState.Damage:
                //        machine.ChangeState(new EnemyBehaviourBull.Walk(machine));
                //        break;
                //    case BullState.Die:
                //        machine.ChangeState(new EnemyBehaviourBull.Die(machine));
                //        break;
                //    case BullState.ReachGoal:
                //        machine.ChangeState(new EnemyBehaviourBull.ReachGoal(machine));
                //        break;
                //}
                machine.ChangeState(new EnemyBehaviourBull.Walk(machine));
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
                ((EnemyBehaviourBull)machine).currentBullState = BullState.Die;
                machine.animator.SetInteger("BullState", (int)BullState.Die);
                machine.animator.speed = 1.0f;

                machine.navMeshAgent.isStopped = true;
                machine.StartCoroutine(DieAnimation());
            }

            IEnumerator DieAnimation()
            {
                yield return new WaitForSeconds(2.0f);
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
                ((EnemyBehaviourBull)machine).currentBullState = BullState.ReachGoal;
                machine.animator.SetInteger("BullState", (int)BullState.ReachGoal);
                machine.animator.speed = 1.0f;

                machine.navMeshAgent.isStopped = true;
                machine.StartCoroutine(IntoGoal());
            }

            IEnumerator IntoGoal()
            {
                yield return new WaitForSeconds(1.0f);
                if (((EnemyBehaviourBull)machine).currentBullState != BullState.ReachGoal) yield break;
                machine.bsManager.enemyAmount.Value--;
                machine.bsManager.enemyGoalLimit.Value--;
                Destroy(machine.gameObject);
            }
        }

    }
}