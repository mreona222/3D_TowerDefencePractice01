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
            EnemyHPLevelUp();
            EnemySpeedLevelUp();
            StartCoroutine(StartWalkCoroutine());

        }
        IEnumerator StartWalkCoroutine()
        {
            yield return null;
            enemyBehaviour.StartWalk();
        }


        protected virtual void EnemyHPLevelUp()
        {

        }

        protected virtual void EnemySpeedLevelUp()
        {

        }
    }
}