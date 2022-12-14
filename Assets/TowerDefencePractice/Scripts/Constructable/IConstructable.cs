using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefencePractice.Constructable
{
    /// <summary>
    /// Œš‘¢‰Â”\‚È‚à‚Ì
    /// </summary>
    public interface IConstructable
    {
        public GameObject InstantiateConstructable(Transform gridCell);
    }
}