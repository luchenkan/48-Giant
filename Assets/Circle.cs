using UnityEngine;
using System.Collections;

public class Circle : MonoBehaviour
{
    // 线速度
    private Vector2 linerSpeed;
    // 圆心
    private Vector2 circleDot;
    // 半径
    private float radius;
    // 刚体
    private Rigidbody2D body;
    // 速度
    private float speed = 5.0f;
    private float omga;
    // 线速度系数
    public float speedRate = 2.0f;

    void Start()
    {
        // 圆心和线速度
        circleDot = new Vector2(0, this.transform.position.y);
        Debug.Log("初始位置" + circleDot);
        linerSpeed = V3RotateAround(new Vector2(circleDot.x - this.transform.position.x, circleDot.y - this.transform.position.y), new Vector3(0, 0, -1), 90);
        // 刚体和线速度
        body = gameObject.GetComponent<Rigidbody2D>();
        body.velocity = linerSpeed;
        // 半径
        radius = new Vector2(circleDot.x - this.transform.position.x, circleDot.y - this.transform.position.y).magnitude;
        Debug.Log("半径是" + radius);
        // 线速度模
        speed = linerSpeed.magnitude;
        omga = speed * speed / radius;          // F = mv^2/R => omga = F*m
    }

    void FixedUpdate()
    {
        Vector2 fp = new Vector2(circleDot.x - this.transform.position.x, circleDot.y - this.transform.position.y);     //向心力矢量，但此时向量模不正确（不然会有吸引力问题）
        fp = fp.normalized * body.mass * omga;                      //纠正向量的模
        body.AddForce(fp, ForceMode2D.Force);

        if (Input.GetMouseButtonDown(0))
        {
            // 转向
            Debug.Log("中心点位置" + circleDot);
            Debug.Log("球位置" + this.transform.position);
            circleDot.x = this.transform.position.x + this.transform.position.x - circleDot.x;
            circleDot.y = this.transform.position.y + this.transform.position.y - circleDot.y;
            Debug.Log("更改后位置" + circleDot);
        }
    }

    // 旋转相量
    public Vector3 V3RotateAround(Vector2 source, Vector3 axis, float angle)
    {
        Quaternion q = Quaternion.AngleAxis(angle, axis);   // 旋转系数
        return q * source;                                  // 返回目标点
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawLine(circleDot, this.transform.position);
    }
}
