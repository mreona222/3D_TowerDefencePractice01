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
            enemyBehaviour.currentHP = EnemyHPLevelUp();
            enemyBehaviour.currentSpeed = EnemySpeedLevelUp();
            enemyBehaviour.navMeshAgent.speed = enemyBehaviour.currentSpeed;
        }


        protected virtual float EnemyHPLevelUp()
        {
            return enemyBehaviour.enemyData.characterHPBase * (1 + (enemyBehaviour.currentLevel / 10));
        }

        protected virtual float EnemySpeedLevelUp()
        {
            // �X�s�[�hMaxLevel���Ⴂ�ꍇ
            if (enemyBehaviour.currentLevel <= enemyBehaviour.enemyData.characterSpeedMaxLevel)
            {
                return enemyBehaviour.enemyData.characterSpeedBase +
                    (enemyBehaviour.enemyData.characterSpeedMax - enemyBehaviour.enemyData.characterSpeedBase) * enemyBehaviour.currentLevel / enemyBehaviour.enemyData.characterSpeedMaxLevel;
            }
            // MaxLevel��荂���ꍇ
            else
            {
                return enemyBehaviour.enemyData.characterSpeedMax;
            }
        }
    }
}