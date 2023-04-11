using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Models;
using JetBrains.Annotations;
using strange.extensions.context.api;
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
            if (IsValidGridCellCoordinate(gridCoordinate))
            {
                var gridCell = _gridCellModelMap[gridCoordinate.x, gridCoordinate.y];
                return gridCell;
            }
            else
            {
                return null;
            }
        }

        public bool IsValidGridCellCoordinate(Vector2Int coordinate)
        {
            return coordinate.x >= 0 &&
                   coordinate.x < _gridWidth &&
                   coordinate.y >= 0 &&
                   coordinate.y < _gridHeight;
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

                    if (!IsValidGridCellCoordinate(cell))
                    {
                        continue;
                    }

                    var directionToCell = cell - originCell;

                    if (directionToCell.magnitude > range)
                    {
                        continue;
                    }

                    var cellModel = GridCoordinateToGridCellModel(cell);
                    if (walkableCondition != null && cellModel != null && walkableCondition.Invoke(cellModel) == false)
                    {
                        cellModel = null;
                    }

                    cells[x + range, y + range] = cellModel;
                }
            }

            return cells;
        }
        
        public List<Vector2Int> GetCellCoordinatesInRange(int range, Vector2Int originCell, Func<GridCellModel, bool> cellModelPassFilter = null)
        {
            var result = new List<Vector2Int>();

            for (int x = -range; x < range + 1; x++)
            {
                for (int y = -range; y < range + 1; y++)
                {
                    var cell = new Vector2Int(originCell.x + x, originCell.y + y);
                    
                    if (!IsValidGridCellCoordinate(cell))
                    {
                        continue;
                    }

                    var directionToCell = cell - originCell;

                    if (directionToCell.magnitude > range)
                    {
                        continue;
                    }

                    if (cellModelPassFilter != null)
                    {
                        var cellModel = GridCoordinateToGridCellModel(cell);
                        if (cellModel == null || cellModelPassFilter.Invoke(cellModel) == false)
                        {
                            continue;
                        }
                    }

                    result.Add(cell);
                }
            }

            return result;
        }

        public Vector2 GetUnitCoordinatesByUnitId(int unitId)
        {
            var coordinates = Vector2.one * -1;
            foreach (var cell in _gridCellModelMap)
            {
                if(cell.Entities.Contains(unitId))
                {
                    coordinates = cell.Coordinates;
                    break;
                }
            }

            return coordinates;
        }

        public List<Vector2Int> GetAdjacentGridCells(Vector2Int position)
        {
            var cells = new List<Vector2Int>();
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y < 1; y++)
                {
                    if(x == 0 && y == 0 ) continue;
                    if (IsValidGridCellCoordinate(position))
                    {
                        cells.Add(new Vector2Int(x, y));
                    }
                }
            }

            return cells;
        }

        public Vector2Int? GetClosestAdjacentCellTowards(Vector2Int position)
        {
            var adjacentCells = GetAdjacentGridCells(position);
            if (adjacentCells.Count == 0) return null;
            if (adjacentCells.Count == 1) return adjacentCells[0];

            return GetClosestCellInRangeTowardsTarget(adjacentCells, position);
        }

        public Vector2Int GetClosestCellInRangeTowardsTarget(IEnumerable<Vector2Int> range, Vector2Int target)
        {
            Vector2Int closestCell = new Vector2Int(Int32.MaxValue, Int32.MaxValue);
            float shortestDistance = float.MaxValue;

            foreach (var cell in range)
            {
                if(cell == target) continue;
                
                var distance = (cell - target).sqrMagnitude;
                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    closestCell = cell;
                }
            }

            return closestCell;
        }
    }
}