using UnityEngine;

public class FadeOut : MonoBehaviour
{
    [Tooltip("フェード")][SerializeField]
    private Fade _fade;
    [Tooltip("フェードアウト時間")][SerializeField]
    private float _fadeOutTime = 0.25f;

    void Start()
    {
        _fade.FadeOut(_fadeOutTime);
    }
}
