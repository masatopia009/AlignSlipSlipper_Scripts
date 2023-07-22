using UnityEngine;

public class SlipperClamp : MonoBehaviour
{
    [SerializeField] private float blank;

    private Vector2 leftBottom;
    private Vector2 rightTop;
    
    private void Start()
    {
        rightTop = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        leftBottom = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(
            Mathf.Clamp(transform.position.x, leftBottom.x + blank, rightTop.x - blank),
            Mathf.Clamp(transform.position.y, leftBottom.y + blank, rightTop.y - blank));
    }


}
