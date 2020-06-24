using UnityEngine;
using System.Collections;

public class RoundAction : MonoBehaviour
{
    public float _radius_length;
    public float _angle_speed;

    private float temp_angle;

    private Vector3 _pos_new;

    public Vector3 _center_pos;

    public bool _round_its_center;

    // 刚体
    private Rigidbody2D body;

    // Use this for initialization
    void Start()
    {
        body = gameObject.GetComponent<Rigidbody2D>();
        body.velocity = new Vector2(0, -2);

        if (_round_its_center)
        {
            _center_pos = transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        temp_angle += _angle_speed * Time.deltaTime; // 

        _pos_new.x = _center_pos.x + Mathf.Cos(temp_angle) * _radius_length;
        _pos_new.y = _center_pos.y + Mathf.Sin(temp_angle) * _radius_length;
        _pos_new.z = transform.position.z;

        transform.position = _pos_new;

        //Vector3 fp = _center_pos - transform.localPosition;

        if (Input.GetKey(KeyCode.Space))
        {
            
        }
    }

}