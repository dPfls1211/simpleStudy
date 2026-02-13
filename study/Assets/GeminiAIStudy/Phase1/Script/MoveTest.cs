using UnityEngine;

public class MoveTest : MonoBehaviour
{
    public float speed = 5.0f; // 1초에 5만큼 이동하겠다는 뜻
    public GameObject bulletPrefab; // 총알 도장을 넣을 빈칸
    public AudioClip fireSound; // 총소리 파일 넣을 곳
    AudioSource audioSource;    // 소리 재생기
    void Start()
    {
        // 내 몸에 붙어있는 오디오 소스 가져오기
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        // 1. 사용자 입력 받기 (키보드 방향키)
        float h = Input.GetAxis("Horizontal"); // 좌우 (A/D or Arrow)
        float v = Input.GetAxis("Vertical");   // 상하 (W/S or Arrow)

        // 2. 방향 벡터 만들기
        // (h, v, 0) 벡터를 만듭니다.
        Vector3 dir = new Vector3(h, 0, v);

        // 3. 정규화 (대각선 이동 시 속도가 빨라지는 것 방지)
        // normalized 프로퍼티를 쓰면 유니티가 알아서 길이를 1로 만들어줍니다.
        dir = dir.normalized;

        // 4. 이동 적용
        // 위치 = 현재위치 + (방향 * 속도 * 시간)
        transform.position += dir * speed * Time.deltaTime;

        // 스페이스바를 누르면?
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Instantiate(원본, 위치, 회전)
            // 내 위치(transform.position)와 내 회전(transform.rotation)에 총알을 생성하라!
            Instantiate(bulletPrefab, transform.position, transform.rotation);// 소리 재생 (탕!)
            // PlayOneShot: 소리가 겹쳐도 끊기지 않고 탕탕탕 나옴
            if (fireSound != null)
                audioSource.PlayOneShot(fireSound);
        }
    }
}