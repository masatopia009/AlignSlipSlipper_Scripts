using System;
using UnityEngine;

public class Distance
{
    private readonly int meterValue;
    private readonly int centiValue;

    // Unity�̍��W����Z�o�������������̂܂ܗ��p�����
    // �X�P�[�����傫�����邽�߂P�ȉ��̐��̐���{���Ƃ��Ċ|����
    private readonly float DISTANCE_SCALE = 0.3f;

    // �e�l�̌��x
    private readonly int METER_MIN = 0;

    private readonly int CENTI_MIN = 0;
    private readonly int CENYI_MAX = 99;

    /// <summary>
    /// ���[�g���E�Z���`�l���킩���Ă���ꍇ�̃R���X�g���N�^ 
    /// </summary>
    public Distance(int meterValue, int centiValue)
    {
        if (meterValue < METER_MIN)
        {
            throw new ArgumentException(
                $"���[�g���̒l�� {METER_MIN} �ȏ�Ŏw�肵�Ă��������B���͒l = {meterValue}");
        }
        if (centiValue < CENTI_MIN || centiValue > CENYI_MAX)
        {
            throw new ArgumentException(
                $"�Z���`�̒l�� {CENTI_MIN} �ȏ� {CENYI_MAX} �ȉ��Ŏw�肵�Ă��������B���͒l = {centiValue}");
        }

        this.meterValue = meterValue;
        this.centiValue = centiValue;
    }

    /// <summary>
    /// �N�_�E�I�_���W���킩���Ă���ꍇ�̃R���X�g���N�^
    /// </summary>
    public Distance(Vector2 startPos, Vector2 endPos)
    {
        float directDis = Vector2.Distance(startPos, endPos);
        float centimeterDis = directDis * DISTANCE_SCALE * 100;

        this.meterValue = (int)(centimeterDis / 100f);
        this.centiValue = (int)(centimeterDis % 100f);
    }

    public int MeterValue
    {
        get { return meterValue; }
    }

    public int CentiValue
    {
        get { return centiValue; }
    }

    /// <summary>
    /// �������Z���`�ɒ������ꍇ�̒l
    /// </summary>
    public int SumCentiUnit
    {
        get { return meterValue * 100 + centiValue; }
    }

    /// <summary>
    /// Distance���m�̉��Z
    /// </summary>
    public Distance AddDistance(Distance addend)
    {
        // �����̍��v���p�ŏo��
        int sumCentiUnit = this.SumCentiUnit + addend.SumCentiUnit;

        // m��cm�ɕ�����
        int meter = sumCentiUnit / 100;
        int centi = sumCentiUnit % 100;

        return new Distance(meter, centi);
    }
}
