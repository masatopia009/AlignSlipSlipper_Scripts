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

    [Header("�Q�[����ʂɈڍs������Ă΂�郁�\�b�h")]
    [SerializeField] UnityEvent gameStartEvent = new UnityEvent();
    [Header("�X���b�p���V���b�g���ꂽ��Ă΂�郁�\�b�h")]
    [SerializeField] UnityEvent startShotEvent = new UnityEvent();
    [Header("�X���b�p�̐Î~���m�F���ꂽ��Ă΂�郁�\�b�h")]
    [SerializeField] UnityEvent sleepSlipperEvent = new UnityEvent();
    [Header("���U���g���������̌�ɌĂ΂�郁�\�b�h")]
    [SerializeField] UnityEvent gameOverEvent = new UnityEvent();

    PlayPhase playPhase; // �Q�[���̏��
    int maxStep = 5; // �ő�萔 �f�t�H���g�l:5
    int currentStep = 0; // ���݂̎萔
    int star; // �X�^�[
    int scoreRotate; // ����
    Distance scoreDis; // ���� 

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

        stageNumText.text = string.Format("�X�e�[�W{0:D2}", stageNum);
        maxStep = stageDB.GetStageData(stageNum).MaxStep;
    }

    public void FixedUpdate()
    {
        switch (playPhase) 
        {
            case PlayPhase.Start:
                // �Q�[���J�n�C�x���g���s
                gameStartEvent.Invoke();
                playPhase = PlayPhase.Ready;
                break;

            case PlayPhase.Ready:
                // �I���L�[�������ꂽ�Ƃ�
                if (Input.GetKey(KeyCode.F))
                    playPhase = PlayPhase.Over;
                break;

            case PlayPhase.Shot:
                bool isSleep = JudgeSleep();
                // �Î~���m�F�ł����ꍇ
                if (isSleep)
                {
                    Debug.Log("�Î~����");
                    // �Î~�C�x���g���s
                    sleepSlipperEvent.Invoke();

                    bool isOver = currentStep >= maxStep;
                    // �Q�[���I�[�o�[�������ꍇ
                    if (isOver)
                    {
                        playPhase = PlayPhase.Over;
                    }
                    else
                    {
                        playPhase = PlayPhase.Ready;
                        // ���̃V���b�g�̏���
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
        // �Î~���� //
        // �Î~���Ă��Ȃ��X���b�p������P�ł��ꍇ�͐Î~���Ă��Ȃ��Ƃ���
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
        // �Z�[�u�f�[�^�̃��[�h
        SaveManager saveManager = new SaveManager();
        StageSaveData saveData = new StageSaveData(stageNum);
        saveManager.Load(saveData);
        Distance highScore = saveData.Distance;
        int highStar = saveData.Star;
        int highRotate = saveData.Rotate;

        // ���v�����Z�o�i�����l�ƍŏI�l�j
        Distance disSum = new Distance(0, 0);
        foreach (Slipper slipper in slippers)
        {
            disSum = disSum.AddDistance(slipper.CurrentGoalDistance);
        }
        ScoreDis = disSum;

        // ���v�p�x�Z�o
        int rotateSum = 0;
        foreach (Slipper slipper in slippers)
        {
            rotateSum += slipper.CurrentRotate;
        }
        ScoreRotate = rotateSum;

        // �X�^�[���Z�o
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

        // �n�C�X�R�A���m�F
        if (ScoreDis.SumCentiUnit < highScore.SumCentiUnit)
            highScore = ScoreDis;

        // �n�C�X�^�[���m�F
        if (Star > highStar)
            highStar = Star;

        // �n�C���e�[�g�m�F�i��3�ȏ�ŋL�^�j
        if (ScoreRotate < highRotate && Star >= 3)
            highRotate = ScoreRotate;

        // �Z�[�u
        saveData.Distance = highScore;
        saveData.Star = highStar;
        saveData.Rotate = highRotate;
        saveManager.Save(saveData);

        // �Q�[���I�[�o�[�C�x���g���s
        gameOverEvent.Invoke();
    }

    /// <summary>
    /// �X���b�p�̃V���b�g���ׂ��ꂽ�Ƃ��̏���
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
