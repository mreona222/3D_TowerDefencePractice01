using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefencePractice.Grids
{
    public class GridMapGenerator : MonoBehaviour
    {
        [SerializeField]
        Vector2 matrix;

        [SerializeField]
        Vector3 cellScale;

        [SerializeField]
        float gridSpacing = 0;

        [SerializeField]
        GameObject gridCell;

        void Start()
        {

        }

        [ContextMenu("Generate Grid")]
        void GridGenerator()
        {
            for (int j = 0; j < matrix.y; j++)
            {
                for (int i = 0; i < matrix.x; i++)
                {
                    // GridMap‚Ì¶¬
                    GameObject gridInstance = Instantiate(gridCell, transform.position + new Vector3(
                        -(cellScale.x * (matrix.x - 1) / 2 + (matrix.x - 1) / 2 * gridSpacing) + (cellScale.x * i) + (gridSpacing * i),
                        -cellScale.y / 2,
                        (cellScale.z * (matrix.y - 1) / 2 + (matrix.x - 1) / 2 * gridSpacing) - (cellScale.z * j) - (gridSpacing * j)),
                        transform.rotation, transform);
                    gridInstance.name = $"GridCell [ {i}, {j}]";
                    gridInstance.transform.localScale = Vector3.Scale(gridInstance.transform.localScale, cellScale);
                }
            }
        }
    }
}