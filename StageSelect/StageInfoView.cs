using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StageInfoView : MonoBehaviour
{
    [Tooltip("サムネイル用イメージ")][SerializeField]
    Image _thumbnailImage;
    [Tooltip("ベスト距離")][SerializeField]
    TextMeshProUGUI _bestDistanceText;
    [Tooltip("POP全体")][SerializeField]
    GameObject _bestRotatePop;
    [Tooltip("ベスト回転用テキスト")][SerializeField]
    TextMeshProUGUI _bestRotateText;

    private void Start()
    {
        _bestRotatePop.SetActive(false);
    }

    public void SetThumbnail(Sprite thumbnail)
    {
        _thumbnailImage.sprite = thumbnail;
    }

    public void SetBestDistance(Distance bestDistance)
    {
        _bestDistanceText.text = 
            string.Format("{0:D1}m{1:D2}cm", bestDistance.MeterValue, bestDistance.CentiValue); ;
    }

    public void ActivateRotatePopByStar(int star)
    {
        // スター数が2以下のときはやりこみ要素を隠す
        if (star <= 2) _bestRotatePop.SetActive(false);
        else           _bestRotatePop.SetActive(true);
    }

    public void SetBestRotate(int bestRotate)
    {
        _bestRotateText.text = string.Format("向きのズレ：{0:D3}°", bestRotate);
    }
}
