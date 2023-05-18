using System;
using System.Collections.Generic;
using _Scripts.Models;
using _Scripts.Views;
using UnityEditor.UIElements;
using UnityEngine;

namespace _Scripts
{
    public class GridVisualsService
    {
        [Inject] public GridService GridService { private get; set; }

        private readonly Vector2Int[] cardinalOffsets =
            { 
                new(1, 0), 
                new(-1, 0), 
                new(0, 1), 
                new(0, -1) 
            };
        
        private readonly Transform gridVisualsRoot;
        private readonly GridVisualView prefab;
        private Queue<GridVisualView> freeViews;
        private List<GridVisualView> occupiedViews;

        public GridVisualsService(Transform gridVisualsRoot, GridVisualView prefab)
        {
            this.gridVisualsRoot = gridVisualsRoot;
            this.prefab = prefab;
            freeViews = new Queue<GridVisualView>();
            occupiedViews = new List<GridVisualView>();
        }

        public void DrawWalkableGrid(TileModel[,] walkableTileModels)
        {
            DrawGrid(walkableTileModels, GridStyle.Walkable);
        }

        public void DrawAttacableGrid(TileModel[,] walkableTileModels)
        {
            DrawGrid(walkableTileModels, GridStyle.Attackable);
        }

        private void DrawGrid(TileModel[,] walkableTileModels, GridStyle style)
        {
            ClearGrid();
            
            if (walkableTileModels.Length > freeViews.Count)
            {
                SpawnViews(walkableTileModels.Length - freeViews.Count);
            }
            
            for (int x = 0; x <= walkableTileModels.GetUpperBound(0); x++)
            {
                for (int y = 0; y <= walkableTileModels.GetUpperBound(1); y++)
                {
                    var model = walkableTileModels[x, y];
                    if (model == null) continue;
                    
                    var coordinate = new Vector2Int(x, y);   
                    SetupView(coordinate, walkableTileModels, style);
                }
            }
        }

        public void ClearGrid()
        {
            foreach (var view in occupiedViews)
            {
                view.BorderNorth.enabled = false;
                view.BorderSouth.enabled = false;
                view.BorderEast.enabled = false;
                view.BorderWest.enabled = false;
                view.gameObject.SetActive(false);
                freeViews.Enqueue(view);
            }
            occupiedViews.Clear();
        }

        private void SpawnViews(int viewCount)
        {
            for (int i = 0; i < viewCount; i++)
            {
                var view = GameObject.Instantiate(prefab, gridVisualsRoot);
                view.gameObject.SetActive(false);
                freeViews.Enqueue(view);
            }
        }

        private void SetupView(Vector2Int coordinate, TileModel[,] map, GridStyle style)
        {
            var view = freeViews.Dequeue();
            occupiedViews.Add(view);
            
            foreach (var offset in cardinalOffsets)
            {
                var neighborCoordinate = coordinate + offset;
                if (HasBorderAtLocalCoordinate(neighborCoordinate, map))
                {
                    if (offset == Vector2Int.up)    view.BorderNorth.enabled = true;
                    if (offset == Vector2Int.down)  view.BorderSouth.enabled = true;
                    if (offset == Vector2Int.right) view.BorderEast.enabled  = true;
                    if (offset == Vector2Int.left)  view.BorderWest.enabled  = true;
                }
            }

            view.transform.position = GridService.GridCoordinateToWorldPosition(map[coordinate.x, coordinate.y].Coordinates);
            view.gameObject.SetActive(true);
            switch (style)
            {
                case GridStyle.Walkable:
                    view.SetWalkScheme();
                    break;
                case GridStyle.Attackable:
                    view.SetAttackScheme();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(style), style, null);
            }
            
        }

        private bool HasBorderAtLocalCoordinate(Vector2Int localOffset, TileModel[,] tiles)
        {
            var hasBorder = true;
            if (localOffset.x >= 0 & localOffset.x <= tiles.GetUpperBound(0) && localOffset.y >= 0 &&
                localOffset.y <= tiles.GetUpperBound(1))
            {
                if (tiles[localOffset.x, localOffset.y] != null)
                {
                    hasBorder = false;
                }
            }

            return hasBorder;
        }
    }
}