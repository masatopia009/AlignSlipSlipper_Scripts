// �Z�[�u�p�̃N���X�Ɏ���
public interface ISavable
{
    // ���������N���X�̃t�B�[���h���Z�[�u����
    void Save();

    // �߂�l�̓Z�[�u�f�[�^�����݂����true��Ԃ��悤�ɂ���
    bool Load();

    // �Z�[�u�f�[�^�̃f�t�H���g�l���Z�b�g����
    void SetDefault();
}
