using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class ButtonDispatcher : MonoBehaviour
{
    [SerializeField]
    private ButtonManager _buttonManager;
    [SerializeField]
    private ButtonPresenter _buttonPresenter;
    [SerializeField]
    private StageView stageView;

    // Start is called before the first frame update
    void Start()
    {
        // 今リストにあるやつをDispatch
        foreach (var m in _buttonManager.Models)
        {
            Dispatch(m);
        }

        // 以降新規作成されたものをDispatch
        _buttonManager.Models.ObserveAdd()
            .Subscribe(model => Dispatch(model.Value))
            .AddTo(this);
    }

    private void Dispatch(ButtonModel model)
    {
        _buttonPresenter.OnCraateButton(model, stageView);
    }
}
