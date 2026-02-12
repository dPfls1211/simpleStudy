// GridManager.cs - 그리드 관리 클래스
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [Header("Grid Settings")]
    public int gridWidth = 50;
    public int gridHeight = 50;
    public float cellSize = 1f;
    public LayerMask obstacleLayer = 1;

    [Header("Visualization")]
    public bool showGrid = true;
    public Color gridColor = Color.white;
    public Color obstacleColor = Color.red;
    public Color pathColor = Color.green;

    private GridNode[,] grid;
    private Vector3 gridOrigin;

    void Start()
    {
        CreateGrid();
    }

    void CreateGrid()
    {
        grid = new GridNode[gridWidth, gridHeight];
        gridOrigin = transform.position - new Vector3(gridWidth * cellSize * 0.5f, 0, gridHeight * cellSize * 0.5f);

        for (int x = 0; x < gridWidth; x++)
        {
            for (int z = 0; z < gridHeight; z++)
            {
                Vector3 worldPosition = gridOrigin + new Vector3(x * cellSize, 0, z * cellSize);
                GridNode node = new GridNode(new Vector2Int(x, z), worldPosition);

                // 장애물 체크
                Collider[] obstacles = Physics.OverlapSphere(worldPosition, cellSize * 0.4f, obstacleLayer);
                node.isWalkable = obstacles.Length == 0;

                grid[x, z] = node;
            }
        }
    }

    public GridNode GetNodeFromWorldPosition(Vector3 worldPosition)
    {
        Vector3 localPosition = worldPosition - gridOrigin;
        int x = Mathf.RoundToInt(localPosition.x / cellSize);
        int z = Mathf.RoundToInt(localPosition.z / cellSize);

        if (x >= 0 && x < gridWidth && z >= 0 && z < gridHeight)
            return grid[x, z];

        return null;
    }

    public GridNode GetNode(int x, int z)
    {
        if (x >= 0 && x < gridWidth && z >= 0 && z < gridHeight)
            return grid[x, z];
        return null;
    }

    public GridNode[] GetNeighbors(GridNode node)
    {
        System.Collections.Generic.List<GridNode> neighbors = new System.Collections.Generic.List<GridNode>();

        for (int x = -1; x <= 1; x++)
        {
            for (int z = -1; z <= 1; z++)
            {
                if (x == 0 && z == 0) continue;

                int checkX = node.gridPosition.x + x;
                int checkZ = node.gridPosition.y + z;

                GridNode neighbor = GetNode(checkX, checkZ);
                if (neighbor != null)
                    neighbors.Add(neighbor);
            }
        }

        return neighbors.ToArray();
    }

    void OnDrawGizmos()
    {
        if (!showGrid || grid == null) return;

        for (int x = 0; x < gridWidth; x++)
        {
            for (int z = 0; z < gridHeight; z++)
            {
                GridNode node = grid[x, z];
                if (node == null) continue;

                Gizmos.color = node.isWalkable ? gridColor : obstacleColor;
                Gizmos.DrawWireCube(node.worldPosition, Vector3.one * cellSize * 0.9f);
            }
        }
    }
}
