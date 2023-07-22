// セーブ用のクラスに実装
public interface ISavable
{
    // 実装したクラスのフィールドをセーブする
    void Save();

    // 戻り値はセーブデータが存在すればtrueを返すようにする
    bool Load();

    // セーブデータのデフォルト値をセットする
    void SetDefault();
}
