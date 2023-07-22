using UnityEngine;

[System.Serializable]
public class StageBasicData
{
    [SerializeField]
    int stageNum;
    [SerializeField]
    int maxStep;
    [SerializeField]
    Sprite thumbnail;

    public int StageNum { get { return stageNum; } }
    public int MaxStep { get { return maxStep; } }
    public Sprite Thumbnail { get { return thumbnail; } }
}
