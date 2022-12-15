using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefencePractice.Character.Enemies
{
    [CreateAssetMenu(menuName = "My Scriptable/Create CharacterData")]

    public class CharcterData : ScriptableObject
    {
        // ------------------------------------------------------
        // ��{���
        // ------------------------------------------------------

        // �G�̖��O
        public string characterName;


        // ------------------------------------------------------
        // ���̏��
        // ------------------------------------------------------

        // �G��HP
        public float charcterHPBase;

        // �ړ����x
        public float charcterSpeedBase;


    }
}