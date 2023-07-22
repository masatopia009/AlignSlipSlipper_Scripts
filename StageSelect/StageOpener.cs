using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class StageOpener
{
    private List<StageButton> _stageButtonList; // ステージボタン

    public StageOpener(List<StageButton> stageButtonList)
    {
        // ステージ番号順に並べる
        stageButtonList.OrderBy(sb => sb.BasicData.StageNum);

        _stageButtonList = stageButtonList;
    }

    // ステージ解放処理
    public void OpenStage()
    {
        // スター数をもとにステージを解放
        bool isOpenNewStage = false;
        foreach (StageButton stageButton in _stageButtonList)
        {
            int star = stageButton.SaveData.Star;
            // クリア済み
            if (star >= 1)
            {
                stageButton.Interact(true);
                continue;
            }
            // 未クリア
            else
            {
                // 新規ステージの解放
                if (isOpenNewStage == false)
                {
                    stageButton.Interact(true);
                    isOpenNewStage = true;
                    continue;
                }
                // 新規ステージ以降はロック
                else
                {
                    stageButton.Interact(false);
                    continue;
                }
            }
        }
    }
}
