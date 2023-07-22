using System;
using UnityEngine;

public class Distance
{
    private readonly int meterValue;
    private readonly int centiValue;

    // Unityの座標から算出した距離をそのまま利用すると
    // スケールが大きすぎるため１以下の正の数を倍率として掛ける
    private readonly float DISTANCE_SCALE = 0.3f;

    // 各値の限度
    private readonly int METER_MIN = 0;

    private readonly int CENTI_MIN = 0;
    private readonly int CENYI_MAX = 99;

    /// <summary>
    /// メートル・センチ値がわかっている場合のコンストラクタ 
    /// </summary>
    public Distance(int meterValue, int centiValue)
    {
        if (meterValue < METER_MIN)
        {
            throw new ArgumentException(
                $"メートルの値は {METER_MIN} 以上で指定してください。入力値 = {meterValue}");
        }
        if (centiValue < CENTI_MIN || centiValue > CENYI_MAX)
        {
            throw new ArgumentException(
                $"センチの値は {CENTI_MIN} 以上 {CENYI_MAX} 以下で指定してください。入力値 = {centiValue}");
        }

        this.meterValue = meterValue;
        this.centiValue = centiValue;
    }

    /// <summary>
    /// 起点・終点座標がわかっている場合のコンストラクタ
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
    /// 距離をセンチに直した場合の値
    /// </summary>
    public int SumCentiUnit
    {
        get { return meterValue * 100 + centiValue; }
    }

    /// <summary>
    /// Distance同士の加算
    /// </summary>
    public Distance AddDistance(Distance addend)
    {
        // 距離の合計を㎝で出す
        int sumCentiUnit = this.SumCentiUnit + addend.SumCentiUnit;

        // mとcmに分ける
        int meter = sumCentiUnit / 100;
        int centi = sumCentiUnit % 100;

        return new Distance(meter, centi);
    }
}
