using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class PlayManager : SingletonMonoBehaviourInSceneBase<PlayManager>
{
    [SerializeField]
    int stageNum;
    [SerializeField]
    StageDataBase stageDB;
    [SerializeField]
    TextMeshProUGUI stageNumText;
    [SerializeField]
    List<Slipper> slippers = new List<Slipper>();

    [Header("ゲーム画面に移行したら呼ばれるメソッド")]
    [SerializeField] UnityEvent gameStartEvent = new UnityEvent();
    [Header("スリッパがショットされたら呼ばれるメソッド")]
    [SerializeField] UnityEvent startShotEvent = new UnityEvent();
    [Header("スリッパの静止が確認されたら呼ばれるメソッド")]
    [SerializeField] UnityEvent sleepSlipperEvent = new UnityEvent();
    [Header("リザルト準備処理の後に呼ばれるメソッド")]
    [SerializeField] UnityEvent gameOverEvent = new UnityEvent();

    PlayPhase playPhase; // ゲームの状態
    int maxStep = 5; // 最大手数 デフォルト値:5
    int currentStep = 0; // 現在の手数
    int star; // スター
    int scoreRotate; // 向き
    Distance scoreDis; // 距離 

    public int MaxStep
    {
        get { return maxStep; }
    }

    public Distance ScoreDis
    {
        get { return scoreDis; }
        private set { scoreDis = value; }
    }
    
    public int ScoreRotate
    {
        get { return scoreRotate; }
        private set { scoreRotate = value; }
    }

    public int Star
    {
        get { return star; }
        private set { star = value; }
    }

    private void Start()
    {
        playPhase = PlayPhase.Start;

        stageNumText.text = string.Format("ステージ{0:D2}", stageNum);
        maxStep = stageDB.GetStageData(stageNum).MaxStep;
    }

    public void FixedUpdate()
    {
        switch (playPhase) 
        {
            case PlayPhase.Start:
                // ゲーム開始イベント発行
                gameStartEvent.Invoke();
                playPhase = PlayPhase.Ready;
                break;

            case PlayPhase.Ready:
                // 終了キーを押されたとき
                if (Input.GetKey(KeyCode.F))
                    playPhase = PlayPhase.Over;
                break;

            case PlayPhase.Shot:
                bool isSleep = JudgeSleep();
                // 静止が確認できた場合
                if (isSleep)
                {
                    Debug.Log("静止した");
                    // 静止イベント発行
                    sleepSlipperEvent.Invoke();

                    bool isOver = currentStep >= maxStep;
                    // ゲームオーバーだった場合
                    if (isOver)
                    {
                        playPhase = PlayPhase.Over;
                    }
                    else
                    {
                        playPhase = PlayPhase.Ready;
                        // 次のショットの準備
                        foreach (Slipper slipper in slippers)
                            slipper.ReadyNextShot();
                    }
                }
                break;

            case PlayPhase.Over:
                GameOver();
                playPhase = PlayPhase.Finish;
                break;

            case PlayPhase.Finish:
                break;
        }

    }

    private bool JudgeSleep()
    {
        // 静止判定 //
        // 静止していないスリッパがある１つでも場合は静止していないとする
        bool isSleep = true;
        foreach (Slipper slipper in slippers)
        {
            if (!slipper.IsSleep)
            {
                isSleep = false;
                break;
            }
            else continue;
        }

        return isSleep;
    }

    private void GameOver()
    {
        // セーブデータのロード
        SaveManager saveManager = new SaveManager();
        StageSaveData saveData = new StageSaveData(stageNum);
        saveManager.Load(saveData);
        Distance highScore = saveData.Distance;
        int highStar = saveData.Star;
        int highRotate = saveData.Rotate;

        // 合計距離算出（初期値と最終値）
        Distance disSum = new Distance(0, 0);
        foreach (Slipper slipper in slippers)
        {
            disSum = disSum.AddDistance(slipper.CurrentGoalDistance);
        }
        ScoreDis = disSum;

        // 合計角度算出
        int rotateSum = 0;
        foreach (Slipper slipper in slippers)
        {
            rotateSum += slipper.CurrentRotate;
        }
        ScoreRotate = rotateSum;

        // スター数算出
        int score = ScoreDis.SumCentiUnit;
        int rotate = ScoreRotate;
        int star;
        if (score >= 160) star = 0;
        else if (score >= 80) star = 1;
        else if (score >= 40) star = 2;
        else
        {
            if (rotate >= 210) star = 3;
            else if (rotate >= 150) star = 4;
            else if (rotate >= 90) star = 5;
            else star = 6;
        }
        Star = star;

        // ハイスコアか確認
        if (ScoreDis.SumCentiUnit < highScore.SumCentiUnit)
            highScore = ScoreDis;

        // ハイスターか確認
        if (Star > highStar)
            highStar = Star;

        // ハイロテート確認（★3以上で記録）
        if (ScoreRotate < highRotate && Star >= 3)
            highRotate = ScoreRotate;

        // セーブ
        saveData.Distance = highScore;
        saveData.Star = highStar;
        saveData.Rotate = highRotate;
        saveManager.Save(saveData);

        // ゲームオーバーイベント発行
        gameOverEvent.Invoke();
    }

    /// <summary>
    /// スリッパのショットが為されたときの処理
    /// </summary>
    public void StartSlipperShot()
    {
        playPhase = PlayPhase.Shot;
        currentStep++;
        startShotEvent.Invoke();
    }
}

public enum PlayPhase
{
    Start,
    Ready,
    Shot,
    Over,
    Finish
}
