using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarView : MonoBehaviour
{
    [SerializeField]
    private List<Image> _starImageList = new List<Image>();

    [Tooltip("�X�^�[�����̐F")][SerializeField]
    private Color32 _emptyColor;
    [Tooltip("�X�^�[�̒ʏ�F")][SerializeField]
    private Color32 _standardColor;
    [Tooltip("�X�^�[�̃G�N�X�g���F")][SerializeField]
    private Color32 _extraColor;

    public void SetStar(int star)
    {
        int maxStar = _starImageList.Count;

        // �X�^�[������F��ύX
        // ���̐����珇�ɐF��ύX����
        for (int index = 1; index <= maxStar; index++)
        {
            if (index + maxStar <= star) _starImageList[index - 1].color = _extraColor;
            else if (index <= star)      _starImageList[index - 1].color = _standardColor;
            else                         _starImageList[index - 1].color = _emptyColor;
        }
    }
}



public enum StarColor
{
    EmptyColor,
    StandardColor,
    ExtraColor
}