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
    private bool isShot = false; // ���˃t���O
    private bool isSleep = true; // �Î~�t���O
    
    public bool IsSleep
    {
        get { return isSleep; }
    }

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();

        // ��]���C��ݒ� //
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
        // �Î~���� //
        // ���x�̐�Βl�̎Z�o
        float velocityX = Mathf.Abs(myRigidbody.velocity.x);
        float velocityY = Mathf.Abs(myRigidbody.velocity.y);

        // ���x�������� (�V���b�g�̈�ԍŏ��͑��x��0�ƔF�������̂ŏ��O) - ���x����C��0�܂ŗ������ꍇ�̑΍��K�v
        if (0 < velocityX && velocityX < 0.5f && 0 < velocityY && velocityY < 0.5f)
        {
            // �X���[�u����Ă��Ȃ�
            if (!myRigidbody.IsSleeping())
            {
                myRigidbody.Sleep();
                isSleep = true;
            }
            // �X���[�u����Ă���
            else { }
        }
        // ���x��0
        else if (velocityX == 0 && velocityX == 0) { }
        // ���x���傫��
        else isSleep = false;

        // UI�X�V
        slider.UpdateDistance(CurrentGoalDistance, startGoalDistance);
    }

    bool isMouseDown = false;
    void OnMouseDown()
    {
        if (!isShot)
        {
            Debug.Log("������");

            this.dragStartPos = transform.position;

            // �|�C���^�[�Ɩ���\�� //
            // �����Ƀh���b�O�̊֐����Ăяo�����̂ł����ō��W���w�肷��K�v�͂Ȃ�
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
            // �|�C���^�[�̈ʒu�X�V //
            MovePointer();

            // ���̊p�x�Z�o�ƍX�V //
            var rad = GetAngle(pointer.transform.position, dragStartPos);
            Quaternion qua = Quaternion.AngleAxis(rad * Mathf.Rad2Deg, Vector3.back);
            arrow.transform.rotation = qua;
        }
    }

    /// <summary>
    /// �}�E�X�ɍ��킹�ă|�C���^�[�𓮂���, ��������ʒu�ɂ͌��x������.
    /// </summary>
    private void MovePointer()
    {
        // �}�E�X�̃��[���h���W���擾 //
        Vector3 currentMouseScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f);
        Vector3 currentMousePosition = Camera.main.ScreenToWorldPoint(currentMouseScreenPoint);

        // �ʒu������K�p //
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
        // �����������������Z�o //
        // �|�C���^�[�ƃX���b�p�̋��������������������Ƃ��Ĉ���
        float pullDistance = Vector2.Distance(transform.position, pointer.transform.position);

        // �p�x�Ƌ�������ړI�n�܂ł̃x�N�g�������߂� //
        float rad = GetAngle(pointer.transform.position, dragStartPos);
        double addForceX = Mathf.Sin(rad) * pullDistance;
        double addForceY = Mathf.Cos(rad) * pullDistance;

        // �x�N�g����K���ɔ{���ė͂������� //
        Vector2 force = new Vector2((float)addForceX * 400, (float)addForceY * 400);

        // �͂�������ʒu���` //
        // y��-0.3���鎖�ŗ͓_�����S����Y���邽�߁A�͂̃��[�����g�ɂ��X���b�p����]����
        Vector3 forcePos = transform.localPosition;
        forcePos.y -= 0.3f;

        // �͓_�̈ʒu���w�肵�ė͂������� //
        myRigidbody.AddForceAtPosition(force, forcePos);
        
        pointer.SetActive(false);
        arrow.SetActive(false);

        //shot�t���OON
        PlayManager.Instance.StartSlipperShot();
        isShot = true;
        isSleep = false;
        isMouseDown = false;
    }

    /// <summary>
    /// ���̃V���b�g�ւ̏���
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
