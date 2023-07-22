using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UniRx;
using System;

public class Opening : MonoBehaviour
{
    [Header("�A�j���[�V�����Ώ�")]
    [SerializeField]
    private GameObject _rightSlipper;
    [SerializeField]
    private GameObject _arrow;
    [SerializeField]
    private GameObject _rightGoal;

    [Header("�A�j���[�V������̏����p")]
    [Tooltip("�J�ڂ������V�[����")][SerializeField]
    private string _targetScene;
    [Tooltip("�t�F�[�h�p�L�����o�X")][SerializeField]
    private Fade _fadeCanvas;
    [Tooltip("�t�F�[�h�C�����ԁi+0.05�b�j")][SerializeField]
    private float _fadeInTime = 0.25f;

    [Header("���ʉ�")]
    [SerializeField]
    private AudioSource _audioSource;
    [Tooltip("��󍶉�]")][SerializeField]
    private AudioClip _arrowLeftRotateSe;
    [Tooltip("���E��]")][SerializeField]
    private AudioClip _arrowRightRotateSe;
    [Tooltip("�V���b�g")][SerializeField]
    private AudioClip _slipperShotSe;
    [Tooltip("�S�[��")][SerializeField]
    private AudioClip _slipperGoalSe;

    void Start()
    {
        ArrowSwing();
    }

    private void ArrowSwing()
    {
        // �X���b�p�ƃS�[���Ԃ̃��W�A�������߂�
        Vector3 diff = _rightGoal.transform.position - _rightSlipper.transform.position;
        float rad = Mathf.Atan2(diff.y, diff.x);
        // ���W�A�����f�B�O���[�i�p�x�j�ɒ���
        // unity��mathf�Ŋ����90�x�Y���Ă���̂ŏC���̂��߂� -90
        float deg = rad * Mathf.Rad2Deg - 90;

        Sequence arrowSequence = DOTween.Sequence();

        // �悸�͖��̃A�j���[�V����
        arrowSequence
            // ���̈ʒu�����ƕ`��
            .AppendInterval(1.0f)
            .AppendCallback(() => ArrowSetting())
            // ����]
            .AppendCallback(() => _audioSource.PlayOneShot(_arrowLeftRotateSe))
            .Append(_arrow.transform.DORotate(new Vector3(0, 0, 60), 0.5f).SetEase(Ease.OutQuad))
            // �E��]
            .AppendInterval(0.25f)
            .AppendCallback(() => _audioSource.PlayOneShot(_arrowRightRotateSe))
            .Append(_arrow.transform.DORotate(new Vector3(0, 0, -30), 0.5f).SetEase(Ease.OutQuad))
            // �S�[���̕�������
            .AppendInterval(0.25f)
            .Append(_arrow.transform.DORotate(new Vector3(0, 0, deg), 0.5f).SetEase(Ease.OutQuad))
            // ���������A���̃A�j���[�V�����ֈړ�
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

        // ���̓X���b�p�̃A�j���[�V����   
        slipperSequence
            // ��]�����Ȃ����΂�
            .AppendCallback(() =>
            {
                _audioSource.time = 1.2f;
                _audioSource.PlayOneShot(_slipperShotSe);
                _audioSource.time = 0f;
            })
            .Append(_rightSlipper.transform.DOMove(_rightGoal.transform.position, 0.8f).SetEase(Ease.OutQuad))
            .Join(_rightSlipper.transform.DORotate(new Vector3(0, 0, 720), 0.8f, RotateMode.FastBeyond360).SetEase(Ease.OutQuad))
            // �~�܂�����V�[�������[�h
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
        // �t�F�[�h�C��
        if (_fadeCanvas != null)
        {
            _fadeCanvas.FadeIn(_fadeInTime);
        }

        // �t�F�[�h�C����҂��Ă���V�[���J�ځi���������Ԃ��������Ȃ��ƃt�F�[�h�C�����r�؂��j
        Observable.Timer(TimeSpan.FromSeconds(_fadeInTime + 0.05f))
            .Subscribe(_ => SceneManager.LoadScene(_targetScene));
    }

    private void ArrowSetting()
    {
        _arrow.transform.position = _rightSlipper.transform.position;
        _arrow.SetActive(true);
    }
}
