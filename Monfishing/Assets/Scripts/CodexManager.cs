using UnityEngine;
using UnityEngine.UI;

public class CodexManager : MonoBehaviour
{
    public FishCodexEntry[] entries;  // 도감에 표시할 물고기들
    private bool[] caughtFishes;      // 낚은 여부 저장용

    public GameObject codexPanel;  // 도감 패널 오브젝트

    // 이건 도감 초기화 관련 기존 코드들 생략...

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
