using UnityEngine;
using TMPro;

public class Step : MonoBehaviour
{
    int step = 5;

    TextMeshProUGUI stepText;

    void Start()
    {
        stepText = GetComponent<TextMeshProUGUI>();
    }

    // イベントで呼び出す
    public void InitStep()
    {
        step = PlayManager.Instance.MaxStep;
        stepText.text = $"残り{step}手";
    }

    // イベントで呼び出す
    public void DecreaseStep()
    {
        step--;
        stepText.text = $"残り{step}手";
    }
}
