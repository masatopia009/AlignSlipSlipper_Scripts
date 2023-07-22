using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioVolume : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider bgmSlider;
    [SerializeField] Slider seSlider;

    SaveManager saveManager;
    AudioConfig config;

    void Start()
    {
        //�X���C�_�[�𓮂��������̏�����o�^
        bgmSlider.onValueChanged.AddListener(SetAudioMixerBGM);
        seSlider.onValueChanged.AddListener(SetAudioMixerSE);

        // �f�[�^�����[�h
        saveManager = new SaveManager();
        config = new AudioConfig();
        saveManager.Load(config);

        // �X���C�_�[��������
        bgmSlider.value = config.BgmSliderValue;
        seSlider.value = config.SeSliderValue;

        // �~�L�T�[��������
        SetAudioMixerBGM(bgmSlider.value);
        SetAudioMixerSE(seSlider.value);
    }

    //BGM
    public void SetAudioMixerBGM(float value)
    {
        //�X���C�_�[�̒l���Z�[�u
        config.BgmSliderValue = value;
        saveManager.Save(config);

        // 1�ɕ␳�i100�i�K�̎��j
        value /= 100;
        //-80~0�ɕϊ�
        var volume = Mathf.Clamp(Mathf.Log10(value) * 20f, -80f, 0f);
        //audioMixer�ɑ��
        audioMixer.SetFloat("BGM", volume);
    }

    //SE
    public void SetAudioMixerSE(float value)
    {
        //�X���C�_�[�̒l���Z�[�u
        config.SeSliderValue = value;
        saveManager.Save(config);

        // 1�ɕ␳�i100�i�K�̎��j
        value /= 100;
        //-80~0�ɕϊ�
        var volume = Mathf.Clamp(Mathf.Log10(value) * 20f, -80f, 0f);
        //audioMixer�ɑ��
        audioMixer.SetFloat("SE", volume);
    }
}
