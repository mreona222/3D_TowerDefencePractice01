using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefencePractice.Character
{
    [CreateAssetMenu(menuName = "My Scriptable/Create CharacterData")]

    public class CharacterData : ScriptableObject
    {
        // ------------------------------------------------------
        // ��{���
        // ------------------------------------------------------

        // �L�����N�^�[�̖��O
        public string characterName;
        // �L�����N�^�[�̃v���n�u
        public GameObject characterPrefab;
        // �L�����N�^�[�̃^�C�v
        public enum CharacterType
        {
            Enemies,
            Fairies,

        }
        public CharacterType characterType;


        // ------------------------------------------------------
        // ���̏��
        // ------------------------------------------------------

        // �L�����N�^�[��MaxLevel
        public float characterLevelMax;

        // �L�����N�^�[�̊�{HP
        public float characterHPBase;
        // �L�����N�^�[��MaxHP
        public float characterHPMax;
        // �L�����N�^�[��HPMaxLevel
        public float characterHPMaxLevel;
        // �L�����N�^�[��HP�̏㏸�䗦
        public float characterHPRatio;
        // �L�����N�^�[��HP�̗ݏ�
        public float characterHPPow;

        // �L�����N�^�[�̊�{�ړ����x
        public float characterSpeedBase;
        // �L�����N�^�[�̍ō����x
        public float characterSpeedMax;
        // �L�����N�^�[�̑��x��MaxLevel
        public float characterSpeedMaxLevel;
        // �L�����N�^�[�̑��x�̏㏸�䗦
        public float characterSpeedRatio;
        // �L�����N�^�[�̑��x�̗ݏ�
        public float characterSpeedPow;

        // �L�����N�^�[�̗��Ƃ���{���z
        public float characterCoinBase;
        // �L�����N�^�[�̋��z�̏㏸�䗦
        public float characterCoinRatio;
        // �L�����N�^�[�̋��z�̗ݏ�
        public float characterCoinPow;
    }
}