using UnityEngine;

public class AudioConfig : ISavable
{
    [SerializeField] 
    float bgmSliderValue;
    [SerializeField]
    float seSliderValue;

    public float BgmSliderValue
    {
        get { return bgmSliderValue; }
        set { bgmSliderValue = value; }
    }

    public float SeSliderValue
    {
        get { return seSliderValue; }
        set { seSliderValue = value; }
    }

    public AudioConfig()
    {
        SetDefault();
    }

    public bool Load()
    {
        AudioConfig data;

        string key = "AudioConfig";
        string json = PlayerPrefs.GetString(key, null);

        // json‚ª•Ô‚Á‚Ä‚«‚Ä‚¢‚ê‚Î•œŒ³
        if (json != "")
        {
            data = JsonUtility.FromJson<AudioConfig>(json);
            BgmSliderValue = data.BgmSliderValue;
            SeSliderValue = data.SeSliderValue;
            return true;
        }
        else return false;
    }

    public void Save()
    {
        string key = "AudioConfig";
        string json = JsonUtility.ToJson(this);

        PlayerPrefs.SetString(key, json);
        PlayerPrefs.Save();
    }

    public void SetDefault()
    {
        bgmSliderValue = 50f;
        seSliderValue = 50f;
    }
}
