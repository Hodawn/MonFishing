using UnityEngine;
using UnityEngine.UI;

public class FishCodexEntry : MonoBehaviour
{
    public Image fishImage;           // ������ �̹��� ������Ʈ
    public Sprite silhouetteSprite;  // ��� �� �̹��� (�� ������� �Ƿ翧)
    public Sprite actualSprite;      // ���� �� �̹��� (�� ������� ���� ���)

    public void ShowSilhouette()
    {
        fishImage.sprite = silhouetteSprite;
    }

    public void ShowActual()
    {
        fishImage.sprite = actualSprite;
    }
}
