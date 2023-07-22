using UnityEngine;

public class FadeOut : MonoBehaviour
{
    [Tooltip("�t�F�[�h")][SerializeField]
    private Fade _fade;
    [Tooltip("�t�F�[�h�A�E�g����")][SerializeField]
    private float _fadeOutTime = 0.25f;

    void Start()
    {
        _fade.FadeOut(_fadeOutTime);
    }
}
