using UnityEngine;
using UnityEngine.UI;

public class FishCodexEntry : MonoBehaviour
{
    public Image fishImage;           // 보여줄 이미지 오브젝트
    public Sprite silhouetteSprite;  // 잡기 전 이미지 (그 물고기의 실루엣)
    public Sprite actualSprite;      // 잡은 후 이미지 (그 물고기의 실제 모습)

    public void ShowSilhouette()
    {
        fishImage.sprite = silhouetteSprite;
    }

    public void ShowActual()
    {
        fishImage.sprite = actualSprite;
    }
}
