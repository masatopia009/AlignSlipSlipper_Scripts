public class SaveManager
{
    // 渡されたインスタンスのフィールドをセーブ
    public void Save(ISavable s)
    {
        s.Save();
    }

    // ロード時にデータがなければデフォルト値にしてセーブ
    public void Load(ISavable s)
    {
        bool isDataExist = s.Load();
        if (!isDataExist)
        {
            s.SetDefault();
            s.Save();
        }
    }
}
