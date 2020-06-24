using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ScoreBallType
{
    Normal = 0,
    AddBall = 1,
    RotateBall = 2,
    BombBall = 3,
    TimingBall = 4,
}

public class ScoreBall : MonoBehaviour {

    public ScoreBallType scrollBallType;

    public float shrinkSize = 1.5f;

    public float increaseSpeed = 0.1f;
    // Use this for initialization
    void Start () {
        if (scrollBallType == ScoreBallType.Normal || scrollBallType == ScoreBallType.AddBall)
        {
            Init();
        }
    }
	
	// Update is called once per frame
	void Update () {
        float scaleDelta = transform.localScale.x + increaseSpeed;
        if (scaleDelta >= 1)
            scaleDelta = 1;
        switch (scrollBallType)
        {
            case ScoreBallType.Normal:
                if (transform.localScale.x != scaleDelta)
                    transform.localScale = Vector3.one * scaleDelta;
                break;
            case ScoreBallType.AddBall:
                if (transform.localScale.x != scaleDelta)
                    transform.localScale = Vector3.one * scaleDelta;
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
    }

    public void Init()
    {
        transform.localScale = Vector3.zero;
    }
}
