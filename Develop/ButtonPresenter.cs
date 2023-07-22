using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class ButtonPresenter : MonoBehaviour
{
    public void OnCraateButton(ButtonModel model, StageView view)
    {
        Debug.Log(model.StageButton);

        model.StageButton.OnClickAsObservable()
            .Subscribe(_ =>
            {
                // model.Info‚ğ—p‚¢‚ÄView‚ğXV‚·‚é
                Debug.Log("aa");
                view.SetScoreText(model.Info.BestRotate.ToString());
            })
            .AddTo(this);
    }
}
