using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour {

    public static SceneManager Instance;

    private ObstacleManager obstacleManager;

    private ScoreManager scoreManager;

    public bool isStop, isDie;

    public GameObject stopGo;

    public SpriteRenderer spriteRender;

    public List<Sprite> spList;

    public Sphere sphere;

    public ResultPanelCtrl resultPanel;

    void Awake()
    {
        obstacleManager = GetComponent<ObstacleManager>();
        scoreManager = GetComponent<ScoreManager>();
        stopGo.gameObject.SetActive(false);
        spList = new List<Sprite>(); 
        string[] spriteName = new string[] { "Normal", "Score", "AddThree", "Bomb", "Stop", "Die" };
        for (int i = 0; i < spriteName.Length; i++)
        {
            spList.Add(Resources.Load<Sprite>("Art/" + spriteName[i]));
        }
        resultPanel.gameObject.SetActive(false);
    }

    void Start () {
        AudioController.Instance.PlayBackgroundMusic("BGM", true);
        Instance = this;
        isDie = false;
        BorderHandle(ObstacleType.None);
    }
	
    public void GameOver()
    {
        isDie = true;
        Debug.Log("游戏结束");
        obstacleManager.StopAllObstacle();
        AudioController.Instance.PlayUISound("Die");
        resultPanel.score = ScoreManager.Instance.curScore;
        sphere.gameObject.SetActive(false);
        GenerateDieFx();
        StartCoroutine(OpenGameOverPanel());
    }

    void GenerateDieFx()
    {
        GameObject go = GameObject.Instantiate((GameObject)Resources.Load("FX/SoulExplosionOrange"));
        if (sphere != null)
        {
            go.transform.position = sphere.transform.position;
        }
    }

    IEnumerator OpenGameOverPanel()
    {
        yield return new WaitForSeconds(1.5f);
        resultPanel.gameObject.SetActive(true);
    }

    public IEnumerator Stop()
    {
        isStop = true;
        obstacleManager.StopAllObstacle();
        stopGo.gameObject.SetActive(true);
        BorderHandle(ObstacleType.Stop);
        AudioController.Instance.PlayUISound("Stop");
        yield return new WaitForSeconds(5f);
        isStop = false;
        obstacleManager.RecoverAllObstacle();
        stopGo.gameObject.SetActive(false);
        BorderHandle(ObstacleType.None);
    }

    public IEnumerator Bomb()
    {
        isStop = true;
        obstacleManager.RecycleAllObstacle();
        BorderHandle(ObstacleType.Bomb);
        AudioController.Instance.PlayUISound("Bomb");
        yield return new WaitForSeconds(0.5f);
        isStop = false;
        BorderHandle(ObstacleType.None);
    }

    void StopAllObstacle()
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

    public void BorderHandle(ObstacleType obstacleType, int index = -1)
    {
        Sprite sprite = null;
        switch (obstacleType)
        {
            case ObstacleType.Normal:
                sprite = spList[5];
                break;
            case ObstacleType.Addon:
                sprite = spList[2];
                break;
            case ObstacleType.Circle:
                break;
            case ObstacleType.Bomb:
                sprite = spList[3];
                break;
            case ObstacleType.Stop:
                sprite = spList[4];
                break;
            default:
                sprite = spList[0];
                break;
        }
        if (index != -1)
            sprite = spList[index];
        spriteRender.sprite = sprite;
    }


    public void Restart()
    {
        if (isDie)
        {
            obstacleManager.RecycleAllObstacle();
            obstacleManager.Init();
            if (ScoreManager.Instance.scoreBall != null)
            {
                ScoreManager.Instance.StartCoroutine(ScoreManager.Instance.DespawnScoreBall(ScoreManager.Instance.scoreBall.gameObject));
            }
            sphere.gameObject.SetActive(true);
            sphere.Reset();
            ScoreManager.Instance.ResetScore();
            isDie = false;
        }
    }
}
