using UnityEngine;
using UnityEngine.UI;

public class CodexManager : MonoBehaviour
{
    public FishCodexEntry[] entries;  // ������ ǥ���� ������
    private bool[] caughtFishes;      // ���� ���� �����

    public GameObject codexPanel;  // ���� �г� ������Ʈ

    // �̰� ���� �ʱ�ȭ ���� ���� �ڵ�� ����...

    public void ToggleCodex()
    {
        codexPanel.SetActive(!codexPanel.activeSelf);
    }
    void Start()
    {
        caughtFishes = new bool[entries.Length];
        LoadCodexData();
        UpdateUI();
    }

    public void MarkFishAsCaught(int index)
    {
        if (index < 0 || index >= caughtFishes.Length) return;

        if (!caughtFishes[index])
        {
            caughtFishes[index] = true;
            SaveCodexData();
            UpdateUI();
        }
    }

    void UpdateUI()
    {
        for (int i = 0; i < entries.Length; i++)
        {
            if (caughtFishes[i]) entries[i].ShowActual();
            else entries[i].ShowSilhouette();
        }
    }

    void SaveCodexData()
    {
        for (int i = 0; i < caughtFishes.Length; i++)
        {
            PlayerPrefs.SetInt("FishCaught_" + i, caughtFishes[i] ? 1 : 0);
        }
    }

    void LoadCodexData()
    {
        for (int i = 0; i < entries.Length; i++)
        {
            caughtFishes[i] = PlayerPrefs.GetInt("FishCaught_" + i, 0) == 1;
        }
    }
}
