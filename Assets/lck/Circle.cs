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
    // 游戏开始向下的初速度
    public float oldSpeed = 2.0f;
    // 摄像机
    [SerializeField]
    private GameObject camera;
    // 
    public int state = 0;

    public int flag = 1;

    void Start()
    {
        body = gameObject.GetComponent<Rigidbody2D>();
        // 一开始给一个向下的速度
        body.velocity = new Vector2(0, -oldSpeed);
        state = 0;
    }

    void FixedUpdate()
    {
        // 松开走直线，按的时候绕圈
        /*if(Input.GetKeyDown(KeyCode.Space))
        {
            // 圆心和线速度
            circleDot = new Vector2(this.transform.position.x +2, this.transform.position.y - 2);
            linerSpeed = V3RotateAround(new Vector2(circleDot.x - this.transform.position.x, circleDot.y - this.transform.position.y), new Vector3(0, 0, -1), -90);
            // 刚体和线速度
            body = gameObject.GetComponent<Rigidbody2D>();
            body.velocity = linerSpeed;
            // 半径
            radius = new Vector2(circleDot.x - this.transform.position.x, circleDot.y - this.transform.position.y).magnitude;
            //Debug.Log("半径是" + radius);
            // 线速度模
            speed = linerSpeed.magnitude;
            omga = speed * speed / radius;
        }*/
        if (Input.GetKey(KeyCode.Space))        // 按着走直线
        {
            /*
            // 圆心和线速度
            circleDot = new Vector2(this.transform.position.x + 2, this.transform.position.y - 2);
            linerSpeed = V3RotateAround(new Vector2(circleDot.x - this.transform.position.x, circleDot.y - this.transform.position.y), new Vector3(0, 0, -1), -90);
            // 刚体和线速度
            body = gameObject.GetComponent<Rigidbody2D>();
            body.velocity = linerSpeed;
            // 半径
            radius = new Vector2(circleDot.x - this.transform.position.x, circleDot.y - this.transform.position.y).magnitude;
            // 线速度模
            speed = linerSpeed.magnitude;
            omga = speed * speed / radius;

            Vector2 fp = new Vector2(circleDot.x - this.transform.position.x, circleDot.y - this.transform.position.y);     //向心力矢量，但此时向量模不正确（不然会有吸引力问题）
            Debug.Log(new Vector2(circleDot.x - this.transform.position.x, circleDot.y - this.transform.position.y));
            fp = fp.normalized * body.mass * omga;                      //纠正向量的模
            body.AddForce(fp, ForceMode2D.Force);
            */
            flag = 1;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            // body.velocity = new Vector3(0, 0, 0);
            // 圆心和线速度
            circleDot = new Vector2(this.transform.position.x + 5, this.transform.position.y + 5);
            //Debug.Log("初始位置" + circleDot);
            linerSpeed = V3RotateAround(new Vector2(circleDot.x - this.transform.position.x, circleDot.y - this.transform.position.y), new Vector3(0, 0, -1), -90);
            // 线速度
            body.velocity = linerSpeed;
            // 半径
            radius = new Vector2(circleDot.x - this.transform.position.x, circleDot.y - this.transform.position.y).magnitude;
            // 线速度模
            speed = linerSpeed.magnitude;
            omga = speed * speed / radius;

            // Vector2 fp = new Vector2(circleDot.x - this.transform.position.x, circleDot.y - this.transform.position.y);     //向心力矢量，但此时向量模不正确（不然会有吸引力问题）
            // Debug.Log(new Vector2(circleDot.x - this.transform.position.x, circleDot.y - this.transform.position.y));
            // fp = fp.normalized * body.mass * omga;                      //纠正向量的模
            // Debug.Log(fp);
            // body.AddForce(fp, ForceMode2D.Force);
        }
        else
        {
            Vector2 fp = new Vector2(0,0);
            if(flag != 1 )
                // 圆心和线速度
                circleDot = new Vector2(this.transform.position.x + 5, this.transform.position.y + 5);
                //Debug.Log("初始位置" + circleDot);
                linerSpeed = V3RotateAround(new Vector2(circleDot.x - this.transform.position.x, circleDot.y - this.transform.position.y), new Vector3(0, 0, -1), -90);
                // 线速度
                body.velocity = linerSpeed;
                // 半径
                radius = new Vector2(circleDot.x - this.transform.position.x, circleDot.y - this.transform.position.y).magnitude;
                // 线速度模
                speed = linerSpeed.magnitude;
                omga = speed * speed / radius;
                fp = new Vector2(circleDot.x - this.transform.position.x, circleDot.y - this.transform.position.y);     //向心力矢量，但此时向量模不正确（不然会有吸引力问题）
                fp = fp.normalized * body.mass * omga;                      //纠正向量的模
                body.AddForce(fp);
                
        }


        // 遇到特殊情况改变旋转方向
        // 转向
        // circleDot.x = this.transform.position.x + this.transform.position.x - circleDot.x;
        // circleDot.y = this.transform.position.y + this.transform.position.y - circleDot.y;
    }

    // 旋转相量
    public Vector3 V3RotateAround(Vector2 source, Vector3 axis, float angle)
    {
        Quaternion q = Quaternion.AngleAxis(angle, axis);   // 旋转系数
        return q * source;                                  // 返回目标点
    }

    void OnDrawGizmos()
    {
         Gizmos.DrawLine(circleDot,this.transform.position);
    }

    void LateUpdate()
    {
        camera.transform.position = new Vector3(camera.transform.position.x, this.transform.position.y, -5);
    }
}
