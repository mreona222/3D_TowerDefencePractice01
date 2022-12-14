using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefencePractice.Constructable
{
    /// <summary>
    /// 建造可能なもの
    /// </summary>
    public interface IConstructable
    {
        public GameObject InstantiateConstructable(Transform gridCell);
    }
}