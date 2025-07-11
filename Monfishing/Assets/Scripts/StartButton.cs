using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    // ���� ���� ��ư���� ȣ��
    public void OnStartButtonClick()
    {
        SceneManager.LoadScene("PlayScene"); // PlayScene���� �̵�
    }

    // ���� ���� ��ư���� ȣ��
    public void OnQuitButtonClick()
    {
        Debug.Log("���� ����");
        Application.Quit(); // ���� ���Ͽ����� ����� (�����Ϳ����� ���� �� ��)
    }
}
