using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultPanelCtrl : MonoBehaviour {

    public Text scoreText;

    public Button restartButton;

    public int score;

    void Start()
    {
        restartButton.onClick.AddListener(()=> RestartButtonClicked());
    }

    void OnEnable()
    {
        scoreText.text = score + "";
    }

    public void RestartButtonClicked()
    {
        gameObject.SetActive(false);
        SceneManager.Instance.Restart();
    }
}
