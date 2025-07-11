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
        SceneManager.LoadScene("StartScene"); // �� �̸��� ��Ȯ�� ��ġ�ؾ� ��!
    }

    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // �����Ϳ��� ���� ������
#endif
    }
}
