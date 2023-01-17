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
        // �����\���̃^�C�v
        public enum ConstructableType
        {
            Turret,

        }
        public ConstructableType constructableType;

        //---------------------------------------------------------
        // �ˌ��Ԋu
        //---------------------------------------------------------

        // ��{�ˌ��Ԋu
        public float fireRateBase;
        // �ˌ��Ԋu�̍ő�̑���
        public float fireRateMax;
        // �ˌ��Ԋu�̍ő�̃��x��
        public float fireRateMaxLevel;
        // �ˌ��Ԋu�̏㏸�䗦
        public float fireRateRatio;
        // �ˌ��Ԋu�̗ݏ�
        public float fireRatePow;
        // �ˌ��Ԋu�A�b�v�O���[�h�̏�����p
        public float fireRateUpgradeCoinBase;
        // �ˌ��Ԋu�A�b�v�O���[�h�R�X�g�̏㏸�䗦
        public float fireRateUpgradeCoinRatio;
        // �ˌ��Ԋu�A�b�v�O���[�h�R�X�g�̋}�s��
        public float fireRateUpgradeCoinPow;

        //---------------------------------------------------------
        // �ˌ��Η�
        //---------------------------------------------------------

        // ��{�Η�
        public float firePowerBase;
        // �ő�Η�
        public float firePowerMax;
        // �ő�Ἠ��x��
        public float firePowerMaxLevel;
        // �ˌ��Η͂̏㏸�䗦
        public float firePowerRatio;
        // �ˌ��Η̗͂ݏ�
        public float firePowerPow;
        // �ˌ��ἨA�b�v�O���[�h�̏�����p
        public float firePowerUpgradeCoinBase;
        // �ˌ��ἨA�b�v�O���[�h�R�X�g�̏㏸�䗦
        public float firePowerUpgradeCoinRatio;
        // �ˌ��ἨA�b�v�O���[�h�R�X�g�̋}�s��
        public float firePowerUpgradeCoinPow;

        //---------------------------------------------------------
        // �˒�
        //---------------------------------------------------------

        // ��{�˒�
        public float fireRangeBase;
        // �ő�˒�
        public float fireRangeMax;
        // �˒����x��
        public float fireRangeMaxLevel;
        // �˒��̏㏸�䗦
        public float fireRangeRatio;
        // �˒��̗ݏ�
        public float fireRangePow;
        // �˒��A�b�v�O���[�h�̏�����p
        public float fireRangeUpgradeCoinBase;
        // �˒��A�b�v�O���[�h�R�X�g�̏㏸�䗦
        public float fireRangeUpgradeCoinRatio;
        // �ˌ��Ԋu�A�b�v�O���[�h�R�X�g�̋}�s��
        public float fireRangeUpgradeCoinPow;

        //---------------------------------------------------------
        // �R�X�g
        //---------------------------------------------------------

        // ��{�K�v�l��
        public int requireStuffBase;
        // ��{�K�v�o��
        public int requireCoinBase;
    }
}