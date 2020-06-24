/*
 * @小球的吸引力
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttracForce : MonoBehaviour
{
    /// <summary>
    /// 一边环绕，一边躲避
    /// </summary>

    // 被吸引球的刚体
    private Rigidbody _rigid;
    // 吸引力系数
    public float _force = 20;
    // 速度系数
    public float _speedNum = 1.0f;
    // 吸引中心
    public Vector3 _origin;
    // 绕轴半径
    public float _radius = 5.0f;

    void Start()
    {
        _rigid = this.GetComponent<Rigidbody>();
        _origin = new Vector3(0, 0, this.transform.position.z);
    }

    void FixedUpdate()
    {
        if (_rigid == null)
            return;

        // 吸引力方向
        var _vector = _origin - this.transform.position;

        // 施加引力
        // _rigid.AddForce(_vector * _force,ForceMode.Impulse);

        // 切线方向
        Vector3 _vectorQ = V3RotateAround(_vector,new Vector3(0,0,-1),-90);

        // 切线速度
        _rigid.velocity = _vectorQ * _speedNum;

        // 绕轴旋转
        this.transform.RotateAround(_origin,Vector3.forward,1.5f);

        if(Input.GetKey(KeyCode.Space))
        {
            // _origin += (_origin - this.transform.position);
        }

    }

    // 旋转相量
    public Vector3 V3RotateAround(Vector3 source, Vector3 axis, float angle)
    {
        Quaternion q = Quaternion.AngleAxis(angle, axis);// 旋转系数
        return q * source;                               // 返回目标点
    }
}
