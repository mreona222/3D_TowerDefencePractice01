using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefencePractice.Constructable
{
    /// <summary>
    /// �����\�Ȃ���
    /// </summary>
    public interface IConstructable
    {
        public GameObject InstantiateConstructable(Transform gridCell);
    }
}