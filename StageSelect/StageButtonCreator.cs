using System.Collections.Generic;
using UnityEngine;

// �X�e�[�W�{�^�����쐬���A�Z�[�u�f�[�^+��{�f�[�^�����т��ăv���[���^�ɓo�^����
public class StageButtonCreator : MonoBehaviour
{
    [Header("�X�e�[�W�{�^��")]
    [Tooltip("�X�e�[�W�{�^���̃v���n�u")][SerializeField]
    private StageButton _stageButton;
    [Tooltip("�X�e�[�W�{�^���̐e")][SerializeField]
    private Transform _stageButtonParent;

    [Header("���̑��K�v�Ȃ���")]
    [Tooltip("��{�f�[�^��DB")][SerializeField]
    private StageDataBase _dataBase;
    [Tooltip("�v���[���^�[")][SerializeField]
    private StageButtonPresenter _presenter;

    private List<StageButton> _stageButtonList = new List<StageButton>(); 

    void Start()
    {
        CreateStageButton();
    }

    private void CreateStageButton()
    {
        int stageCount = _dataBase.Count;

        // �f�[�^�x�[�X��ɂ���S�ẴX�e�[�W���{�^����
        for (int index = 0; index < stageCount; index++)
        {
            StageBasicData basicData = _dataBase.GetStageDataByIndex(index);
            StageSaveData saveData = LoadSaveData(basicData.StageNum);

            // �{�^���̃Q�[���I�u�W�F�N�g�𐶐�/������
            GameObject stageButtonObject = Instantiate(_stageButton.gameObject, _stageButtonParent);
            StageButton stageButton = stageButtonObject.GetComponent<StageButton>();
            stageButton.Init(basicData, saveData);

            // �v���[���^�[�ɓo�^���ĊĎ����Ă��炤
            _presenter.SubscribeStageButton(stageButton);

            _stageButtonList.Add(stageButton);
        }

        // �X�e�[�W�̉������
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
