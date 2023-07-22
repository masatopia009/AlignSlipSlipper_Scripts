using UnityEngine;

public class StageSaveData : ISavable
{
    [SerializeField]
    int stageNum;
    [SerializeField]
    int star;
    [SerializeField]
    int meterValue;
    [SerializeField]
    int centiValue;
    [SerializeField]
    int rotate;

    public int StageNum
    {
        get { return stageNum; }
        set { stageNum = value; }
    }

    public int Star
    {
        get { return star; }
        set { star = value; }
    }

    public Distance Distance
    {
        get { return new Distance(meterValue, centiValue); }
        set { meterValue = value.MeterValue; centiValue = value.CentiValue; }
    }

    public int Rotate
    {
        get { return rotate; }
        set { rotate = value; }
    }

    public StageSaveData(int stageNum)
    {
        StageNum = stageNum;
        SetDefault();
    }

    /// <summary>
    /// �f�t�H���g�l��ݒ�i�Z�[�u�f�[�^�������ꍇ�Ȃǂɗ��p�j
    /// </summary>
    public void SetDefault()
    {
        Star = 0;
        meterValue = 9;
        centiValue = 99;
        rotate = 360;
    }

    /// <summary>
    /// �t�B�[���h���Z�[�u
    /// </summary>
    public void Save()
    {
        string key = "Stage" + stageNum;
        string json = JsonUtility.ToJson(this);

        PlayerPrefs.SetString(key, json);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// �t�B�[���h�����[�h
    /// </summary>
    public bool Load()
    {
        StageSaveData data;

        string key = "Stage" + stageNum;
        string json = PlayerPrefs.GetString(key, "");

        // json���Ԃ��Ă��Ă���Ε���
        if (json != "")
        {
            data = JsonUtility.FromJson<StageSaveData>(json);

            StageNum = data.StageNum;
            Star = data.Star;
            meterValue = data.meterValue;
            centiValue = data.centiValue;
            Rotate = data.Rotate;

            return true;
        }
        else return false;
    }
}
