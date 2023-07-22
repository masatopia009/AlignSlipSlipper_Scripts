using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UniRx;
using System;

public class Opening : MonoBehaviour
{
    [Header("アニメーション対象")]
    [SerializeField]
    private GameObject _rightSlipper;
    [SerializeField]
    private GameObject _arrow;
    [SerializeField]
    private GameObject _rightGoal;

    [Header("アニメーション後の処理用")]
    [Tooltip("遷移したいシーン名")][SerializeField]
    private string _targetScene;
    [Tooltip("フェード用キャンバス")][SerializeField]
    private Fade _fadeCanvas;
    [Tooltip("フェードイン時間（+0.05秒）")][SerializeField]
    private float _fadeInTime = 0.25f;

    [Header("効果音")]
    [SerializeField]
    private AudioSource _audioSource;
    [Tooltip("矢印左回転")][SerializeField]
    private AudioClip _arrowLeftRotateSe;
    [Tooltip("矢印右回転")][SerializeField]
    private AudioClip _arrowRightRotateSe;
    [Tooltip("ショット")][SerializeField]
    private AudioClip _slipperShotSe;
    [Tooltip("ゴール")][SerializeField]
    private AudioClip _slipperGoalSe;

    void Start()
    {
        ArrowSwing();
    }

    private void ArrowSwing()
    {
        // スリッパとゴール間のラジアンを求める
        Vector3 diff = _rightGoal.transform.position - _rightSlipper.transform.position;
        float rad = Mathf.Atan2(diff.y, diff.x);
        // ラジアンをディグリー（角度）に直す
        // unityとmathfで基準線が90度ズレているので修正のために -90
        float deg = rad * Mathf.Rad2Deg - 90;

        Sequence arrowSequence = DOTween.Sequence();

        // 先ずは矢印のアニメーション
        arrowSequence
            // 矢印の位置調整と描画
            .AppendInterval(1.0f)
            .AppendCallback(() => ArrowSetting())
            // 左回転
            .AppendCallback(() => _audioSource.PlayOneShot(_arrowLeftRotateSe))
            .Append(_arrow.transform.DORotate(new Vector3(0, 0, 60), 0.5f).SetEase(Ease.OutQuad))
            // 右回転
            .AppendInterval(0.25f)
            .AppendCallback(() => _audioSource.PlayOneShot(_arrowRightRotateSe))
            .Append(_arrow.transform.DORotate(new Vector3(0, 0, -30), 0.5f).SetEase(Ease.OutQuad))
            // ゴールの方を向く
            .AppendInterval(0.25f)
            .Append(_arrow.transform.DORotate(new Vector3(0, 0, deg), 0.5f).SetEase(Ease.OutQuad))
            // 矢印を消す、次のアニメーションへ移動
            .AppendInterval(0.25f)
            .AppendCallback(() =>
            {
                _arrow.SetActive(false);
                SlipperShot();
            });

        arrowSequence.Play();
    }

    private void SlipperShot()
    {
        Sequence slipperSequence = DOTween.Sequence();

        // 次はスリッパのアニメーション   
        slipperSequence
            // 回転させながら飛ばす
            .AppendCallback(() =>
            {
                _audioSource.time = 1.2f;
                _audioSource.PlayOneShot(_slipperShotSe);
                _audioSource.time = 0f;
            })
            .Append(_rightSlipper.transform.DOMove(_rightGoal.transform.position, 0.8f).SetEase(Ease.OutQuad))
            .Join(_rightSlipper.transform.DORotate(new Vector3(0, 0, 720), 0.8f, RotateMode.FastBeyond360).SetEase(Ease.OutQuad))
            // 止まったらシーンをロード
            .AppendCallback(() =>
            {
                _audioSource.Stop();
                _audioSource.PlayOneShot(_slipperGoalSe);
            })
            .AppendInterval(1.0f)
            .AppendCallback(() => LoadScene());
    }

    private void LoadScene()
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

    private void ArrowSetting()
    {
        _arrow.transform.position = _rightSlipper.transform.position;
        _arrow.SetActive(true);
    }
}
