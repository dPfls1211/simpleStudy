using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 3.0f;
    Transform target; // 플레이어의 위치

    void Start()
    {
        // 씬에 있는 'Player'라는 태그를 가진 놈을 찾아라! (플레이어 찾기)
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            target = playerObj.transform;
        }
    }

    void Update()
    {
        if (target == null) return; // 플레이어가 죽었으면 멈춤

        // 1. 방향 구하기 (플레이어 - 나)
        Vector3 dir = target.position - transform.position;
        dir.Normalize(); // 정규화 (방향만 남김)

        // 2. 이동 (플레이어 쪽으로)
        transform.position += dir * speed * Time.deltaTime;

        // 3. 회전 (플레이어 쳐다보기) - 부드럽게!
        // Quaternion.LookRotation(방향): 그 방향을 쳐다보는 회전값
        transform.rotation = Quaternion.LookRotation(dir);
    }
}