using UnityEngine;

namespace ThePrototype.Scripts.Managers
{
    public class GridDataManager
    {
        private CellManager[,] _cellGrid;
        private bool[,] _verticalEdges;
        private bool[,] _horizontalEdges;

        public GridDataManager(int rows, int columns)
        {
            _cellGrid = new CellManager[rows, columns];


            _verticalEdges = new bool[rows, columns + 1];

            _horizontalEdges = new bool[rows + 1, columns];
        }

        public void SetCell(int x, int y, CellManager cell)
        {
            _cellGrid[x, y] = cell;
        }

        public bool IsCellFull(int x, int y)
        {
            return _cellGrid[x, y].IsFilled;
        }

        public void SetVerticalEdge(int x, int y, bool isFull)
        {
            _verticalEdges[x, y] = isFull;
        }

        public void SetHorizontalEdge(int x, int y, bool isFull)
        {
            _horizontalEdges[x, y] = isFull;
        }

        public bool IsVerticalEdgeFull(int x, int y)
        {
            return _verticalEdges[x, y];
        }

        public bool IsHorizontalEdgeFull(int x, int y)
        {
            return _horizontalEdges[x, y];
        }
    }
}