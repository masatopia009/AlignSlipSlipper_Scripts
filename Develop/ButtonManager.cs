using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class ButtonManager : MonoBehaviour
{
    [SerializeField]
    private ButtonModel _model;
    [SerializeField]
    private Canvas canvas;

    public IReactiveCollection<ButtonModel> Models => _models;

    private readonly ReactiveCollection<ButtonModel> _models = new ReactiveCollection<ButtonModel>();

    // Start is called before the first frame update
    void Start()
    {
        // セーブデータやデータベースなどから情報を集めてボタンを作る
        var newModel = Instantiate(_model, canvas.transform);
        newModel.Init();
        var buttonInfo = newModel.gameObject.GetComponent<ButtonInfo>();

        buttonInfo.BestDistance = new Distance(12, 34);
        buttonInfo.BestRotate = 10;

        _models.Add(newModel);
    }
}
