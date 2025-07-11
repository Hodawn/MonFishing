using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    // 게임 시작 버튼에서 호출
    public void OnStartButtonClick()
    {
        SceneManager.LoadScene("PlayScene"); // PlayScene으로 이동
    }

    // 게임 종료 버튼에서 호출
    public void OnQuitButtonClick()
    {
        Debug.Log("게임 종료");
        Application.Quit(); // 실행 파일에서만 종료됨 (에디터에서는 종료 안 됨)
    }
}
