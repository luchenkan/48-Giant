using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour {

    public float speed = 5;

    public Vector3 moveVec;

    private bool clockwise;

    public float sphereRadius;

    public float angularSpeed;//角速度
    public float aroundRadius;//半径

    private Vector3 aroundPoint;
    private float angled;

    private float timeCounter;

    private Vector3 mCurVelocity;
    void Start () {
        clockwise = true;
        angularSpeed = 180;
        aroundRadius = 1;
        Reset();
    }

    public void Reset()
    {
        transform.position = Vector3.zero;
        GetAroundPoint(-Vector3.up);
    }

    // Update is called once per frame
    void Update () {
        if (SceneManager.Instance.isDie) return;

        //moveVec = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        //transform.position += moveVec.normalized * Time.deltaTime * speed;
        //CheckBorder.Instance.CheckBorderByTransform(transform, sphereRadius, sphereRadius);

        if (CheckBorder.Instance.CheckOutOfBorder(transform, sphereRadius, sphereRadius))
        {
            SceneManager.Instance.GameOver();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            mCurVelocity = GetRotateVector(aroundPoint - transform.position);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            transform.position += mCurVelocity.normalized * 5 * Time.deltaTime;
            return;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            GetAroundPoint(mCurVelocity);
        }
        angled += (angularSpeed * Time.deltaTime) % 360;//累加已经转过的角度
        float posX = aroundRadius * Mathf.Sin(angled * Mathf.Deg2Rad);//计算x位置
        float posY = aroundRadius * Mathf.Cos(angled * Mathf.Deg2Rad);//计算y位置

        transform.position = new Vector3(posX, posY, 0) + aroundPoint;//更新位置

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Obstacle ob = other.GetComponent<Obstacle>();
        if (ob != null)
        {
            switch (ob.obstacleType)
            {
                case ObstacleType.Normal:
                    Debug.Log("普通障碍物");
                    SceneManager.Instance.GameOver();
                    break;
                case ObstacleType.Addon:
                    Debug.Log("清屏障碍物");
                    ScoreManager.Instance.AddScore(3);
                    AudioController.Instance.PlayUISound("Score2");
                    PoolManager.RecycleGameObject(ob.gameObject);
                    break;
                case ObstacleType.Circle:
                    Debug.Log("方向反转");
                    //clockwise = !clockwise;
                    PoolManager.RecycleGameObject(ob.gameObject);
                    break;
                case ObstacleType.Bomb:
                    SceneManager.Instance.StartCoroutine(SceneManager.Instance.Bomb());
                    break;
                case ObstacleType.Stop:
                    PoolManager.RecycleGameObject(ob.gameObject);
                    SceneManager.Instance.StartCoroutine(SceneManager.Instance.Stop());
                    break;
                default:
                    break;
            }
        }
        ScoreBall sb = other.GetComponent<ScoreBall>();
        if (sb != null)
        {
            int addScore = 0;
            switch (sb.scrollBallType)
            {
                case ScoreBallType.Normal:
                    addScore = 1;
                    AudioController.Instance.PlayUISound("Score1");
                    ScoreManager.Instance.StartCoroutine(ScoreManager.Instance.DespawnScoreBall(sb.gameObject));
                    break;
                case ScoreBallType.AddBall:
                    addScore = 3;
                    break;
                case ScoreBallType.RotateBall:
                    break;
                case ScoreBallType.BombBall:
                    break;
                case ScoreBallType.TimingBall:
                    break;
                default:
                    break;
            }
            ScoreManager.Instance.AddScore(addScore);
        }
    }


    void GetAroundPoint(Vector3 velocity)
    {
        //Vector3 p = aroundPoint.rotation * Vector3.forward * aroundRadius;
        //transform.position = new Vector3(p.x, aroundPoint.position.y, p.z);
        Quaternion q = Quaternion.AngleAxis(90, new Vector3(0, 0, clockwise ? -1 : 1));
        Vector3 offset = q * velocity;
        aroundPoint = transform.position + offset.normalized * aroundRadius;
        Vector3 from = Vector3.up * (clockwise ? 1 : -1);
        Vector3 to = -offset.normalized;
        Vector3 v3 = Vector3.Cross(from, to);
        if (v3.z <= 0)
        {
            angled = Vector3.Angle(from, to);
        }
        else
        {
            angled = 360 - Vector3.Angle(from, to);
        }
    }

    Vector3 GetRotateVector(Vector3 velocity)
    {
        Quaternion q = Quaternion.AngleAxis(-90, new Vector3(0, 0, clockwise ? -1 : 1));
        Vector3 offset = q * velocity;
        return offset;
    }
}
