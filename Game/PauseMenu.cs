using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UniRx;
using System;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] 
    GameObject pauseMenu;
    [SerializeField]
    AudioClip pauseSE;
    [SerializeField]
    AudioClip retrySE;
    [SerializeField]
    AudioClip returnSE;

    [Tooltip("フェード用キャンバス")][SerializeField]
    private Fade _fadeCanvas;
    [Tooltip("フェードイン時間（+0.05秒）")][SerializeField]
    private float _fadeInTime = 0.25f;

    private bool isOn = false;

    private void Start()
    {
        Time.timeScale = 1f;
        GetComponent<Button>().onClick.AddListener(OnPauseMenu);
        pauseMenu.SetActive(isOn);
    }

    public void OnPauseMenu()
    {
        PlaySeDontDestroy.Instance.PlaySound(pauseSE);
        isOn = !isOn;
        pauseMenu.SetActive(isOn);
        if (isOn)
        {
            Time.timeScale = 0f;
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(null);
            Time.timeScale = 1f;
        }
    }

    public void Retry()
    {
        PlaySeDontDestroy.Instance.PlaySound(retrySE);
        Time.timeScale = 1f;
        Load(SceneManager.GetActiveScene().name);
    }

    public void StageSelect()
    {
        PlaySeDontDestroy.Instance.PlaySound(returnSE);
        Time.timeScale = 1f;
        Load("StageSelect");
    }

    private void Load(string sceneName)
    {
        // フェードイン
        if (_fadeCanvas != null)
        {
            _fadeCanvas.FadeIn(_fadeInTime);
        }

        // フェードインを待ってからシーン遷移（少しだけ間を持たせないとフェードインが途切れる）
        Observable.Timer(TimeSpan.FromSeconds(_fadeInTime + 0.05f))
            .Subscribe(_ => SceneManager.LoadScene(sceneName));
    }

    public void UnInteractive()
    {
        GetComponent<Button>().interactable = false;
    }
}
