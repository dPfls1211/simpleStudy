using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 10.0f;

    void Start()
    {
        // 생성된 지 3초 뒤에 스스로 파괴됨 (아주 중요!)
        // 이걸 안 하면 총알이 수천 개 쌓여서 게임이 렉걸림 (메모리 누수 방지)
        Destroy(gameObject, 3.0f);
    }

    void Update()
    {
        // 매 프레임마다 '자신의 앞쪽'으로 이동
        // transform.forward: 이 물체가 바라보는 앞쪽 방향 (벡터)
        // Space.World: 월드 좌표계 기준으로 이동
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    // 유니티 약속: "Is Trigger"가 체크된 놈이랑 부딪히면 이 함수가 자동 실행됨
    void OnTriggerEnter(Collider other)
    {
        // 부딪힌 놈(other)의 이름표(Tag)가 "Enemy"인가?
        if (other.CompareTag("Enemy"))
        {
            // GameManager야, 점수 10점 올려줘! (static 덕분에 찾을 수 있음)
            GameManager.Instance.AddScore(10);

            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}