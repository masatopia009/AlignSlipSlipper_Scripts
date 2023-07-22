using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

public class ButtonModel : MonoBehaviour
{
    public Button StageButton => _stageButton;

    public ButtonInfo Info => _buttonInfo;

    private Button _stageButton;
    private ButtonInfo _buttonInfo;

    // Start is called before the first frame update
    public void Init()
    {
        Debug.Log("Init");
        _stageButton = GetComponent<Button>();
        _buttonInfo = GetComponent<ButtonInfo>();
    }

    // データ保持してるだけやん
    // ボタンinfoをここに合併しても良いのでは
}
