using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Navigation
{
    public class GridManager : MonoBehaviour
    {
        public int gridWidth;
        public int gridHeight;

        public GameObject gridPrefab;


        [ContextMenu("Call This")]
        public void GenerateGrid()
        {
            for (int i = 0; i < gridWidth; i++)
            {       
                for (int j = 0; j < gridHeight; j++)
                {
                    GameObject gridSpace = Instantiate(gridPrefab, new Vector3(i, 0, j), Quaternion.identity);
                    gridSpace.name = i.ToString() + "," + j.ToString();
                }
            }
        }

    }
}
