using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    CharacterController controller;
    Animator animator;

    public float speed = 5.0f;
    public float jumpHeight = 2.0f;
    public float gravity = -9.81f;

    Vector3 velocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // 1. 땅에 닿아있는지 확인 및 중력 초기화
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // 2. 입력 받기 (수평 이동)
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;

        // 3. 점프 (땅에 있을 때만!)
        if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            Debug.Log("점프!");
        }

        // 4. 중력 누적
        velocity.y += gravity * Time.deltaTime;

        // 5. 최종 이동 (수평 이동과 수직 이동을 합침)
        // 수평 방향(move)에 속도를 곱해주고, 수직 방향(Y)은 velocity.y를 덮어씌웁니다.
        Vector3 finalMove = move * speed;
        finalMove.y = velocity.y;

        // Move()는 무조건 Update의 맨 마지막에 딱 한 번만!
        controller.Move(finalMove * Time.deltaTime);

        // 6. 애니메이션 연동 (수평 이동량만 계산)
        animator.SetFloat("Speed", move.magnitude);

        // 결과 확인용 로그
        // Debug.Log("현재 isGrounded 상태: " + controller.isGrounded);
    }
}