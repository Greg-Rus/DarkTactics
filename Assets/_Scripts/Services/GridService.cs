using _Scripts.Models;
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
                    var cellPosition = GridCoordinatesToWorldPosition(x, z);
                    var debugObject = GameObject.Instantiate(GameContextRoot.DebugGridCellMarker, cellPosition,
                        Quaternion.identity);
                    debugObject.transform.SetParent(GameContextRoot.DebugRoot);
                    debugObject.CoordiantesText.text = $"{x}:{z}";
                }
            }
        }

        public Vector3 WorldPositionToWorldGridCell(Vector3 worldPosition)
        {
            var gridCellOrigin = new Vector3(Mathf.RoundToInt(worldPosition.x) / _gridCellSize,
                0f,
                Mathf.RoundToInt(worldPosition.z) / _gridCellSize);
            return gridCellOrigin;
        }

        public Vector3 GridCoordinatesToWorldPosition(int x, int z)
        {
            return new Vector3(x, 0f, z) * _gridCellSize;
        }
    }
}