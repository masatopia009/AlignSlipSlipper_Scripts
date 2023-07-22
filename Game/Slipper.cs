using UnityEngine;

public class Slipper : MonoBehaviour
{
    [SerializeField] private float shotAngularDrag;

    [SerializeField] private GameObject pointer;
    [SerializeField] private GameObject arrow;
    [SerializeField] private GameObject goal;

    [SerializeField] private DistanceSlider slider;

    private Rigidbody2D myRigidbody;
    private Distance startGoalDistance;

    private Vector3 dragStartPos;
    private bool isShot = false; // 発射フラグ
    private bool isSleep = true; // 静止フラグ
    
    public bool IsSleep
    {
        get { return isSleep; }
    }

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();

        // 回転摩擦を設定 //
        myRigidbody.angularDrag = shotAngularDrag;

        startGoalDistance = new Distance(transform.position, goal.transform.position);
    }

    public int CalcRotate(Quaternion myRotate, Quaternion targetRotate)
    {
        int relativeRotate360 = (int)Mathf.Abs(myRotate.eulerAngles.z - targetRotate.eulerAngles.z);
        int rotate;
        if (relativeRotate360 > 180) rotate = 360 - relativeRotate360; // 0 <= startRotate360 < 360
        else rotate = relativeRotate360;
        return rotate;
    }

    private void FixedUpdate()
    {
        // 静止判定 //
        // 速度の絶対値の算出
        float velocityX = Mathf.Abs(myRigidbody.velocity.x);
        float velocityY = Mathf.Abs(myRigidbody.velocity.y);

        // 速度が小さい (ショットの一番最初は速度が0と認識されるので除外) - 速度が一気に0まで落ちた場合の対策必要
        if (0 < velocityX && velocityX < 0.5f && 0 < velocityY && velocityY < 0.5f)
        {
            // スリーブされていない
            if (!myRigidbody.IsSleeping())
            {
                myRigidbody.Sleep();
                isSleep = true;
            }
            // スリーブされている
            else { }
        }
        // 速度が0
        else if (velocityX == 0 && velocityX == 0) { }
        // 速度が大きい
        else isSleep = false;

        // UI更新
        slider.UpdateDistance(CurrentGoalDistance, startGoalDistance);
    }

    bool isMouseDown = false;
    void OnMouseDown()
    {
        if (!isShot)
        {
            Debug.Log("押した");

            this.dragStartPos = transform.position;

            // ポインターと矢印を表示 //
            // 直ぐにドラッグの関数が呼び出されるのでここで座標を指定する必要はない
            pointer.SetActive(true);
            pointer.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            arrow.SetActive(true);
            arrow.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);

            isMouseDown = true;
        }
    }


    void OnMouseDrag()
    {
        if (isShot == false && isMouseDown == true)
        {
            // ポインターの位置更新 //
            MovePointer();

            // 矢印の角度算出と更新 //
            var rad = GetAngle(pointer.transform.position, dragStartPos);
            Quaternion qua = Quaternion.AngleAxis(rad * Mathf.Rad2Deg, Vector3.back);
            arrow.transform.rotation = qua;
        }
    }

    /// <summary>
    /// マウスに合わせてポインターを動かす, 動かせる位置には限度がある.
    /// </summary>
    private void MovePointer()
    {
        // マウスのワールド座標を取得 //
        Vector3 currentMouseScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f);
        Vector3 currentMousePosition = Camera.main.ScreenToWorldPoint(currentMouseScreenPoint);

        // 位置制限を適用 //
        pointer.transform.position = new Vector2(
            Mathf.Clamp(currentMousePosition.x, dragStartPos.x - 1.5f, dragStartPos.x + 1.5f),
            Mathf.Clamp(currentMousePosition.y, dragStartPos.y - 1.5f, dragStartPos.y + 1.5f)
            );

    }

    private void OnMouseUp()
    {
        if (isShot == false && isMouseDown == true)
        {
            Shot();
        }
    }

    private void Shot()
    {
        // 引っ張った距離を算出 //
        // ポインターとスリッパの距離を引っ張った距離として扱う
        float pullDistance = Vector2.Distance(transform.position, pointer.transform.position);

        // 角度と距離から目的地までのベクトルを求める //
        float rad = GetAngle(pointer.transform.position, dragStartPos);
        double addForceX = Mathf.Sin(rad) * pullDistance;
        double addForceY = Mathf.Cos(rad) * pullDistance;

        // ベクトルを適当に倍して力を加える //
        Vector2 force = new Vector2((float)addForceX * 400, (float)addForceY * 400);

        // 力を加える位置を定義 //
        // yを-0.3する事で力点が中心からズレるため、力のモーメントによりスリッパが回転する
        Vector3 forcePos = transform.localPosition;
        forcePos.y -= 0.3f;

        // 力点の位置を指定して力を加える //
        myRigidbody.AddForceAtPosition(force, forcePos);
        
        pointer.SetActive(false);
        arrow.SetActive(false);

        //shotフラグON
        PlayManager.Instance.StartSlipperShot();
        isShot = true;
        isSleep = false;
        isMouseDown = false;
    }

    /// <summary>
    /// 次のショットへの準備
    /// </summary>
    public void ReadyNextShot()
    {
        isShot = false;
    }

    public Distance GetGoalDistance()
    {
        Distance distance = new Distance(transform.position, goal.transform.position);
        return distance;
    }

    public Distance StartGoalDistance
    {
        get { return startGoalDistance; }
    }

    public Distance CurrentGoalDistance
    {
        get { return new Distance(transform.position, goal.transform.position); }
    }

    public int CurrentRotate
    {
        get { return CalcRotate(transform.rotation, goal.transform.rotation); }
    }

    float GetAngle(Vector2 start, Vector2 target)
    {
        Vector2 dt = target - start;
        float rad = Mathf.Atan2(dt.x, dt.y);

        return rad;
    }

}
