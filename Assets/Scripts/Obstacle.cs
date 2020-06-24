using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObstacleType
{
    None = -1,
    Normal = 0,
    Addon = 1,
    Circle = 2,
    Bomb = 3,
    Stop = 4,
}


public class Obstacle : MonoBehaviour {

    private bool direction;

    private float speed, defaultSpeed;

    public ObstacleType obstacleType;

    public float width, height;

    // Use this for initialization
    void Awake () {
        width = width / 2;
        height = height / 2;
        defaultSpeed = 2;
    }

    public void Init()
    {
        RecoverMove();
        direction = transform.position.x > 0;
    }
	
	// Update is called once per frame
	void Update () {
        transform.position += new Vector3((direction ? -1 : 1) * speed * Time.deltaTime, 0, 0);
        if (speed != 0 && CheckBorder.Instance.CheckBeyondBorder(transform, width))
        {
            StopMove();
            PoolManager.RecycleGameObject(gameObject);
        }
    }

    public void StopMove()
    {
        speed = 0;
    }

    public float GetSpeed()
    {
        return speed;
    }

    public void RecoverMove()
    {
        speed = defaultSpeed;
    }

}
