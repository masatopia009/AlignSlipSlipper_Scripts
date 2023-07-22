using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyScriptable/StageDataBase")]
public class StageDataBase : ScriptableObject
{
    [SerializeField]
    List<StageBasicData> stageDataList = new List<StageBasicData>();

    public int Count
    {
        get { return stageDataList.Count; }
    }

    // �X�e�[�W�ԍ����w�肵�ăf�[�^������
    public StageBasicData GetStageData(int stageNum)
    {
        foreach (StageBasicData sd in stageDataList)
        {
            if (sd.StageNum == stageNum)
            {
                return sd;
            }
        }

        return null;
    }

    // �f�[�^�x�[�X���ŉ��Ԗڂ̃f�[�^�����w�肵�ăf�[�^������
    public StageBasicData GetStageDataByIndex(int index)
    {
        if (index >= 0 && index < stageDataList.Count)
            return stageDataList[index];
        else 
            return null;
    }

    
}
