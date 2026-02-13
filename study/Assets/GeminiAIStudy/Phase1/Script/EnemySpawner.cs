using UnityEngine;
using System.Collections; // 코루틴을 쓰려면 이게 필요함

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // 적 프리팹 연결할 곳
    public float spawnInterval = 2.0f; // 2초마다 생성

    void Start()
    {
        // 코루틴 시작! (SpawnRoutine 함수를 실행해라)
        StartCoroutine(SpawnRoutine());
    }

    // 이름이 IEnumerator인 함수 (코루틴)
    IEnumerator SpawnRoutine()
    {
        // 무한 루프 (게임이 끝날 때까지)
        while (true)
        {
            // 1. 랜덤 위치 계산 (-5 ~ 5 사이)
            float randomX = Random.Range(-5.0f, 5.0f);
            Vector3 spawnPos = new Vector3(randomX, 0, 10); // z=10 (플레이어 앞쪽 멀리)

            // 2. 적 생성 (Instantiate)
            // Quaternion.identity는 "회전 없음"이라는 뜻
            Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

            // 3. 대기 (아주 중요!)
            // 2초 동안 이 함수를 멈춤. 그 동안 게임은 계속 돌아감.
            // 이게 없으면 무한 루프 때문에 게임이 멈춤(렉)!
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}