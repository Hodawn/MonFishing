using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEditor.Experimental.GraphView;

public class FishingManager : MonoBehaviour
{
    public Image characterImage;          // 캐릭터 이미지
    public Sprite normalSprite;           // 기본 캐릭터
    public Sprite fishingSprite;          // 낚시할 때 캐릭터
    public Sprite caughtSprite;           // 낚시 후 캐릭터

    public Button fishButton;             // 낚시 버튼
    public GameObject exclamationMark;    // 느낌표 이미지
    public Slider gaugeBar;               // 게이지바
    public Image resultFishLargeImage; // 큰 이미지
    public Image resultFishSmallImage; // 작은 이미지
    public Sprite[] fishSprites;          // 물고기 그림 (S, A, B, C)

    private bool canReact = false;
    private float gauge = 0f;
    private bool isFishing = false;

    [Header("Fish Probabilities")]
    [Range(0f, 1f)] public float sRate = 0.05f; // 5%
    [Range(0f, 1f)] public float aRate = 0.15f; // 15%
    [Range(0f, 1f)] public float bRate = 0.3f;  // 30%
    [Range(0f, 1f)] public float cRate = 1f;

    void Start()
    {
        exclamationMark.SetActive(false);
        gaugeBar.gameObject.SetActive(false);

        // 두 이미지 모두 꺼주기!
        resultFishLargeImage.gameObject.SetActive(false);
        resultFishSmallImage.gameObject.SetActive(false);

        Debug.Log("시작함");
    }

    void OnFishButtonClick()
    {
        if (isFishing == false)
        {
            StartCoroutine(StartFishing());
        }
        else if (canReact)
        {
            canReact = false;
            StopAllCoroutines(); // 느낌표 대기 중지
            StartCoroutine(StartGauge());
        }
        else if (gaugeBar.gameObject.activeSelf)
        {
            IncreaseGauge();
        }
    }

    IEnumerator StartFishing()
    {
        isFishing = true;
        characterImage.sprite = fishingSprite;

        yield return new WaitForSeconds(Random.Range(2f, 4f)); // 랜덤 시간 기다리기

        exclamationMark.SetActive(true);
        canReact = true;

        yield return new WaitForSeconds(1.5f); // 반응할 시간
        if (canReact)
        {
            // 반응 못 했음
            exclamationMark.SetActive(false);
            characterImage.sprite = normalSprite;
            isFishing = false;
            canReact = false;
        }
    }

    IEnumerator StartGauge()
    {
        exclamationMark.SetActive(false);
        gaugeBar.gameObject.SetActive(true);
        gauge = 0f;
        gaugeBar.value = 0f;

        // 낚싯대를 든 캐릭터 이미지로 변경!
        characterImage.sprite = caughtSprite;

        float timeLimit = 3f;
        float timer = 0f;

        while (timer < timeLimit && gauge < 1f)
        {
            gaugeBar.value = gauge;
            timer += Time.deltaTime;
            yield return null;
        }

        gaugeBar.gameObject.SetActive(false);

        // 결과물 보여주기 (캐릭터 이미지는 유지)
        ShowRandomFish();
    }

    void IncreaseGauge()
    {
        gauge += 0.1f;
        gauge = Mathf.Clamp01(gauge);
        gaugeBar.value = gauge;
    }

    void ShowRandomFish()
    {
        Sprite selectedFish = ChooseFishByProbability();

        // 두 이미지에 같은 물고기 스프라이트 넣기
        resultFishLargeImage.sprite = selectedFish;
        resultFishSmallImage.sprite = selectedFish;

        // 이미지 보여주기
        resultFishLargeImage.gameObject.SetActive(true);
        resultFishSmallImage.gameObject.SetActive(true);

        StartCoroutine(HideResultFish());
    }

    IEnumerator HideResultFish()
    {
        yield return new WaitForSeconds(2f);

        resultFishLargeImage.gameObject.SetActive(false);
        resultFishSmallImage.gameObject.SetActive(false);

        characterImage.sprite = normalSprite;
        isFishing = false;
    }

    Sprite ChooseFishByProbability()
    {
        float r = Random.Range(0f, 1f);

        if (r < sRate)
            return fishSprites[0]; // S급
        else if (r < sRate + aRate)
            return fishSprites[1]; // A급
        else if (r < sRate + aRate + bRate)
            return fishSprites[2]; // B급
        else if (r < sRate + bRate + cRate)
            return fishSprites[3]; // C급
        else
            return fishSprites[4];  //D급

    }
}
