using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    Animator animator;
    public float moveSpeed = 5.0f;

    void Start()
    {
        // 내 몸에 있는 애니메이터 기계를 가져온다
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float v = Input.GetAxis("Vertical"); // 위아래 키 (W/S)
        float h = Input.GetAxis("Horizontal"); // 좌우 키 (A/D)

        // 이동 벡터의 길이(크기)를 구함 (0 ~ 1 사이)
        // 움직이면 1에 가깝고, 안 움직이면 0
        Vector3 moveInput = new Vector3(h, 0, v);
        float inputMagnitude = moveInput.magnitude;

        // 1. 애니메이터에게 'Speed' 값을 전달! (여기가 핵심)
        // 절댓값(Mathf.Abs)을 쓰는 이유는 뒤로 가도(-1) 뛰는 모션은 같아야 하니까.
        animator.SetFloat("Speed", inputMagnitude);

        // 2. 실제 이동 (이전과 동일)
        transform.Translate(moveInput * moveSpeed * Time.deltaTime);
    }
}