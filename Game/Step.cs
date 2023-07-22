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

    // �C�x���g�ŌĂяo��
    public void InitStep()
    {
        step = PlayManager.Instance.MaxStep;
        stepText.text = $"�c��{step}��";
    }

    // �C�x���g�ŌĂяo��
    public void DecreaseStep()
    {
        step--;
        stepText.text = $"�c��{step}��";
    }
}
