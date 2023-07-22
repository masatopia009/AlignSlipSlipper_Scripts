using System;
using UnityEngine;
using UnityEngine.UI;

/*
 * �K�v�ȃq�G�����L�[�̍\��
 * 
 * RootCanvas
 * |-Canvas(CanvasGroup)
 *   |-UI(ButtonInteractor)
 */

// �{�^���������ꂽ�瑼�̃{�^���𖳌��ɂ������ꍇ�ɃA�^�b�`(�g���K�[�ƂȂ�Button�ɃA�^�b�`)
public class ButtonInteractor : MonoBehaviour
{
    Transform rootCanvas = null;
    Transform firstCanvas = null;
    CanvasGroup canvasGroup = null;

    bool isInteractiveGroup;

    void Start()
    {
        int loop = 0; // �������[�v���p
        Transform itr = transform; // �C�e���[�^
        while (loop < 100) // �e�I�u�W�F�N�g��k���ĕK�v�ȃC���X�^���X���擾
        {
            Transform parent = itr.transform.parent;

            // ����ȏ�e�����݂��Ȃ��i�k��Ȃ��j�ꍇ�͏I��
            if (parent == null) break;

            Canvas canvas = parent.GetComponent<Canvas>();
            // ��ԍŏ��ɔ������ꂽCanvas������Canvas
            if (canvas && firstCanvas == null)
            {
                firstCanvas = parent.transform;
            }
            // ��ԍŌ�ɔ������ꂽCanvas�����[�gCanvas
            else if (canvas)
            {
                rootCanvas = parent.transform;
            }

            CanvasGroup group = parent.GetComponent<CanvasGroup>();
            // ��ԍŏ��ɔ������ꂽCanvasGroup���g�p
            if (group && canvasGroup == null)
            {
                canvasGroup = group;
            }

            itr = parent;
            loop++;
        }

        // �������m�F
        if (
            rootCanvas == null 
            || firstCanvas == null
            || canvasGroup == null
            || rootCanvas == firstCanvas
        )
        {
            throw new Exception($"�ϐ�������������������Ă��܂���F" +
                $"rootCanvas = {rootCanvas}, firstCanvas = {firstCanvas}, layoutGroup = {canvasGroup}");
        }

        // ���̑��̏�����
        isInteractiveGroup = canvasGroup.interactable;

        Button button = GetComponent<Button>();
        button.onClick.AddListener(SwitchInteractable);
    }

    // �{�^����������邽�тɃC���^���N�e�B�u��Ԃ�؂�ւ���
    public void SwitchInteractable()
    {
        // �L�����o�X�O���[�v�̃C���^���N�e�B�u��Ԃ𔽓]�����Ĕ��f
        isInteractiveGroup = !isInteractiveGroup;
        canvasGroup.interactable = isInteractiveGroup;

        // ���g�̐e��ύX���ăL�����o�X�O���[�v�̊O�ɔ���
        // �O���[�v���̗v�f�i����UI�j�����𖳌���Ԃɂ��Ă���
        if (isInteractiveGroup) // �{�^���������O�̏�Ԃɖ߂�
        {
            transform.SetParent(firstCanvas);
        }
        else // �{�^������������̏�Ԃɂ���
        {
            transform.SetParent(rootCanvas);
        }
    }
}
