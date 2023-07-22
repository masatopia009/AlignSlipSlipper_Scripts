using UnityEngine;

public class DistanceSliderManager : MonoBehaviour
{
    [SerializeField]
    DistanceSlider front;
    [SerializeField]
    Color32 color1;
    [SerializeField]
    DistanceSlider back;
    [SerializeField]
    Color32 color2;

    // Start is called before the first frame update
    void Start()
    {
        FrontSetting(front);
        front.FillImage.CrossFadeColor(color1, 0.5f, true, true);
        front.GoalImage.CrossFadeAlpha(0, 0.5f, true);

        BackSetting(back);
        back.FillImage.CrossFadeColor(color2, 0.5f, true, true);
        back.GoalImage.CrossFadeAlpha(1, 0.5f, true);
    }

    // Update is called once per frame
    void Update()
    {
        if (back.Slider.value < front.Slider.value)
        {
            SwapFrontBack();
        }
    }

    private void SwapFrontBack()
    {
        var oldFront = front;
        var oldBack = back;

        front = FrontSetting(oldBack);
        back = BackSetting(oldFront);
    }

    private DistanceSlider FrontSetting(DistanceSlider target)
    {
        target.Slider.transform.SetAsLastSibling();
        target.BackgroundImage.CrossFadeAlpha(0, 0.5f, true);
        target.GoalImage.CrossFadeAlpha(0, 0.5f, true);
        return target;
    }

    // ‚©‚Â‚Ä‘O‚¾‚Á‚½‚à‚Ì‚ð‰œ‚Éˆø‚Áž‚Ü‚¹‚é
    private DistanceSlider BackSetting(DistanceSlider target)
    {
        target.BackgroundImage.CrossFadeAlpha(1, 0.5f, true);
        target.GoalImage.CrossFadeAlpha(1, 0.5f, true);
        return target;
    }
}
