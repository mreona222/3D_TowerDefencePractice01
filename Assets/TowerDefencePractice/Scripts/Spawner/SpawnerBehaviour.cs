using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using TowerDefencePractice.Character.Enemies;

namespace TowerDefencePractice.Spawner
{
    public class SpawnerBehaviour : MonoBehaviour
    {
        [SerializeField]
        GameObject slime;

        [SerializeField]
        GameObject goal;

        private void Start()
        {
            StartCoroutine(a());
        }


        IEnumerator a()
        {
            while (true)
            {
                yield return new WaitForSeconds(3.0f);
                SpawnCharacter(slime, 10.0f);
            }
        }

        // キャラクタースポーン
        private void SpawnCharacter(GameObject character, float level)
        {
            GameObject _character = Instantiate(character, transform.position, transform.rotation);
            if (_character.TryGetComponent<EnemyBehaviourBase>(out EnemyBehaviourBase enemy))
            {
                enemy.currentLevel = level;
                enemy.goalPoint = goal;
            }
        }
    }
}