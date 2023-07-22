using UnityEngine;
using UnityEngine.UI;
using TMPro;

// �X�e�[�W�{�^����Model�Ƃ��Ĉ����A�{�^���ɕR�Â��f�[�^����������
[RequireComponent(typeof(Button))]
public class StageButton : MonoBehaviour
{
    private StageBasicData _basicData; // ��{�f�[�^
    private StageSaveData _saveData; // �Z�[�u�f�[�^
    private Button _button; // �{�^���̃R���|�[�l���g

    // �f�[�^�ǂݎ��p
    public StageBasicData BasicData => _basicData;
    public StageSaveData SaveData => _saveData;
    public Button Button => _button;

    public void Init(StageBasicData basicData, StageSaveData saveData)
    {
        _basicData = basicData;
        _saveData = saveData;

        _button = GetComponent<Button>();

        // �{�^���̔ԍ���ݒ�
        TextMeshProUGUI numText = GetComponentInChildren<TextMeshProUGUI>();
        numText.text = numText.text = string.Format("{0:D2}", basicData.StageNum);
    }

    // �{�^���̗L���E�����؂�ւ�
    public void Interact(bool isInteract)
    {
        _button.interactable = isInteract;
    }
}
