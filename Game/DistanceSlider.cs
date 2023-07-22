using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DistanceSlider : MonoBehaviour
{
    [SerializeField]
    Slider slider;
    public Slider Slider
    {
        get { return slider; }
    }

    [SerializeField] 
    Image backgroundImage;
    public Image BackgroundImage
    {
        get { return backgroundImage; }
    }

    [SerializeField] 
    Image fillImage;
    public Image FillImage
    {
        get { return fillImage; }
    }

    [SerializeField] 
    Image goalImage;
    public Image GoalImage
    {
        get { return goalImage; }
    }

    [SerializeField]
    TextMeshProUGUI distanceText;

    public void UpdateDistance(Distance currentDis, Distance startDis)
    {
        // スライダーの初期値は20
        slider.value = (1 - (float)currentDis.SumCentiUnit / startDis.SumCentiUnit) * 100 + 20;
        distanceText.text = $"{currentDis.MeterValue}m{currentDis.CentiValue}cm";
    }
}
