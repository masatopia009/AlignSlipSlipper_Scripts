using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UniRx;
using System;

[RequireComponent(typeof(Button))]
public class SceneLoader : MonoBehaviour
{
    [Tooltip("�J�ڂ������V�[����")][SerializeField]
    private string _targetScene;
    [Tooltip("�t�F�[�h�p�L�����o�X")][SerializeField]
    private Fade _fadeCanvas;
    [Tooltip("�t�F�[�h�C�����ԁi+0.05�b�j")][SerializeField]
    private float _fadeInTime = 0.25f;

    void Start()
    {
        // �{�^���������ꂽ�Ƃ��ɌĂԊ֐����w��
        Button button = GetComponent<Button>();
        button.onClick.AddListener(LoadScene);
    }

    // �w�肳�ꂽ�V�[�������[�h
    public void LoadScene()
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
}
