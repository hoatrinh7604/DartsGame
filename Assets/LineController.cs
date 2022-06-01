using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineController : MonoBehaviour
{
    [SerializeField] float minVerticalPosX, maxVerticalPosX;
    [SerializeField] float minHorizontalPosY, maxHorizontalPosY;

    [SerializeField] RectTransform verticalLine, horizontalLine;

    [SerializeField] float speedVer;
    [SerializeField] int dirVer;
    private bool verticalMoving;

    [SerializeField] float speedHor;
    [SerializeField] int dirHor;
    private bool horizontalMoving;

    // Update is called once per frame
    void Update()
    {
        if(verticalMoving)
        {
            VerticalMoving();
        }

        if (horizontalMoving)
        {
            HorizontalMoving();
        }
    }

    public Vector3 GetCutPoint()
    {
        Vector3 cutPoint = new Vector3(verticalLine.transform.localPosition.x, horizontalLine.transform.localPosition.y, 0);
        return cutPoint;
    }

    public void HandleVertical(bool isMoving)
    {
        dirVer = Mathf .Abs(dirVer);
        int scale = Random.Range(40, 80);
        speedVer = 10*scale;
        verticalMoving = isMoving;
    }

    private void SwitchDir()
    {
        dirVer = dirVer * -1;
        speedVer = -1 * speedVer;

    }
    private void VerticalMoving()
    {
        verticalLine.transform.localPosition = new Vector3(verticalLine.transform.localPosition.x + speedVer * Time.deltaTime, verticalLine.transform.localPosition.y, verticalLine.transform.localPosition.z);

        if(dirVer > 0)
        {
            if(verticalLine.transform.localPosition.x >= maxVerticalPosX)
            {
                SwitchDir();
            }
        }else
        {
            if (verticalLine.transform.localPosition.x <= minVerticalPosX)
            {
                SwitchDir();
            }
        }
    }

    public void ResetVerticalLine()
    {
        verticalLine.transform.localPosition = new Vector3(minVerticalPosX, verticalLine.transform.localPosition.y, verticalLine.transform.localPosition.z);
    }

    public void HandleHorizontal(bool isMoving)
    {
        dirHor = Mathf.Abs(dirHor);
        int scale = Random.Range(40, 80);
        speedHor = 10*scale;
        horizontalMoving = isMoving;
    }

    private void SwitchHorDir()
    {
        dirHor = dirHor * -1;
        speedHor = -1 * speedHor;

    }
    private void HorizontalMoving()
    {
        horizontalLine.transform.localPosition = new Vector3(horizontalLine.transform.localPosition.x, horizontalLine.transform.localPosition.y + speedHor * Time.deltaTime, horizontalLine.transform.localPosition.z);

        if (dirHor > 0)
        {
            if (horizontalLine.transform.localPosition.y >= maxHorizontalPosY)
            {
                SwitchHorDir();
            }
        }
        else
        {
            if (horizontalLine.transform.localPosition.y <= minHorizontalPosY)
            {
                SwitchHorDir();
            }
        }
    }

    public void ResetHorizontalLine()
    {
        horizontalLine.transform.localPosition = new Vector3(horizontalLine.transform.localPosition.x, minHorizontalPosY, horizontalLine.transform.localPosition.z);
    }
}
