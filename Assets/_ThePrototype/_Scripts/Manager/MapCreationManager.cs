using System.Collections.Generic;
using UnityEngine;

namespace ThePrototype.Scripts.Managers
{
    public class MapCreationManager : MonoBehaviour
    {
        [Header("References")] public GameObject edgePrefab;
        public GameObject cellPrefab;
        public GameObject pointPrefab;

        [Header("Settings")] public int columns = 5;
        public int rows = 5;

        public float cellSize = 1.0f;
        public float edgeWidth = 0.2f;
        public float edgeHeight = 0.2f;
        public float pointSize = 0.2f;

        private HashSet<(Vector3 position, float rotation)> _edgeSet = new HashSet<(Vector3, float)>();

        private void Start()
        {
            CreateGrid();
        }

        private void CreateGrid()
        {
            float startX = (columns - 1) * (cellSize + edgeWidth) / 2;
            float startY = (rows - 1) * (cellSize + edgeHeight) / 2;

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < columns; c++)
                {
                    Vector3 cellPosition = new Vector3(c * (cellSize + edgeWidth) - startX,
                        r * (cellSize + edgeHeight) - startY, 0);
                    var cell = Instantiate(cellPrefab, cellPosition, Quaternion.identity);
                    cell.transform.localScale = new Vector3(cellSize, cellSize, 1);

                    cell.name = $"cell_{c}_{r}";
                    CreateEdgesAroundCell(cellPosition, c, r);
                }
            }

            CreateCirclePoints(new Vector3(-startX, -startY));
        }


        private void CreateEdgesAroundCell(Vector3 cellPosition, int cellX, int cellY)
        {
            Vector3 topEdgePosition = cellPosition + new Vector3(0, (cellSize / 2) + (edgeHeight / 2), 0);
            if (!EdgeExists(topEdgePosition, 0))
            {
                var topEdge = Instantiate(edgePrefab, topEdgePosition, Quaternion.identity);
                topEdge.transform.localScale = new Vector3(cellSize, edgeHeight, 1);
                topEdge.name = $"edge_{cellX}_{cellY}_top";


                _edgeSet.Add((topEdgePosition, 0));
            }

            Vector3 bottomEdgePosition = cellPosition - new Vector3(0, (cellSize / 2) + (edgeHeight / 2), 0);
            if (!EdgeExists(bottomEdgePosition, 0))
            {
                var bottomEdge = Instantiate(edgePrefab, bottomEdgePosition, Quaternion.identity);
                bottomEdge.transform.localScale = new Vector3(cellSize, edgeHeight, 1);
                bottomEdge.name = $"edge_{cellX}_{cellY}_bottom";


                _edgeSet.Add((bottomEdgePosition, 0));
            }

            Vector3 leftEdgePosition = cellPosition - new Vector3((cellSize / 2) + (edgeWidth / 2), 0, 0);
            if (!EdgeExists(leftEdgePosition, 90))
            {
                var leftEdge = Instantiate(edgePrefab, leftEdgePosition, Quaternion.Euler(0, 0, 90));
                leftEdge.transform.localScale = new Vector3(cellSize, edgeWidth, 1);
                leftEdge.name = $"edge_{cellX}_{cellY}_left";


                _edgeSet.Add((leftEdgePosition, 90));
            }

            Vector3 rightEdgePosition = cellPosition + new Vector3((cellSize / 2) + (edgeWidth / 2), 0, 0);
            if (!EdgeExists(rightEdgePosition, 90))
            {
                var rightEdge = Instantiate(edgePrefab, rightEdgePosition, Quaternion.Euler(0, 0, 90));
                rightEdge.transform.localScale = new Vector3(cellSize, edgeWidth, 1);
                rightEdge.name = $"edge_{cellX}_{cellY}_right";

                _edgeSet.Add((rightEdgePosition, 90));
            }
        }

        private void CreateCirclePoints(Vector3 startPos)
        {
            Vector3 startPosition = startPos -
                                    new Vector3((cellSize + edgeWidth) / 2, (cellSize + edgeHeight) / 2, 0);

            for (int r = 0; r <= rows; r++)
            {
                for (int c = 0; c <= columns; c++)
                {
                    Vector3 circlePosition = startPosition +
                                             new Vector3(c * (cellSize + edgeWidth), r * (cellSize + edgeHeight), 0);
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

        private bool EdgeExists(Vector3 position, float rotationZ = 0)
        {
            const float tolerance = 0.01f;

            foreach (var edge in _edgeSet)
            {
                if (Vector3.Distance(edge.position, position) < tolerance &&
                    Mathf.Abs(edge.rotation - rotationZ) < tolerance)
                {
                    return true;
                }
            }

            return false;
        }
    }
}