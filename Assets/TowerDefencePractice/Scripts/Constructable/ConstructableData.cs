using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefencePractice.Constructable
{
    [CreateAssetMenu(menuName = "My Scriptable/Create ConstructableData")]

    public class ConstructableData : ScriptableObject
    {
        //---------------------------------------------------------
        // ��{���
        //---------------------------------------------------------

        // �����\���̖��O
        public string constructableName;
        // �����\���̃A�C�R��
        public Sprite constructableIcon;
        // �����\���̃v���n�u
        public GameObject constructablePrefab;

        //---------------------------------------------------------
        // �ˌ��Ԋu
        //---------------------------------------------------------

        // ���̎ˌ��Ԋu
        public float fireRateBase;
        // �ˌ��Ԋu�̍ő�̑���
        public float fireRateMax;
        // �ˌ��Ԋu�̍ő�̃��x��
        public float fireRateMaxLevel;

        //---------------------------------------------------------
        // �ˌ��Η�
        //---------------------------------------------------------

        // �^���b�g�̊�{�Η�
        public float firePowerBase;

    }
}