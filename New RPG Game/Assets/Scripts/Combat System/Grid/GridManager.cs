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

        public GameObject currentGrid;

        [ContextMenu("Call This")]
        public void GenerateGrid()
        {
            for (int i = 0; i < gridWidth; i++)
            {
                for (int j = 0; j < gridHeight; j++)
                {
                    GameObject gridSpace = Instantiate(gridPrefab, new Vector3(j, 0, i), Quaternion.identity);
                    gridSpace.name = i.ToString() + "," + j.ToString();
                    GameObject parent = GameObject.FindGameObjectWithTag("LevelGridContainer");
                    gridSpace.gameObject.transform.SetParent(parent.transform);
                }
            }
        }

        public void GridState(bool on)
        {
            currentGrid.SetActive(on);
        }

        public void Awake()
        {
            while (Manager.instance == null)
            {
                return;
            }

            Manager.instance.gridManager = this;

        }
    }
}
