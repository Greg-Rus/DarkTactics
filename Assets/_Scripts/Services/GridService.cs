using System;
using System.Collections.Generic;
using _Scripts.Models;
using JetBrains.Annotations;
using UnityEngine;

namespace _Scripts
{
    public class GridService
    {
        [Inject] public GameContextRoot GameContextRoot { get; set; }
        private int _gridWidth;
        private int _gridHeight;
        private float _gridCellSize;
        private GridCellModel[,] _gridCellModelMap;

        public GridService(int width, int height, float cellSize)
        {
            _gridWidth = width;
            _gridHeight = height;
            _gridCellSize = cellSize;
            _gridCellModelMap = new GridCellModel[width, height];
        }

        public void Initialize()
        {
            for (int x = 0; x < _gridWidth; x++)
            {
                for (int z = 0; z < _gridHeight; z++)
                {
                    var cellCoordinates = new Vector2Int(x, z);
                    var cellPosition = GridCoordinateToWorldPosition(cellCoordinates);
                    var debugObject = GameObject.Instantiate(GameContextRoot.DebugGridCellMarker, cellPosition,
                        Quaternion.identity);
                    debugObject.transform.SetParent(GameContextRoot.DebugRoot);
                    debugObject.CoordiantesText.text = $"{x}:{z}";
                    _gridCellModelMap[x, z] = new GridCellModel(cellCoordinates);
                }
            }
        }

        public Vector2Int WorldPositionToGridCoordinate(Vector3 worldPosition)
        {
            var gridCoordinate = new Vector2Int(Mathf.RoundToInt(worldPosition.x / _gridCellSize),
                Mathf.RoundToInt(worldPosition.z / _gridCellSize));

            return gridCoordinate;
        }

        [CanBeNull]
        public GridCellModel GridCoordinateToGridCellModel(Vector2Int gridCoordinate)
        {
            if (gridCoordinate.x >= 0 &&
                gridCoordinate.x < _gridWidth &&
                gridCoordinate.y >= 0 &&
                gridCoordinate.y < _gridHeight)
            {
                var gridCell = _gridCellModelMap[gridCoordinate.x, gridCoordinate.y];
                return gridCell;
            }
            else
            {
                return null;
            }
        }

        [CanBeNull]
        public GridCellModel WorldPositionToGridCellModel(Vector3 worldPosition)
        {
            var gridCoordinate = WorldPositionToGridCoordinate(worldPosition);
            return GridCoordinateToGridCellModel(gridCoordinate);
        }

        public Vector3 GridCoordinateToWorldPosition(Vector2Int cellCoordinates)
        {
            return new Vector3(cellCoordinates.x, 0f, cellCoordinates.y) * _gridCellSize;
        }

        public GridCellModel[,] GetCellsInRange(int range, Vector2Int originCell, Func<GridCellModel, bool> walkableCondition = null)
        {
            var cells = new GridCellModel[range * 2 + 1, range * 2 + 1];

            for (int x = -range; x < range + 1; x++)
            {
                for (int y = -range; y < range + 1; y++)
                {
                    var cell = new Vector2Int(originCell.x + x, originCell.y + y);

                    var directionToCell = cell - originCell;

                    if (directionToCell.magnitude > range) continue;

                    var cellModel = GridCoordinateToGridCellModel(cell);
                    if (walkableCondition != null && cellModel != null && walkableCondition.Invoke(cellModel) == false)
                    {
                        cellModel = null;
                    }

                    Debug.Log($"x{x}, y{y}");
                    cells[x + range, y + range] = cellModel;
                }
            }

            return cells;
        }
    }
}