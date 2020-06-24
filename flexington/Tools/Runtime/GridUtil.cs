using UnityEngine;
using UnityEngine.Rendering;

namespace flexington.Tools
{
    public static class GridUtil
    {
        private static Material _lineMaterial;
        private static Color _lineColor;
        private static Vector2Int _gridSize;
        private static Vector2 _cellSize;
        private static Vector2[] _lineStart;
        private static Vector2[] _lineEnd;

        /// <summary>
        /// Draws a 2D grid.
        /// This grid will be shown in the Editor as well as in game.
        /// </summary>
        public static void DrawGrid2D(Vector2Int gridSize, Vector2 cellSize)
        {
            CreateLineMaterial();
            _lineMaterial.SetPass(0);
            CalculateGrid(gridSize, cellSize);
            DrawGrid();
        }

        /// <summary>
        /// Creates the Material for the lines. 
        /// The Material is created only once.
        /// </summary>
        private static void CreateLineMaterial()
        {
            if (_lineMaterial != null) return;                                  // Leave if Material is already created

            Shader shader = Shader.Find("Hidden/Internal-Colored");             // Use Unity Build in Shader
            _lineMaterial = new Material(shader);                               // Create Maetrial with Shader
            _lineMaterial.SetInt("_SrcBlend", (int)BlendMode.SrcAlpha);         // Set Source Blend Mode
            _lineMaterial.SetInt("_DstBlend", (int)BlendMode.OneMinusSrcColor); // Set Distance Blend Mode
            _lineMaterial.SetInt("_Cull", (int)CullMode.Off);                   // Turn Backface culling off
            _lineMaterial.SetInt("_ZWrite", 0);                                 // Turn depth writing off
            _lineMaterial.hideFlags = HideFlags.HideAndDontSave;                // Dont safe this object and hide in hierachy
        }

        /// <summary>
        /// Calculates the lines of the grid and stores them in an array for easy access.
        /// As long as the grid or cell size don't changes, the grid is not recalculated.
        /// </summary>
        private static void CalculateGrid(Vector2Int gridSize, Vector2 cellSize)
        {
            if (_gridSize == gridSize && _cellSize == cellSize) return;         // If grid and cell size didn't change, leave function

            _gridSize = gridSize;                                               // Set grid size
            _cellSize = cellSize;                                               // Set cell size

            _lineStart = new Vector2[_gridSize.x + _gridSize.y + 2];            // Instantiate arrays to hold startpoint of grid lines
            _lineEnd = new Vector2[_gridSize.x + _gridSize.y + 2];              // Instantiate arrays to hold endpoint of grid lines

            int i = 0;                                                          // Int to keep track of overall index for x and y loop

            for (int x = 0; x <= _gridSize.x; x++, i++)                         // Calculate lines along Y axis
            {
                _lineStart[i] = new Vector2(x * _cellSize.x, 0);                // Set start point
                _lineEnd[i] = new Vector2(x * _cellSize.x, _gridSize.y * _cellSize.y);  // Set end point
            }

            for (int y = 0; y <= _gridSize.y; y++, i++)                         // Calculate lines along X axis
            {
                _lineStart[i] = new Vector2(0, y * _cellSize.y);                // Set start point
                _lineEnd[i] = new Vector2(_gridSize.x * _cellSize.x, y * _cellSize.y);  // Set end point
            }
        }

        /// <summary>
        /// Draws the grid
        /// </summary>
        private static void DrawGrid()
        {
            GL.Begin(GL.LINES);                                                 // Mark beginnen of Graphics Library call
            GL.Color(Color.white);                                              // Set color for draw call

            for (int i = 0; i < _lineStart.Length; i++)
            {
                Vector3 start = _lineStart[i];                                  // Get start
                Vector3 end = _lineEnd[i];                                      // Get end

                GL.Vertex3(start.x, start.y, start.z);                          // Set start vertex
                GL.Vertex3(end.x, end.y, end.z);                                // Set end vertex
            }

            GL.End();                                                           // Mark end of Graphics Library call
        }
    }
}