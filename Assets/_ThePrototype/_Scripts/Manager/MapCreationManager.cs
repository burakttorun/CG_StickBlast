using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace ThePrototype.Scripts.Managers
{
    public class MapCreationManager : MonoBehaviour
    {
        [Header("References")]
        public GameObject edgePrefab;
        public GameObject cellPrefab;
        public GameObject nodePrefab;

        [Header("Settings")] 
        [SerializeField] private float cellSize = 1.0f;
        [SerializeField] private float edgeWidth = 0.2f;
        [SerializeField] private float edgeHeight = 0.2f;
        [SerializeField] private float nodeSize = 0.2f;

        private int _columns;
        private int _rows;
        private GridDataManager _gridDataManager;
        private float _startX;
        private float _startY;

        private void Awake()
        {
            _gridDataManager = GameManager.Instance.GridDataManager;
            _columns = GameManager.Instance.Columns;
            _rows = GameManager.Instance.Rows;

            CreateGrid();
        }

        private void CreateGrid()
        {
            float startX = (_columns - 1) * (cellSize + edgeWidth) / 2;
            float startY = (_rows - 1) * (cellSize + edgeHeight) / 2;

            for (int row = 0; row <= _rows; row++)
            {
                for (int col = 0; col <= _columns; col++)
                {
                    Vector3 cellPosition = new Vector3(col * (cellSize + edgeWidth) - startX,
                        -row * (cellSize + edgeHeight) + startY, 0);
                    Vector3 nodePosition = cellPosition + new Vector3(-1 * ((cellSize / 2) + (edgeWidth / 2)),
                        (cellSize / 2) + (edgeHeight / 2), 0);
                    CreateNodeAtPosition(nodePosition, col, row);

                    if (col < _columns)
                    {
                        Vector3 horizontalEdgePosition =
                            cellPosition + new Vector3(0, (cellSize / 2) + (edgeHeight / 2), 0);
                        CreateEdgeOnPosition(horizontalEdgePosition, col, row, 0, "horizontal");
                        _gridDataManager.SetHorizontalEdge(row, col, false);
                    }

                    if (row < _rows)
                    {
                        Vector3 edgePosition = cellPosition - new Vector3((cellSize / 2) + (edgeWidth / 2), 0, 0);
                        CreateEdgeOnPosition(edgePosition, col, row, 90, "vertical");
                        _gridDataManager.SetVerticalEdge(row, col, false);
                    }

                    if (col < _columns && row < _rows)
                    {
                        var cell = Instantiate(cellPrefab, cellPosition, Quaternion.identity);
                        cell.transform.localScale = new Vector3(cellSize, cellSize, 1);

                        cell.name = $"cell_{row}_{col}";
                        _gridDataManager.SetCell(row, col, cell.GetComponent<CellManager>());
                    }
                }
            }
        }
        private void CreateEdgeOnPosition(Vector3 edgePosition, int col, int row, int angle, string direction)
        {
            var leftEdge = Instantiate(edgePrefab, edgePosition, Quaternion.Euler(0, 0, angle));
            leftEdge.transform.localScale = new Vector3(cellSize, edgeHeight, 1);
            leftEdge.name = $"edge_{col}_{row}_{direction}";
        }

        private void CreateNodeAtPosition(Vector3 position, int col, int row)
        {
            var node = Instantiate(nodePrefab, position, Quaternion.identity);
            node.transform.localScale = new Vector3(nodeSize, nodeSize, 1);
            node.name = $"node_{col}_{row}";
        }
    }
}