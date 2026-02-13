using UnityEngine;
using TMPro; // 텍스트메쉬프로를 쓰려면 이게 필요함!
using UnityEngine.SceneManagement; // 씬 관리자 (리스타트용)

public class GameManager : MonoBehaviour
{
    // static: 어디서든 GameManager.Instance로 나를 부를 수 있게 함 (싱글톤 간이 버전)
    public static GameManager Instance;

    public TextMeshProUGUI scoreText; // UI 연결할 변수
    public GameObject gameOverPanel; // 패널 연결할 변수
    int score = 0; // 실제 점수 데이터
    bool isGameOver = false; // 상태 플래그

    void Awake()
    {
        // "내가 바로 그 유일한 매니저다"라고 선언
        Instance = this;
    }

    // 점수 올리는 함수 (외부에서 부를 예정)
    public void AddScore(int amount)
    {
        if (isGameOver) return; // 죽었으면 점수 안 오름
        score += amount;
        // UI 갱신: 숫자를 글자로 바꿔서 집어넣음
        scoreText.text = "Score: " + score.ToString();
    }
    // 게임 오버 함수 (공개)
    public void GameOver()
    {
        isGameOver = true;

        // 1. 패널을 켠다
        gameOverPanel.SetActive(true);

        // 2. 시간을 멈춘다 (모든 움직임 정지)
        Time.timeScale = 0f;
    }

    // 재시작 함수 (버튼에 연결할 것)
    public void RestartGame()
    {
        // 1. 시간을 다시 흐르게 한다 (매우 중요! 안 하면 재시작해도 멈춰있음)
        Time.timeScale = 1f;

        // 2. 현재 씬을 다시 로드한다 (초기화)
        // SceneManager.GetActiveScene().name : 현재 씬의 이름
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}