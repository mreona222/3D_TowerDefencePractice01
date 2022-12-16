using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefencePractice.Character.Enemies
{
    public abstract class EnemyLevelUpBase : MonoBehaviour
    {
        protected EnemyBehaviourBase enemyBehaviour;

        protected virtual void Start()
        {
            enemyBehaviour = GetComponent<EnemyBehaviourBase>();
            enemyBehaviour.currentHP = EnemyHPLevelUp();
            enemyBehaviour.currentSpeed = EnemySpeedLevelUp();
            StartCoroutine(StartWalkCoroutine());

        }
        IEnumerator StartWalkCoroutine()
        {
            yield return null;
            enemyBehaviour.StartWalkState();
        }


        protected virtual float EnemyHPLevelUp()
        {
            return 0;
        }

        protected virtual float EnemySpeedLevelUp()
        {
            if (enemyBehaviour.currentLevel <= enemyBehaviour.enemyData.characterSpeedMaxLevel)
            {
                return enemyBehaviour.enemyData.characterSpeedBase +
                    (enemyBehaviour.enemyData.characterSpeedMax - enemyBehaviour.enemyData.characterSpeedBase) * enemyBehaviour.currentLevel / enemyBehaviour.enemyData.characterSpeedMaxLevel;
            }
            else
            {
                return enemyBehaviour.enemyData.characterSpeedMax;
            }
        }
    }
}