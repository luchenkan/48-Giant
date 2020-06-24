using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

    public static ScoreManager Instance;
    public int curScore;

    private string scoreName;

    private float deltaTime;

    public float intervalTime;

    public float shrinkRatio = 1.5f;

    public TextMesh curText;

    public ScoreBall scoreBall;
    void Awake()
    {
        scoreName = "NormalScore";
    }
    void Start()
    {
        Instance = this;
        SpawnScoreBall();
    }

    // Update is called once per frame
    void SpawnScoreBall()
    {
        GameObject obj = PoolManager.GetPrefabFromCachePool(scoreName, transform, PoolType.None);
        ScoreBall ball = obj.GetComponent<ScoreBall>();
        if (ball)
        {
            float scoreBallx = Random.Range(CheckBorder.Instance.leftBorder + shrinkRatio, CheckBorder.Instance.rightBorder - shrinkRatio);
            float scoreBallY = Random.Range(CheckBorder.Instance.downBorder + shrinkRatio, CheckBorder.Instance.topBorder - shrinkRatio);
            obj.transform.position = new Vector3(scoreBallx, scoreBallY);
            ball.Init();
            scoreBall = ball;
        }
    }

    public IEnumerator DespawnScoreBall(GameObject go)
    {
        PoolManager.RecycleGameObject(go);
        scoreBall = null;
        yield return new WaitForSeconds(0.5f);
        SpawnScoreBall();
    }

    public void AddScore(int score)
    {
        curScore += score;
        curText.text = curScore + "";
        ObstacleManager.Instance.intervalTime = 1 / (0.5f * Mathf.Pow(1.013f, curScore));
    }

    public void ResetScore()
    {
        curScore = 0;
        curText.text = curScore + "";
        ObstacleManager.Instance.intervalTime = 1 / (0.5f * Mathf.Pow(1.013f, curScore));
    }
}
