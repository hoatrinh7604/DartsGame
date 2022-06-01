using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointController : MonoBehaviour
{
    [SerializeField] Vector3[] piecesOfCircle;
    [SerializeField] int[] pointOfPieces;
    [SerializeField] float radius;

    [SerializeField] Transform tempTran;

    [SerializeField] float centerRadius;
    [SerializeField] int centerPoint;
    [SerializeField] float centerRadius2;
    [SerializeField] int centerPoint2;
    public int GetPointOfPosition(Vector3 point)
    {
        int checkPoint = CheckCenter(point);
        if(checkPoint > 0)
        {
            return checkPoint;
        }

        if(CheckOutSide(point))
        {
            return 0;
        }

        int index = 0;
        int currentIndex = 0;
        float check = 1;
        do
        {
            Vector3 vec1 = piecesOfCircle[index];
            if (index + 1 >= piecesOfCircle.Length) index = 0;
            Vector3 vec2 = piecesOfCircle[index+1];
            currentIndex = index;
            index++;
            if (Vector3.Angle(vec1, point) > 90) continue;

            float check1 = IsLeft(vec1, Vector3.zero, point);
            float check2 = IsLeft(vec2, Vector3.zero, point);
            if (check1 == 0)
            {
                return currentIndex;
            }

            check = check1 * check2;

        } while (check > 0);

        return pointOfPieces[currentIndex];
    }

    private int CheckCenter(Vector3 point)
    {
        if(point.magnitude < centerRadius)
        {
            return centerPoint;
        }

        if(point.magnitude < centerRadius2)
        {
            return centerPoint2;
        }

        return 0;
    }

    private bool CheckOutSide(Vector3 point)
    {
        if(point.magnitude > radius)
        {
            return true;
        }

        if(point.magnitude < 5)
        {

        }

        return false;
    }

    public float IsLeft(Vector3 a, Vector3 b, Vector3 c)
    {
        return ((b.x - a.x) * (c.y - a.y) - (b.y - a.y) * (c.x - a.x));
    }

}
