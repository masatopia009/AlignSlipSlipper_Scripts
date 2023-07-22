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
        //スライダーを動かした時の処理を登録
        bgmSlider.onValueChanged.AddListener(SetAudioMixerBGM);
        seSlider.onValueChanged.AddListener(SetAudioMixerSE);

        // データをロード
        saveManager = new SaveManager();
        config = new AudioConfig();
        saveManager.Load(config);

        // スライダーを初期化
        bgmSlider.value = config.BgmSliderValue;
        seSlider.value = config.SeSliderValue;

        // ミキサーを初期化
        SetAudioMixerBGM(bgmSlider.value);
        SetAudioMixerSE(seSlider.value);
    }

    //BGM
    public void SetAudioMixerBGM(float value)
    {
        //スライダーの値をセーブ
        config.BgmSliderValue = value;
        saveManager.Save(config);

        // 1に補正（100段階の時）
        value /= 100;
        //-80~0に変換
        var volume = Mathf.Clamp(Mathf.Log10(value) * 20f, -80f, 0f);
        //audioMixerに代入
        audioMixer.SetFloat("BGM", volume);
    }

    //SE
    public void SetAudioMixerSE(float value)
    {
        //スライダーの値をセーブ
        config.SeSliderValue = value;
        saveManager.Save(config);

        // 1に補正（100段階の時）
        value /= 100;
        //-80~0に変換
        var volume = Mathf.Clamp(Mathf.Log10(value) * 20f, -80f, 0f);
        //audioMixerに代入
        audioMixer.SetFloat("SE", volume);
    }
}
