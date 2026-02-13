// PlayerCollision.cs 라고 짓고 플레이어에게 붙이세요
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    void OnCollisionEnter(Collision collision) // Trigger가 아니라 물리 충돌(Collision)
    {
        // 적과 부딪혔다면?
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // 게임 매니저에게 "나 죽었어"라고 알림
            GameManager.Instance.GameOver();
        }
    }
}