using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefencePractice.Character.Enemies
{
    [CreateAssetMenu(menuName = "My Scriptable/Create CharacterData")]

    public class CharacterData : ScriptableObject
    {
        // ------------------------------------------------------
        // ��{���
        // ------------------------------------------------------

        // �L�����N�^�[�̖��O
        public string characterName;


        // ------------------------------------------------------
        // ���̏��
        // ------------------------------------------------------

        // �L�����N�^�[��MaxLevel
        public float characterLevelMax;

        // �L�����N�^�[��HP
        public float characterHPBase;
        // �L�����N�^�[��MaxHP
        public float characterHPMax;
        // �L�����N�^�[��HPMaxLevel
        public float characterHPMaxLevel;

        // �L�����N�^�[�̈ړ����x
        public float characterSpeedBase;
        // �L�����N�^�[�̍ō����x
        public float characterSpeedMax;
        // �L�����N�^�[��HPMaxLevel
        public float characterSpeedMaxLevel;

    }
}