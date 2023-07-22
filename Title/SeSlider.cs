using UnityEngine;
using UnityEngine.EventSystems;

public class SeSlider : MonoBehaviour, IPointerUpHandler
{
    [SerializeField]
    AudioClip configSE;

    public void OnPointerUp(PointerEventData eventData)
    {
        PlaySeDontDestroy.Instance.PlaySound(configSE);
    }
}
