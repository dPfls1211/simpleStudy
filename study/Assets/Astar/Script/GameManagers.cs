using UnityEngine;

public class GameManagers : MonoBehaviour
{
    [Header("Scene Setup")]
    public GameObject characterPrefab;
    public GameObject obstaclePrefab;
    public int numberOfObstacles = 10;

    private GridManager gridManager;
    private GameObject character;

    void Start()
    {
        SetupScene();
    }

    void SetupScene()
    {
        // GridManager 설정
        gridManager = FindObjectOfType<GridManager>();
        if (gridManager == null)
        {
            GameObject gridObj = new GameObject("GridManager");
            gridManager = gridObj.AddComponent<GridManager>();
        }

        // 캐릭터 생성
        if (characterPrefab != null && character == null)
        {
            character = Instantiate(characterPrefab);
            character.transform.position = Vector3.zero;

            if (character.GetComponent<CharacterController>() == null)
                character.AddComponent<CharacterController>();
        }

        // A* Pathfinder 설정
        if (FindObjectOfType<AStarPathfinder>() == null)
        {
            GameObject pathfinderObj = new GameObject("AStarPathfinder");
            pathfinderObj.AddComponent<AStarPathfinder>();
        }

        // 랜덤 장애물 생성
        StartCoroutine(CreateObstaclesAfterGrid());
    }

    System.Collections.IEnumerator CreateObstaclesAfterGrid()
    {
        yield return new WaitForSeconds(0.1f); // 그리드가 생성될 때까지 대기

        if (obstaclePrefab != null)
        {
            for (int i = 0; i < numberOfObstacles; i++)
            {
                Vector3 randomPos = new Vector3(
                    Random.Range(-gridManager.gridWidth * 0.4f, gridManager.gridWidth * 0.4f),
                    0.5f,
                    Random.Range(-gridManager.gridHeight * 0.4f, gridManager.gridHeight * 0.4f)
                );

                Instantiate(obstaclePrefab, randomPos, Quaternion.identity);
            }

            // 장애물 생성 후 그리드 재생성
            yield return new WaitForSeconds(0.1f);
            gridManager.SendMessage("CreateGrid");
        }
    }
}