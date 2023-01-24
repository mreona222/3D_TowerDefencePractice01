using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UniRx;
using UnityEngine.UI;

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
            ShellSlime,
            Bull,
        }


        public CharacterData enemyData;

        public float currentSpeed;
        public FloatReactiveProperty currentHP;
        public float currentLevel;
        public float currentCoin;

        public NavMeshAgent navMeshAgent;

        [HideInInspector]
        public GameObject goalPoint;

        public Animator animator;

        [HideInInspector]
        public float stanTime;

        private float damageInterval = 1.5f;
        private bool damageMotion = true;

        [HideInInspector]
        public BattleSceneManager bsManager;

        [SerializeField]
        Slider hpBar;
        [SerializeField]
        Text levelText;
        [SerializeField]
        GameObject enemyCanvas;

        [SerializeField]
        AudioSource enemySource;
        [SerializeField]
        AudioClip damageClip;
        [SerializeField]
        AudioClip dieClip;
        

        protected virtual void Start()
        {
            hpBar.maxValue = currentHP.Value;
            levelText.text = $"Lv.{currentLevel}";

            currentHP
                .Subscribe((x) =>
                {
                    hpBar.value = currentHP.Value;
                })
                .AddTo(this);
        }

        protected override void Update()
        {
            base.Update();
            Quaternion cameraRotation = Camera.main.transform.rotation;
            enemyCanvas.transform.rotation = cameraRotation;
        }

        public abstract void StartDamageState();

        public abstract void StartDieState();

        public abstract void StartReachGoal();

        public virtual void DamageApplicate(float _damage, float _stanTime)
        {
            currentHP.Value -= _damage;
            stanTime = _stanTime;

            if (currentHP.Value <= 0)
            {
                StartDieState();
                enemySource.PlayOneShot(dieClip);
                bsManager.enemyAmount.Value--;
                bsManager.money.Value += Mathf.FloorToInt(currentCoin);
            }
            else
            {
                if (damageMotion)
                {
                    StartCoroutine(DamageInterval());
                    StartDamageState();
                    enemySource.PlayOneShot(damageClip);
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Finish")
            {
                StartReachGoal();
            }
        }

        IEnumerator DamageInterval()
        {
            damageMotion = false;
            yield return new WaitForSeconds(damageInterval);
            damageMotion = true;
        }
    }
}