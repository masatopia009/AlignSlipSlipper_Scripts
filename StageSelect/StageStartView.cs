using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UniRx;
using System;

public class StageStartView : MonoBehaviour
{
    [Header("ステージのロード")]
    [Tooltip("スタートボタン")][SerializeField]
    private Button _startButton;
    [Tooltip("ロードするステージ名の接頭辞（ex. Stage0）")][SerializeField]
    private string _STAGE_PREFIX = "Stage";
    [Tooltip("フェード用キャンバス")][SerializeField]
    private Fade _fadeCanvas;
    [Tooltip("フェードイン時間（+0.05秒）")][SerializeField]
    private float _fadeInTime = 0.25f;

    [Header("ステージ番号のPOP")]
    [Tooltip("ステージ番号POP")][SerializeField]
    private GameObject _stageNumPop;
    [Tooltip("ステージ番号用テキスト")][SerializeField]
    private TextMeshProUGUI _stageNumText;
    [Tooltip("POPの接頭辞（ex. ステージ00）")][SerializeField]
    private string _POP_PREFIX = "ステージ";

    private int _selectedStageNum;

    private void Start()
    {
        // ステージが選ばれていない状態ではボタンを押せない/POPは見せない
        _startButton.interactable = false;
        _stageNumPop.SetActive(false);

        _startButton.onClick.AddListener(StageStart);
    }

    // ステージ番号をセットして表示
    public void SetStageNum(int stageNum)
    {
        // ステージ番号が選ばれたらボタンを押せるようにする
        _startButton.interactable = true;

        // POPも表示する
        _stageNumText.text = _POP_PREFIX + string.Format("{0:D2}", stageNum);
        _stageNumPop.SetActive(true);

        // ステージロード用にステージ番号を控える
        _selectedStageNum = stageNum;
    }

    // スタートボタンを押されたらステージをロード
    public void StageStart()
    {
        // ロードするステージの名前を組み立てる
        string load = _STAGE_PREFIX + _selectedStageNum.ToString();

        // フェードイン
        if (_fadeCanvas != null)
        {
            _fadeCanvas.FadeIn(_fadeInTime);
        }

        // フェードインを待ってからシーン遷移（少しだけ間を持たせないとフェードインが途切れる）
        Observable.Timer(TimeSpan.FromSeconds(_fadeInTime + 0.05f))
            .Subscribe(_ => SceneManager.LoadScene(load));
    }
}
