using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInfo : MonoBehaviour
{
    public int BestRotate
    {
        get { return _bestRotate; }
        set { _bestRotate = value; }
    }

    public Distance BestDistance
    {
        get { return _bestDistance; }
        set { _bestDistance = value; }
    }

    private int _bestRotate;
    private Distance _bestDistance;
}
