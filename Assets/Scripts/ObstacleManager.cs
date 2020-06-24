using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour {

    public static ObstacleManager Instance;

    private string[] obstacleName, specialnames;

    private float deltaTime, specialTime;

    public float intervalTime, specialIntervalTime; 
    void Awake()
    {
        obstacleName = new string[] { "Cube", "LongCube", "ThirdCube" };

        specialnames = new string[] { "AddonScore", "Circle", "Bomb", "Stop" };

        Init();
    }

	// Use this for initialization
	void Start () {
        Instance = this;
    }
	
	// Update is called once per frame
	void Update () {
        if (SceneManager.Instance.isStop || SceneManager.Instance.isDie) return;
        deltaTime += Time.deltaTime;
        if (deltaTime >= intervalTime)
        {
            deltaTime = 0f;
            int index = Random.Range(0, obstacleName.Length);
            GameObject obj = PoolManager.GetPrefabFromCachePool(obstacleName[index], transform, PoolType.None);
            Obstacle obstacle = obj.GetComponent<Obstacle>();
            if (obstacle)
            {
                if (obstacle.GetSpeed() != 0)
                {
                    Debug.LogError(obstacle.name, obj);
                }
                float ranged = Random.Range(0f, 1f);
                bool dir = ranged > 0.5f;
                obj.transform.position = new Vector3(CheckBorder.Instance.GetObstacleOffset(dir, obstacle.width), Random.Range(CheckBorder.Instance.topBorder - obstacle.height, CheckBorder.Instance.downBorder + obstacle.height));
                obstacle.Init();
            }
        }
        specialTime += Time.deltaTime;
        if (specialTime >= specialIntervalTime)
        {
            specialTime = 0f;
            int index = Random.Range(0, specialnames.Length);
            GameObject obj = PoolManager.GetPrefabFromCachePool(specialnames[index], transform, PoolType.None);
            Obstacle obstacle = obj.GetComponent<Obstacle>();
            if (obstacle)
            {
                if (obstacle.GetSpeed() != 0)
                {
                    Debug.LogError(obstacle.name, obj);
                }
                float ranged = Random.Range(0f, 1f);
                bool dir = ranged > 0.5f;
                obj.transform.position = new Vector3(CheckBorder.Instance.GetObstacleOffset(dir, obstacle.width), Random.Range(CheckBorder.Instance.topBorder - obstacle.height, CheckBorder.Instance.downBorder + obstacle.height));
                obstacle.Init();
            }
        }
    }
    public void StopAllObstacle()
    {
        PoolManager.GetAllActiveGameobject().ForEach(obstacle =>
        {
            Obstacle ob = obstacle.GetComponent<Obstacle>();
            if (ob != null)
            {
                ob.StopMove();
            }
        });
    }

    public void RecoverAllObstacle()
    {

        PoolManager.GetAllActiveGameobject().ForEach(obstacle =>
        {
            Obstacle ob = obstacle.GetComponent<Obstacle>();
            if (ob != null)
            {
                ob.RecoverMove();
            }
        });
    }

    public void RecycleAllObstacle()
    {
        PoolManager.GetAllActiveGameobject().ForEach(obstacle =>
        {
            Obstacle ob = obstacle.GetComponent<Obstacle>();
            if (ob != null)
            {
                PoolManager.RecycleGameObject(ob.gameObject);
            }
        });
    }


    public void Init()
    {
        intervalTime = 2;
        deltaTime = intervalTime;

        specialTime = 8;
        specialIntervalTime = specialTime;
    }
}
