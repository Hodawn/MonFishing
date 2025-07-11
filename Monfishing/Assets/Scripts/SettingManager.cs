using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingManager : MonoBehaviour
{
    public GameObject settingsPanel;

    public void ToggleSettings()
    {
        settingsPanel.SetActive(!settingsPanel.activeSelf);
    }

    public void ResumeGame()
    {
        settingsPanel.SetActive(false);
    }

    public void GoToStartScene()
    {
        SceneManager.LoadScene("StartScene"); // 씬 이름이 정확히 일치해야 함!
    }

    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // 에디터에서 실행 중지용
#endif
    }
}
