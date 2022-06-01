using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineTrigger : MonoBehaviour
{
    [SerializeField] bool isVertical;
    [SerializeField] bool isHorizontal;
    [SerializeField] Vector3 cutPoint;

    public Vector3 GetPosX()
    {
        return cutPoint;
    }
}
