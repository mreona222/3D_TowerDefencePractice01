using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefencePractice.Turrets
{
    [CreateAssetMenu(menuName = "My Scriptable/Create TurretData")]

    public class TurretData : ScriptableObject
    {
        //---------------------------------------------------------
        // ��{���
        //---------------------------------------------------------

        // �^���b�g�̖��O
        public string turretName;
        // �^���b�g�̃A�C�R��
        public Sprite turretIcon;

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