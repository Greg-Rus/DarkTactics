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
        private float _tileSize;
        private TileModel[,] _tileModelMap;

        public GridService(int width, int height, float tileSize)
        {
            _gridWidth = width;
            _gridHeight = height;
            _tileSize = tileSize;
            _tileModelMap = new TileModel[width, height];
        }

        public void Initialize()
        {
            for (int x = 0; x < _gridWidth; x++)
            {
                for (int z = 0; z < _gridHeight; z++)
                {
                    var tileCoordinates = new Vector2Int(x, z);
                    var tilePosition = GridCoordinateToWorldPosition(tileCoordinates);
                    var debugObject = GameObject.Instantiate(GameContextRoot.DebugTileMarker, tilePosition,
                        Quaternion.identity);
                    debugObject.transform.SetParent(GameContextRoot.DebugRoot);
                    debugObject.CoordiantesText.text = $"{x}:{z}";
                    _tileModelMap[x, z] = new TileModel(tileCoordinates);
                }
            }
        }

        public Vector2Int WorldPositionToGridCoordinate(Vector3 worldPosition)
        {
            var gridCoordinate = new Vector2Int(Mathf.RoundToInt(worldPosition.x / _tileSize),
                Mathf.RoundToInt(worldPosition.z / _tileSize));

            return gridCoordinate;
        }

        [CanBeNull]
        public TileModel GridCoordinateToTileModel(Vector2Int gridCoordinate)
        {
            if (IsValidTileCoordinate(gridCoordinate))
            {
                var tile = _tileModelMap[gridCoordinate.x, gridCoordinate.y];
                return tile;
            }
            else
            {
                return null;
            }
        }

        public bool IsValidTileCoordinate(Vector2Int coordinate)
        {
            return coordinate.x >= 0 &&
                   coordinate.x < _gridWidth &&
                   coordinate.y >= 0 &&
                   coordinate.y < _gridHeight;
        }

        [CanBeNull]
        public TileModel WorldPositionToTileModel(Vector3 worldPosition)
        {
            var gridCoordinate = WorldPositionToGridCoordinate(worldPosition);
            return GridCoordinateToTileModel(gridCoordinate);
        }

        public Vector3 GridCoordinateToWorldPosition(Vector2Int tileCoordinates)
        {
            return new Vector3(tileCoordinates.x, 0f, tileCoordinates.y) * _tileSize;
        }

        public TileModel[,] GetTilesInRange(int range, Vector2Int originTile, Func<TileModel, bool> walkableCondition = null)
        {
            var tiles = new TileModel[range * 2 + 1, range * 2 + 1];

            for (int x = -range; x < range + 1; x++)
            {
                for (int y = -range; y < range + 1; y++)
                {
                    var tile = new Vector2Int(originTile.x + x, originTile.y + y);

                    if (!IsValidTileCoordinate(tile))
                    {
                        continue;
                    }

                    var directionToTile = tile - originTile;

                    if (directionToTile.magnitude > range)
                    {
                        continue;
                    }

                    var tileModel = GridCoordinateToTileModel(tile);
                    if (walkableCondition != null && tileModel != null && walkableCondition.Invoke(tileModel) == false)
                    {
                        tileModel = null;
                    }

                    tiles[x + range, y + range] = tileModel;
                }
            }

            return tiles;
        }
        
        public List<Vector2Int> GetTileCoordinatesInRange(int range, Vector2Int originTile, Func<TileModel, bool> tileModelPassFilter = null)
        {
            var result = new List<Vector2Int>();

            for (int x = -range; x < range + 1; x++)
            {
                for (int y = -range; y < range + 1; y++)
                {
                    var tile = new Vector2Int(originTile.x + x, originTile.y + y);
                    
                    if (!IsValidTileCoordinate(tile))
                    {
                        continue;
                    }

                    var directionToTile = tile - originTile;

                    if (directionToTile.magnitude > range)
                    {
                        continue;
                    }

                    if (tileModelPassFilter != null)
                    {
                        var tileModel = GridCoordinateToTileModel(tile);
                        if (tileModel == null || tileModelPassFilter.Invoke(tileModel) == false)
                        {
                            continue;
                        }
                    }

                    result.Add(tile);
                }
            }

            return result;
        }

        public Vector2 GetUnitCoordinatesByUnitId(int unitId)
        {
            var coordinates = Vector2.one * -1;
            foreach (var tile in _tileModelMap)
            {
                if(tile.Entities.Contains(unitId))
                {
                    coordinates = tile.Coordinates;
                    break;
                }
            }

            return coordinates;
        }

        public List<Vector2Int> GetAdjacentTiles(Vector2Int position)
        {
            var tiles = new List<Vector2Int>();
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y < 1; y++)
                {
                    if(x == 0 && y == 0 ) continue;
                    if (IsValidTileCoordinate(position))
                    {
                        tiles.Add(new Vector2Int(x, y));
                    }
                }
            }

            return tiles;
        }

        public Vector2Int? GetClosestAdjacentTileTowards(Vector2Int position)
        {
            var adjacentTiles = GetAdjacentTiles(position);
            if (adjacentTiles.Count == 0) return null;
            if (adjacentTiles.Count == 1) return adjacentTiles[0];

            return GetClosestTileInRangeTowardsTarget(adjacentTiles, position);
        }

        public Vector2Int GetClosestTileInRangeTowardsTarget(IEnumerable<Vector2Int> range, Vector2Int target)
        {
            Vector2Int closestTile = new Vector2Int(Int32.MaxValue, Int32.MaxValue);
            float shortestDistance = float.MaxValue;

            foreach (var tile in range)
            {
                if(tile == target) continue;
                
                var distance = (tile - target).sqrMagnitude;
                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    closestTile = tile;
                }
            }

            return closestTile;
        }
    }
}