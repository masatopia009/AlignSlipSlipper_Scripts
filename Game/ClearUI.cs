using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClearUI : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI scoreText;
    [SerializeField]
    GameObject rotateUI;
    [SerializeField]
    TextMeshProUGUI rotateText;
    [SerializeField]
    List<Image> starImages;
    [SerializeField]
    Color32 starColor;
    [SerializeField]
    Color32 extraColor;
    [SerializeField]
    AudioClip clearSE;

    /// <summary>
    /// UI�\��
    /// </summary>
    public void ActiveUI()
    {
        PlaySeDontDestroy.Instance.PlaySound(clearSE);
        gameObject.SetActive(true);

        // �X�R�A�ݒ�
        Distance scoreDis = PlayManager.Instance.ScoreDis;
        string scoreStr = string.Format("�L�^ {0:D1}m{1:D2}cm", scoreDis.MeterValue, scoreDis.CentiValue);
        scoreText.text = scoreStr;

        // �X�^�[�ݒ�
        int star = PlayManager.Instance.Star;
        for (int i = 1; i <= starImages.Count; i++)
        {
            if (i + 3 <= star) starImages[i - 1].color = extraColor;
            else if (i <= star) starImages[i - 1].color = starColor;
            else starImages[i - 1].color = Color.black;
        }

        // �����̃Y���ݒ�
        int rotate = PlayManager.Instance.ScoreRotate;
        string rotateStr = string.Format("�����̃Y���F{0:D3}��", rotate);
        rotateText.text = rotateStr;
        if (star >= 3) rotateUI.SetActive(true);
        else rotateUI.SetActive(false);
    }
}
