using UnityEngine;
using UnityEngine.UI;

// �A�N�e�B�u��؂�ւ���{�^���ɃA�^�b�`
public class UISwitcher : MonoBehaviour
{
    [Tooltip("�A�N�e�B�u��؂�ւ�����UI")][SerializeField]
    GameObject targetUI;

    bool isActive; // ����UI�̓A�N�e�B�u��

    private void Start()
    {
        // �{�^���������ꂽ��Ă΂��֐���ݒ�
        Button button = GetComponent<Button>();
        button.onClick.AddListener(SwitchActive);

        // ���݂̃A�N�e�B�u�󋵂��擾
        isActive = targetUI.activeSelf;
    }

    // �{�^����������邽�тɃA�N�e�B�u��Ԃ�؂�ւ���
    public void SwitchActive()
    {
        isActive = !isActive;
        targetUI.SetActive(isActive);
    }
}
