using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using TowerDefencePractice.Character;
using TowerDefencePractice.Character.Enemies;
using TowerDefencePractice.Managers;

namespace TowerDefencePractice.Spawner
{
    public class SpawnerBehaviour : MonoBehaviour
    {
        [SerializeField]
        CharcterBoss characterBoss;

        [SerializeField]
        GameObject[] characterPrefab;

        [SerializeField]
        BattleSceneManager bsManager;

        [SerializeField]
        AudioSource spawnSource;

        [SerializeField]
        AudioClip spawnClip;

        private void Start()
        {
            if (System.Enum.GetValues(typeof(EnemyBehaviourBase.Enemies)).Length != characterPrefab.Length)
            {
                Debug.LogError("Spawnerに登録されている敵の数が間違っています。");
            }
        }


        // キャラクタースポーン
        public void SpawnCharacter(float level, EnemyBehaviourBase.Enemies character, GameObject goal)
        {
            GameObject _character = Instantiate(characterBoss.characterScriptableObject[(int)character].characterPrefab, transform.position, transform.rotation);
            _character.transform.localScale = Vector3.Scale(_character.transform.localScale, transform.parent.localScale);
            if (_character.TryGetComponent<EnemyBehaviourBase>(out EnemyBehaviourBase enemy))
            {
                enemy.currentLevel = level;
                enemy.goalPoint = goal;
                enemy.bsManager = bsManager;
                enemy.GetComponent<EnemyLevelUpBase>().Initialize();
            }
            spawnSource.PlayOneShot(spawnClip);
        }
    }
}