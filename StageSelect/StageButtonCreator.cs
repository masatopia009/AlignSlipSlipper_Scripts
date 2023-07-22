using System.Collections.Generic;
using UnityEngine;

// ステージボタンを作成し、セーブデータ+基本データを結びつけてプレゼンタに登録する
public class StageButtonCreator : MonoBehaviour
{
    [Header("ステージボタン")]
    [Tooltip("ステージボタンのプレハブ")][SerializeField]
    private StageButton _stageButton;
    [Tooltip("ステージボタンの親")][SerializeField]
    private Transform _stageButtonParent;

    [Header("その他必要なもの")]
    [Tooltip("基本データのDB")][SerializeField]
    private StageDataBase _dataBase;
    [Tooltip("プレゼンター")][SerializeField]
    private StageButtonPresenter _presenter;

    private List<StageButton> _stageButtonList = new List<StageButton>(); 

    void Start()
    {
        CreateStageButton();
    }

    private void CreateStageButton()
    {
        int stageCount = _dataBase.Count;

        // データベース上にある全てのステージをボタン化
        for (int index = 0; index < stageCount; index++)
        {
            StageBasicData basicData = _dataBase.GetStageDataByIndex(index);
            StageSaveData saveData = LoadSaveData(basicData.StageNum);

            // ボタンのゲームオブジェクトを生成/初期化
            GameObject stageButtonObject = Instantiate(_stageButton.gameObject, _stageButtonParent);
            StageButton stageButton = stageButtonObject.GetComponent<StageButton>();
            stageButton.Init(basicData, saveData);

            // プレゼンターに登録して監視してもらう
            _presenter.SubscribeStageButton(stageButton);

            _stageButtonList.Add(stageButton);
        }

        // ステージの解放処理
        StageOpener opener = new StageOpener(_stageButtonList);
        opener.OpenStage();
    }

    private StageSaveData LoadSaveData(int stageNum)
    {
        SaveManager saveManager = new SaveManager();
        StageSaveData saveData = new StageSaveData(stageNum);
        saveManager.Load(saveData);

        return saveData;
    }
}
