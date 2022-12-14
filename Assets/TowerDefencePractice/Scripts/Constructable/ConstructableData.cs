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
        // ???{????
        //---------------------------------------------------------

        // ?????\???̖??O
        public string constructableName;
        // ?????\???̃A?C?R??
        public Sprite constructableIcon;
        // ?????\???̃v???n?u
        public GameObject constructablePrefab;

        //---------------------------------------------------------
        // ?ˌ??Ԋu
        //---------------------------------------------------------

        // ???{?ˌ??Ԋu
        public float fireRateBase;
        // ?ˌ??Ԋu?̍ő??̑???
        public float fireRateMax;
        // ?ˌ??Ԋu?̍ő??̃??x??
        public float fireRateMaxLevel;
        // ?ˌ??Ԋu?̏㏸?䗦
        public float fireRateRatio;
        // ?ˌ??Ԋu?̗ݏ?
        public float fireRatePow;

        //---------------------------------------------------------
        // ?ˌ??Η?
        //---------------------------------------------------------

        // ???{?Η?
        public float firePowerBase;
        // ?ő??Η?
        public float firePowerMax;
        // ?ő??Ἠ??x??
        public float firePowerMaxLevel;
        // ?ˌ??Η͂̏㏸?䗦
        public float firePowerRatio;
        // ?ˌ??Η̗͂ݏ?
        public float firePowerPow;

        //---------------------------------------------------------
        // ?˒?
        //---------------------------------------------------------

        // ???{?˒?
        public float fireRangeBase;
        // ?ő??˒?
        public float fireRangeMax;
        // ?˒????x??
        public float fireRangeMaxLevel;
        // ?˒??̏㏸?䗦
        public float fireRangeRatio;
        // ?˒??̗ݏ?
        public float fireRangePow;

        //---------------------------------------------------------
        // ?R?X?g
        //---------------------------------------------------------

        // ???{?K?v?l??
        public float requireStuffBase;
        // ???{?K?v?o??
        public float requireCoinBase;
    }
}