using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class StageOpener
{
    private List<StageButton> _stageButtonList; // �X�e�[�W�{�^��

    public StageOpener(List<StageButton> stageButtonList)
    {
        // �X�e�[�W�ԍ����ɕ��ׂ�
        stageButtonList.OrderBy(sb => sb.BasicData.StageNum);

        _stageButtonList = stageButtonList;
    }

    // �X�e�[�W�������
    public void OpenStage()
    {
        // �X�^�[�������ƂɃX�e�[�W�����
        bool isOpenNewStage = false;
        foreach (StageButton stageButton in _stageButtonList)
        {
            int star = stageButton.SaveData.Star;
            // �N���A�ς�
            if (star >= 1)
            {
                stageButton.Interact(true);
                continue;
            }
            // ���N���A
            else
            {
                // �V�K�X�e�[�W�̉��
                if (isOpenNewStage == false)
                {
                    stageButton.Interact(true);
                    isOpenNewStage = true;
                    continue;
                }
                // �V�K�X�e�[�W�ȍ~�̓��b�N
                else
                {
                    stageButton.Interact(false);
                    continue;
                }
            }
        }
    }
}
