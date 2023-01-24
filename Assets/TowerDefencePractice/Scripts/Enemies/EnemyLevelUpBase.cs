using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefencePractice.Character.Enemies
{
    public abstract class EnemyLevelUpBase : MonoBehaviour
    {
        protected EnemyBehaviourBase enemyBehaviour;

        public void Initialize()
        {
            enemyBehaviour = GetComponent<EnemyBehaviourBase>();
            enemyBehaviour.currentHP.Value = EnemyHPLevelUp();
            enemyBehaviour.currentSpeed = EnemySpeedLevelUp();
            enemyBehaviour.currentCoin = EnemyCoinLevelUp();
            enemyBehaviour.navMeshAgent.speed = enemyBehaviour.currentSpeed;
        }


        protected virtual float EnemyHPLevelUp()
        {
            return enemyBehaviour.enemyData.characterHPBase * Mathf.Pow(1 + enemyBehaviour.currentLevel * enemyBehaviour.enemyData.characterHPRatio, enemyBehaviour.enemyData.characterHPPow);
        }

        protected virtual float EnemySpeedLevelUp()
        {
            // スピードMaxLevelより低い場合
            if (enemyBehaviour.currentLevel <= enemyBehaviour.enemyData.characterSpeedMaxLevel)
            {
                return (enemyBehaviour.enemyData.characterSpeedMax - enemyBehaviour.enemyData.characterSpeedBase)
                    / Mathf.Pow(enemyBehaviour.enemyData.characterSpeedMaxLevel, enemyBehaviour.enemyData.characterSpeedPow)
                    * Mathf.Pow(enemyBehaviour.currentLevel, enemyBehaviour.enemyData.characterSpeedPow) + enemyBehaviour.enemyData.characterSpeedBase;
            }
            // MaxLevelより高い場合
            else
            {
                return enemyBehaviour.enemyData.characterSpeedMax;
            }
        }

        protected virtual float EnemyCoinLevelUp()
        {
            return enemyBehaviour.enemyData.characterCoinBase * Mathf.Pow(1 + enemyBehaviour.currentLevel * enemyBehaviour.enemyData.characterCoinRatio, enemyBehaviour.enemyData.characterCoinPow);
        }
    }
}