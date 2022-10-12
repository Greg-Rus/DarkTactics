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

        public void DrawWalkableGrid(GridCellModel[,] walkableCellModels)
        {
            ClearWalkableGrid();
            
            if (walkableCellModels.Length > freeViews.Count)
            {
                SpawnViews(walkableCellModels.Length - freeViews.Count);
            }
            
            for (int x = 0; x <= walkableCellModels.GetUpperBound(0); x++)
            {
                for (int y = 0; y <= walkableCellModels.GetUpperBound(1); y++)
                {
                    var model = walkableCellModels[x, y];
                    if (model == null) continue;
                    
                    var coordinate = new Vector2Int(x, y);   
                    SetupView(coordinate, walkableCellModels);
                }
            }
        }

        public void ClearWalkableGrid()
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
            Debug.Log($"Spawning extra {viewCount} views");
            for (int i = 0; i < viewCount; i++)
            {
                var view = GameObject.Instantiate(prefab, gridVisualsRoot);
                view.gameObject.SetActive(false);
                freeViews.Enqueue(view);
            }
        }

        private void SetupView(Vector2Int coordinate, GridCellModel[,] map)
        {
            var view = freeViews.Dequeue();
            occupiedViews.Add(view);
            
            foreach (var offset in cardinalOffsets)
            {
                var neighborCoordinate = coordinate + offset;
                if (HasBorderAtLocalCoordinate(neighborCoordinate, map))
                {
                    if (offset == Vector2Int.up) view.BorderNorth.enabled = true;
                    if (offset == Vector2Int.down) view.BorderSouth.enabled = true;
                    if (offset == Vector2Int.right) view.BorderEast.enabled = true;
                    if (offset == Vector2Int.left) view.BorderWest.enabled = true;
                }
            }

            view.transform.position =
                GridService.GridCoordinateToWorldPosition(map[coordinate.x, coordinate.y].Coordinates);
            view.gameObject.SetActive(true);
        }

        private bool HasBorderAtLocalCoordinate(Vector2Int localOffset, GridCellModel[,] cells)
        {
            var hasBorder = true;
            if (localOffset.x >= 0 & localOffset.x <= cells.GetUpperBound(0) && localOffset.y >= 0 &&
                localOffset.y <= cells.GetUpperBound(1))
            {
                if (cells[localOffset.x, localOffset.y] != null)
                {
                    hasBorder = false;
                }
            }

            return hasBorder;
        }
        
        
    }
}