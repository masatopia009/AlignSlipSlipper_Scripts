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

    // ステージ番号を指定してデータを検索
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

    // データベース内で何番目のデータかを指定してデータを検索
    public StageBasicData GetStageDataByIndex(int index)
    {
        if (index >= 0 && index < stageDataList.Count)
            return stageDataList[index];
        else 
            return null;
    }

    
}
