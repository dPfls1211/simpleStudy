using UnityEngine;

[System.Serializable]
public class GridNode
{
    public Vector2Int gridPosition;
    public Vector3 worldPosition;
    public bool isWalkable = true;

    // A* 알고리즘용 변수들
    public int gCost; // 시작점으로부터의 비용
    public int hCost; // 목표점까지의 추정 비용
    public int fCost => gCost + hCost; // 총 비용
    public GridNode parent;

    public GridNode(Vector2Int gridPos, Vector3 worldPos)
    {
        gridPosition = gridPos;
        worldPosition = worldPos;
    }
}