using System.Collections;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

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
    [Header("Failure")]
    public Sprite failSprite;              // 실패한 캐릭터 얼굴
    public GameObject failImage;           // "낚시 실패!" 이미지 오브젝트

    [Header("Fish Probabilities")]
    [Range(0f, 1f)] public float sRate = 0.05f; // 5%
    [Range(0f, 1f)] public float aRate = 0.15f; // 15%
    [Range(0f, 1f)] public float bRate = 0.3f;  // 30%
    [Range(0f, 1f)] public float cRate = 1f;

    private bool isBusy = false; // 결과 보여주는 중인지 여부
    public TMP_Text fishNameText;
    private string[] fishNames = new string[]
    {
    "루비",    // Element 0
    "두기몬",  // Element 1
    "배스",         // Element 2
    "쏘가리",        // Element 3
    "EOAG 캔"

    };



    void Start()
    {
        exclamationMark.SetActive(false);
        gaugeBar.gameObject.SetActive(false);

        // 두 이미지 모두 꺼주기!
        resultFishLargeImage.gameObject.SetActive(false);
        resultFishSmallImage.gameObject.SetActive(false);

        Debug.Log("시작함");
    }

    public void OnFishButtonClick()
    {
        // 결과 보여주는 중일 때는 무시!
        if (isBusy) return;

        if (!isFishing)
        {
            StartCoroutine(StartFishing());
        }
        else if (canReact)
        {
            canReact = false;
            StopAllCoroutines();
            StartCoroutine(StartGauge());
        }
        else if (gaugeBar.gameObject.activeSelf)
        {
            IncreaseGauge();
        }
        else
        {
            // 낚시 진행 중인데 잘못 눌렀을 경우 → 실패 처리
            TriggerFishingFail();
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

            TriggerFishingFail();
        }
    }

    IEnumerator StartGauge()
    {
        exclamationMark.SetActive(false);
        gaugeBar.gameObject.SetActive(true);
        gauge = 0f;
        gaugeBar.value = 0f;

        characterImage.sprite = caughtSprite;

        float timeLimit = 2.5f; // ⏳ 제한 시간 (초)
        float timer = 0f;

        while (timer < timeLimit)
        {
            gaugeBar.value = gauge;
            timer += Time.deltaTime;

            // 게이지가 다 찼으면 성공
            if (gauge >= 1f)
            {
                gaugeBar.gameObject.SetActive(false);
                ShowRandomFish();
                yield break; // 함수 종료
            }

            yield return null;
        }

        // 제한 시간 종료 → 실패 처리
        gaugeBar.gameObject.SetActive(false);
        TriggerFishingFail();
    }

    void IncreaseGauge()
    {
        gauge += 0.1f;
        gauge = Mathf.Clamp01(gauge);
        gaugeBar.value = gauge;
    }

    void ShowRandomFish()
    {
        isBusy = true; // 결과 보여주는 중임!

        int fishIndex = ChooseFishIndexByProbability();

        Sprite selectedFish = fishSprites[fishIndex];
        string selectedFishName = fishNames[fishIndex];

        // 두 이미지에 같은 물고기 스프라이트 넣기
        resultFishLargeImage.sprite = selectedFish;
        resultFishSmallImage.sprite = selectedFish;

        // 이미지 보여주기
        resultFishLargeImage.gameObject.SetActive(true);
        resultFishSmallImage.gameObject.SetActive(true);

        // 🐟 이름 표시
        fishNameText.text = selectedFishName;
        fishNameText.gameObject.SetActive(true);

        StartCoroutine(HideResultFish());
    }

    IEnumerator HideResultFish()
    {
        yield return new WaitForSeconds(2f);

        resultFishLargeImage.gameObject.SetActive(false);
        resultFishSmallImage.gameObject.SetActive(false);
        fishNameText.gameObject.SetActive(false);  // 👈 이름도 꺼주기

        characterImage.sprite = normalSprite;
        isFishing = false;
        isBusy = false; // 결과 다 보여줬으니 버튼 다시 활성화!
    }

    Sprite ChooseFishByProbability()
    {
        int index = ChooseFishIndexByProbability();
        return fishSprites[index];

    }
    void TriggerFishingFail()
    {
        StopAllCoroutines();  // 혹시 실행 중인 코루틴이 있다면 정지
        isFishing = false;
        canReact = false;

        // 실패 이미지 보여주기
        characterImage.sprite = failSprite;
        failImage.SetActive(true);

        // 몇 초 후 초기화
        StartCoroutine(ResetAfterFail());
    }

    IEnumerator ResetAfterFail()
    {
        yield return new WaitForSeconds(2f);

        failImage.SetActive(false);
        characterImage.sprite = normalSprite;
    }

    int ChooseFishIndexByProbability()
    {
        float rand = Random.value;
        if (rand < sRate)
            return 0; // S급
        else if (rand < sRate + aRate)
            return 1; // A급
        else if (rand < sRate + aRate + bRate)
            return 2; // B급
        else if(rand < sRate + aRate + bRate + cRate)
            return 3; // C급
        else
            return 4;
    }
}
