using System;
using UnityEngine;
using UnityEngine.UI;

/*
 * 必要なヒエラルキーの構造
 * 
 * RootCanvas
 * |-Canvas(CanvasGroup)
 *   |-UI(ButtonInteractor)
 */

// ボタンを押されたら他のボタンを無効にしたい場合にアタッチ(トリガーとなるButtonにアタッチ)
public class ButtonInteractor : MonoBehaviour
{
    Transform rootCanvas = null;
    Transform firstCanvas = null;
    CanvasGroup canvasGroup = null;

    bool isInteractiveGroup;

    void Start()
    {
        int loop = 0; // 無限ループ回避用
        Transform itr = transform; // イテレータ
        while (loop < 100) // 親オブジェクトを遡って必要なインスタンスを取得
        {
            Transform parent = itr.transform.parent;

            // これ以上親が存在しない（遡れない）場合は終了
            if (parent == null) break;

            Canvas canvas = parent.GetComponent<Canvas>();
            // 一番最初に発見されたCanvasが初期Canvas
            if (canvas && firstCanvas == null)
            {
                firstCanvas = parent.transform;
            }
            // 一番最後に発見されたCanvasがルートCanvas
            else if (canvas)
            {
                rootCanvas = parent.transform;
            }

            CanvasGroup group = parent.GetComponent<CanvasGroup>();
            // 一番最初に発見されたCanvasGroupを使用
            if (group && canvasGroup == null)
            {
                canvasGroup = group;
            }

            itr = parent;
            loop++;
        }

        // 初期化確認
        if (
            rootCanvas == null 
            || firstCanvas == null
            || canvasGroup == null
            || rootCanvas == firstCanvas
        )
        {
            throw new Exception($"変数が正しく初期化されていません：" +
                $"rootCanvas = {rootCanvas}, firstCanvas = {firstCanvas}, layoutGroup = {canvasGroup}");
        }

        // その他の初期化
        isInteractiveGroup = canvasGroup.interactable;

        Button button = GetComponent<Button>();
        button.onClick.AddListener(SwitchInteractable);
    }

    // ボタンが押されるたびにインタラクティブ状態を切り替える
    public void SwitchInteractable()
    {
        // キャンバスグループのインタラクティブ状態を反転させて反映
        isInteractiveGroup = !isInteractiveGroup;
        canvasGroup.interactable = isInteractiveGroup;

        // 自身の親を変更してキャンバスグループの外に非難させ
        // グループ内の要素（他のUI）だけを無効状態にしている
        if (isInteractiveGroup) // ボタンを押す前の状態に戻す
        {
            transform.SetParent(firstCanvas);
        }
        else // ボタンを押した後の状態にする
        {
            transform.SetParent(rootCanvas);
        }
    }
}
