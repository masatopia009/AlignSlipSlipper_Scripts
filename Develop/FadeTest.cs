using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeTest : MonoBehaviour
{
    public Fade fade;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            fade.FadeIn(0.5f, () => print("�t�F�[�h�C������"));
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            fade.FadeOut(0.5f, () => print("�t�F�[�h�A�E�g����"));
        }
    }
}