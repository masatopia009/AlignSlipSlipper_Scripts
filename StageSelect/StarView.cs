using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarView : MonoBehaviour
{
    [SerializeField]
    private List<Image> _starImageList = new List<Image>();

    [Tooltip("スター無しの色")][SerializeField]
    private Color32 _emptyColor;
    [Tooltip("スターの通常色")][SerializeField]
    private Color32 _standardColor;
    [Tooltip("スターのエクストラ色")][SerializeField]
    private Color32 _extraColor;

    public void SetStar(int star)
    {
        int maxStar = _starImageList.Count;

        // スター数から色を変更
        // 左の星から順に色を変更する
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