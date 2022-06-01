using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillowController : MonoBehaviour
{
    public void UpdateTransform(Vector3 pos)
    {
        transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
    }
}
