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
                // 各情報をViewに伝える
                _infoView.SetThumbnail(stageButton.BasicData.Thumbnail);
                _infoView.SetBestDistance(stageButton.SaveData.Distance);
                _infoView.SetBestRotate(stageButton.SaveData.Rotate);
                _starView.SetStar(stageButton.SaveData.Star);

                // スター数をもとにRotatePopのON/OFFを切り替える
                _infoView.ActivateRotatePopByStar(stageButton.SaveData.Star);

                // ステージ番号を表示する
                _stageStartView.SetStageNum(stageButton.BasicData.StageNum);
            })
            .AddTo(this);
    }
}
