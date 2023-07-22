using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UniRx;
using System;

public class StageStartView : MonoBehaviour
{
    [Header("�X�e�[�W�̃��[�h")]
    [Tooltip("�X�^�[�g�{�^��")][SerializeField]
    private Button _startButton;
    [Tooltip("���[�h����X�e�[�W���̐ړ����iex. Stage0�j")][SerializeField]
    private string _STAGE_PREFIX = "Stage";
    [Tooltip("�t�F�[�h�p�L�����o�X")][SerializeField]
    private Fade _fadeCanvas;
    [Tooltip("�t�F�[�h�C�����ԁi+0.05�b�j")][SerializeField]
    private float _fadeInTime = 0.25f;

    [Header("�X�e�[�W�ԍ���POP")]
    [Tooltip("�X�e�[�W�ԍ�POP")][SerializeField]
    private GameObject _stageNumPop;
    [Tooltip("�X�e�[�W�ԍ��p�e�L�X�g")][SerializeField]
    private TextMeshProUGUI _stageNumText;
    [Tooltip("POP�̐ړ����iex. �X�e�[�W00�j")][SerializeField]
    private string _POP_PREFIX = "�X�e�[�W";

    private int _selectedStageNum;

    private void Start()
    {
        // �X�e�[�W���I�΂�Ă��Ȃ���Ԃł̓{�^���������Ȃ�/POP�͌����Ȃ�
        _startButton.interactable = false;
        _stageNumPop.SetActive(false);

        _startButton.onClick.AddListener(StageStart);
    }

    // �X�e�[�W�ԍ����Z�b�g���ĕ\��
    public void SetStageNum(int stageNum)
    {
        // �X�e�[�W�ԍ����I�΂ꂽ��{�^����������悤�ɂ���
        _startButton.interactable = true;

        // POP���\������
        _stageNumText.text = _POP_PREFIX + string.Format("{0:D2}", stageNum);
        _stageNumPop.SetActive(true);

        // �X�e�[�W���[�h�p�ɃX�e�[�W�ԍ����T����
        _selectedStageNum = stageNum;
    }

    // �X�^�[�g�{�^���������ꂽ��X�e�[�W�����[�h
    public void StageStart()
    {
        // ���[�h����X�e�[�W�̖��O��g�ݗ��Ă�
        string load = _STAGE_PREFIX + _selectedStageNum.ToString();

        // �t�F�[�h�C��
        if (_fadeCanvas != null)
        {
            _fadeCanvas.FadeIn(_fadeInTime);
        }

        // �t�F�[�h�C����҂��Ă���V�[���J�ځi���������Ԃ��������Ȃ��ƃt�F�[�h�C�����r�؂��j
        Observable.Timer(TimeSpan.FromSeconds(_fadeInTime + 0.05f))
            .Subscribe(_ => SceneManager.LoadScene(load));
    }
}
