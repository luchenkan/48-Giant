using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBorder : MonoBehaviour {
    public float leftBorder;
    public float rightBorder;
    public float topBorder;
    public float downBorder;
    private float width;
    private float height;
    public static CheckBorder Instance;
    // Use this for initialization
    void Start () {
        Instance = this;
        //世界坐标的右上角  因为视口坐标右上角是1,1,点
        Vector3 cornerPos = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f,
         Mathf.Abs(-Camera.main.transform.position.z)));
        //世界坐标左边界
        leftBorder = Camera.main.transform.position.x - (cornerPos.x - Camera.main.transform.position.x);
        //世界坐标右边界
        rightBorder = cornerPos.x;
        //世界坐标上边界
        topBorder = cornerPos.y;
        //世界坐标下边界
        downBorder = Camera.main.transform.position.y - (cornerPos.y - Camera.main.transform.position.y);

        width = rightBorder - leftBorder;
        height = topBorder - downBorder;
    }

    public void CheckBorderByTransform(Transform checkTrans, float width, float height)
    {
        if (checkTrans.position.x > rightBorder - width)
        {
            checkTrans.position = new Vector3(rightBorder - width, checkTrans.position.y, checkTrans.position.z);
        }
        if (checkTrans.position.x < leftBorder + width)
        {
            checkTrans.position = new Vector3(leftBorder + width, checkTrans.position.y, checkTrans.position.z);
        }
        if (checkTrans.position.y > topBorder - height)
        {
            checkTrans.position = new Vector3(checkTrans.position.x, topBorder - height, checkTrans.position.z);
        }
        if (checkTrans.position.y < downBorder + height)
        {
            checkTrans.position = new Vector3(checkTrans.position.x, downBorder + height, checkTrans.position.z);
        }
    }

    public float GetObstacleOffset(bool isLeft, float width)
    {
        return isLeft ? (leftBorder - width) : (rightBorder + width);
    }

    public bool CheckBeyondBorder(Transform checkTrans, float width)
    {
        return (checkTrans.position.x > rightBorder + width) || (checkTrans.position.x < leftBorder - width);
    }

    public bool CheckOutOfBorder(Transform checkTrans, float width, float height)
    {
        return (checkTrans.position.x > rightBorder - width) || (checkTrans.position.x < leftBorder + width) ||
            (checkTrans.position.y > topBorder - height) || (checkTrans.position.y < downBorder + height);
    }

}
