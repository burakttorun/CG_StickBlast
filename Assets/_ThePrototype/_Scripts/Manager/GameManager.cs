using System;
using System.Collections.Generic;
using BasicArchitecturalStructure;
using Unity.VisualScripting;
using UnityEngine;

namespace ThePrototype.Scripts.Managers
{
    public class GameManager : PersistentSingleton<GameManager>
    {
        public GridDataManager GridDataManager { get; private set; }
        [field: SerializeField] public int Rows { get; set; }
        [field: SerializeField] public int Columns { get; set; }

        private EventBinding<CellFilled> _cellFilledEventBinding;
        private List<CellManager> _cellManagers;

        protected override void Awake()
        {
            base.Awake();
            GridDataManager = new GridDataManager(Rows, Columns);
            _cellFilledEventBinding = new EventBinding<CellFilled>(CheckCells);
        }

        private void OnEnable()
        {
            EventBus<CellFilled>.Subscribe(_cellFilledEventBinding);
        }

        private void OnDisable()
        {
            EventBus<CellFilled>.Unsubscribe(_cellFilledEventBinding);
        }

        private void Start()
        {
            PrintMatrix();
        }

        private void CheckCells(CellFilled args)
        {
            int cellRow = args.ownDatas.Row;
            int cellCol = args.ownDatas.Column;
            bool isMadePointCol = true;
            bool isMadePointRow = true;
            for (int row = 0; row < Rows; row++)
            {
                isMadePointRow &= GridDataManager._cellGrid[row, cellCol].IsFilled;
            }

            for (int col = 0; col < Columns; col++)
            {
                isMadePointCol &= GridDataManager._cellGrid[cellRow, col].IsFilled;
            }

            if (isMadePointCol)
            {
                for (int col = 0; col < Columns; col++)
                {
                    GridDataManager._cellGrid[cellRow, col].ResetCell();
                }
            }

            if (isMadePointRow)
            {
                for (int row = 0; row < Rows; row++)
                {
                    GridDataManager._cellGrid[row, cellCol].ResetCell();
                }
            }

            if (isMadePointRow || isMadePointCol)
            {
                foreach (var cellGrid in GridDataManager._cellGrid)
                {
                    cellGrid.CleanPaint();
                }
            }
        }


        private void PrintMatrix()
        {
            string matrix = "";
            for (int i = 0; i <= Rows; i++)
            {
                for (int j = 0; j <= Columns; j++)
                {
                    if (i < Rows)
                    {
                        matrix += GridDataManager._verticalEdges[i, j] + " ";
                    }
                }

                matrix += "\n";
            }

            Debug.Log(matrix);
        }
    }
}