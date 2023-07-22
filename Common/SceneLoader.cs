using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UniRx;
using System;

[RequireComponent(typeof(Button))]
public class SceneLoader : MonoBehaviour
{
    [Tooltip("遷移したいシーン名")][SerializeField]
    private string _targetScene;
    [Tooltip("フェード用キャンバス")][SerializeField]
    private Fade _fadeCanvas;
    [Tooltip("フェードイン時間（+0.05秒）")][SerializeField]
    private float _fadeInTime = 0.25f;

    void Start()
    {
        // ボタンを押されたときに呼ぶ関数を指定
        Button button = GetComponent<Button>();
        button.onClick.AddListener(LoadScene);
    }

    // 指定されたシーンをロード
    public void LoadScene()
    {
        // フェードイン
        if (_fadeCanvas != null)
        {
            _fadeCanvas.FadeIn(_fadeInTime);
        }
        
        // フェードインを待ってからシーン遷移（少しだけ間を持たせないとフェードインが途切れる）
        Observable.Timer(TimeSpan.FromSeconds(_fadeInTime + 0.05f))
            .Subscribe(_ => SceneManager.LoadScene(_targetScene));
    }
}
