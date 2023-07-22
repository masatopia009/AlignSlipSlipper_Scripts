using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StageInfoView : MonoBehaviour
{
    [Tooltip("�T���l�C���p�C���[�W")][SerializeField]
    Image _thumbnailImage;
    [Tooltip("�x�X�g����")][SerializeField]
    TextMeshProUGUI _bestDistanceText;
    [Tooltip("POP�S��")][SerializeField]
    GameObject _bestRotatePop;
    [Tooltip("�x�X�g��]�p�e�L�X�g")][SerializeField]
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
        // �X�^�[����2�ȉ��̂Ƃ��͂�肱�ݗv�f���B��
        if (star <= 2) _bestRotatePop.SetActive(false);
        else           _bestRotatePop.SetActive(true);
    }

    public void SetBestRotate(int bestRotate)
    {
        _bestRotateText.text = string.Format("�����̃Y���F{0:D3}��", bestRotate);
    }
}
