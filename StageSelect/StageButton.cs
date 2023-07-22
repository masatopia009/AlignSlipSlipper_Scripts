using UnityEngine;
using UnityEngine.UI;
using TMPro;

// ステージボタンをModelとして扱い、ボタンに紐づくデータを持たせる
[RequireComponent(typeof(Button))]
public class StageButton : MonoBehaviour
{
    private StageBasicData _basicData; // 基本データ
    private StageSaveData _saveData; // セーブデータ
    private Button _button; // ボタンのコンポーネント

    // データ読み取り用
    public StageBasicData BasicData => _basicData;
    public StageSaveData SaveData => _saveData;
    public Button Button => _button;

    public void Init(StageBasicData basicData, StageSaveData saveData)
    {
        _basicData = basicData;
        _saveData = saveData;

        _button = GetComponent<Button>();

        // ボタンの番号を設定
        TextMeshProUGUI numText = GetComponentInChildren<TextMeshProUGUI>();
        numText.text = numText.text = string.Format("{0:D2}", basicData.StageNum);
    }

    // ボタンの有効・無効切り替え
    public void Interact(bool isInteract)
    {
        _button.interactable = isInteract;
    }
}
