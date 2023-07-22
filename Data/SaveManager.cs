public class SaveManager
{
    // �n���ꂽ�C���X�^���X�̃t�B�[���h���Z�[�u
    public void Save(ISavable s)
    {
        s.Save();
    }

    // ���[�h���Ƀf�[�^���Ȃ���΃f�t�H���g�l�ɂ��ăZ�[�u
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
