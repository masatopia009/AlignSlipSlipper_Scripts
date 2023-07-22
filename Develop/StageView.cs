using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StageView : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText;

    public void SetScoreText(string scoreText)
    {
        this.scoreText.text = scoreText;
    }
}
