using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonSe : MonoBehaviour
{
    [Tooltip("押したときの効果音")][SerializeField]
    private AudioClip _se;

    private void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(PlaySe);
    }

    public void PlaySe()
    {
        PlaySeDontDestroy.Instance.PlaySound(_se);
    }
}
