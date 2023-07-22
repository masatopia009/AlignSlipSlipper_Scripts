using UnityEngine;
using UnityEngine.UI;

// アクティブを切り替えるボタンにアタッチ
public class UISwitcher : MonoBehaviour
{
    [Tooltip("アクティブを切り替えたいUI")][SerializeField]
    GameObject targetUI;

    bool isActive; // 現在UIはアクティブか

    private void Start()
    {
        // ボタンを押されたら呼ばれる関数を設定
        Button button = GetComponent<Button>();
        button.onClick.AddListener(SwitchActive);

        // 現在のアクティブ状況を取得
        isActive = targetUI.activeSelf;
    }

    // ボタンが押されるたびにアクティブ状態を切り替える
    public void SwitchActive()
    {
        isActive = !isActive;
        targetUI.SetActive(isActive);
    }
}
