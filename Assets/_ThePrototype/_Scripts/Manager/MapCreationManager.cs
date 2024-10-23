using System.Collections.Generic;
using UnityEngine;

namespace ThePrototype.Scripts.Managers
{
    public class MapCreationManager : MonoBehaviour
    {
        [Header("References")] public GameObject edgePrefab;
        public GameObject cellPrefab;
        public GameObject pointPrefab;

        [Header("Settings")] [SerializeField] private float cellSize = 1.0f;
        [SerializeField] private float edgeWidth = 0.2f;
        [SerializeField] private float edgeHeight = 0.2f;
        [SerializeField] private float pointSize = 0.2f;

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
            _startX = (_columns - 1) * (cellSize + edgeWidth) / 2;
            _startY = (_rows - 1) * (cellSize + edgeHeight) / 2;
            CreateGrid();
        }

        private void CreateGrid()
        {
            for (int r = 0; r < _rows; r++)
            {
                for (int c = 0; c < _columns; c++)
                {
                    Vector3 cellPosition = new Vector3(c * (cellSize + edgeWidth) - _startX,
                        -r * (cellSize + edgeHeight) + _startY, 0);
                    var cell = Instantiate(cellPrefab, cellPosition, Quaternion.identity);
                    cell.transform.localScale = new Vector3(cellSize, cellSize, 1);

                    cell.name = $"cell_{c}_{r}";
                    _gridDataManager.SetCell(r, c, cell.GetComponent<CellManager>());
                }
            }

            CreateHorizontalEdges();
            CreateVerticleEdges();
            CreateCirclePoints();
        }

        private void CreateHorizontalEdges()
        {
            for (int r = 0; r <= _rows; r++)
            {
                for (int c = 0; c < _columns; c++)
                {
                    Vector3 cellPosition = new Vector3(c * (cellSize + edgeWidth) - _startX,
                        -r * (cellSize + edgeHeight) + _startY, 0);
                    Vector3 edgePosition = cellPosition + new Vector3(0, (cellSize / 2) + (edgeHeight / 2), 0);
                    var topEdge = Instantiate(edgePrefab, edgePosition, Quaternion.identity);
                    topEdge.transform.localScale = new Vector3(cellSize, edgeHeight, 1);
                    topEdge.name = $"edge_{c}_{r}_horizontal";
                    _gridDataManager.SetHorizontalEdge(r, c, false);
                }
            }
        }

        private void CreateVerticleEdges()
        {
            for (int r = 0; r < _rows; r++)
            {
                for (int c = 0; c <= _columns; c++)
                {
                    Vector3 cellPosition = new Vector3(c * (cellSize + edgeWidth) - _startX,
                        -r * (cellSize + edgeHeight) + _startY, 0);
                    Vector3 edgePosition = cellPosition - new Vector3((cellSize / 2) + (edgeWidth / 2), 0, 0);
                    var leftEdge = Instantiate(edgePrefab, edgePosition, Quaternion.Euler(0, 0, 90));
                    leftEdge.transform.localScale = new Vector3(cellSize, edgeHeight, 1);
                    leftEdge.name = $"edge_{c}_{r}_verticle";
                    _gridDataManager.SetVerticalEdge(r, c, false);
                }
            }
        }


        private void CreateCirclePoints()
        {
            for (int r = 0; r <= _rows; r++)
            {
                for (int c = 0; c <= _columns; c++)
                {
                    Vector3 cellPosition = new Vector3(c * (cellSize + edgeHeight) - _startX,
                        -r * (cellSize + edgeHeight) + _startY, 0);
                    Vector3 circlePosition = cellPosition + new Vector3(-1 * ((cellSize / 2) + (edgeWidth / 2)),
                        (cellSize / 2) + (edgeHeight / 2), 0);
                    CreateCircleAtPosition(circlePosition, c, r);
                }
            }
        }

        private void CreateCircleAtPosition(Vector3 position, int circleX, int circleY)
        {
            var circle = Instantiate(pointPrefab, position, Quaternion.identity);
            circle.transform.localScale = new Vector3(pointSize, pointSize, 1);
            circle.name = $"circle_{circleX}_{circleY}";
        }
    }
}