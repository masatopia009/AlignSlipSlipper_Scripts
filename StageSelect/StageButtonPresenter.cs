using UnityEngine;
using UniRx;

public class StageButtonPresenter : MonoBehaviour
{
    [SerializeField]
    private StageInfoView _infoView;
    [SerializeField]
    private StarView _starView;
    [SerializeField]
    private StageStartView _stageStartView;

    public void SubscribeStageButton(StageButton stageButton)
    {
        stageButton.Button.OnClickAsObservable()
            .Subscribe(_ =>
            {
                // �e����View�ɓ`����
                _infoView.SetThumbnail(stageButton.BasicData.Thumbnail);
                _infoView.SetBestDistance(stageButton.SaveData.Distance);
                _infoView.SetBestRotate(stageButton.SaveData.Rotate);
                _starView.SetStar(stageButton.SaveData.Star);

                // �X�^�[�������Ƃ�RotatePop��ON/OFF��؂�ւ���
                _infoView.ActivateRotatePopByStar(stageButton.SaveData.Star);

                // �X�e�[�W�ԍ���\������
                _stageStartView.SetStageNum(stageButton.BasicData.StageNum);
            })
            .AddTo(this);
    }
}
