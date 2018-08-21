using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMgr : MonoBehaviour {
    private static GameMgr _instance;
    public static GameMgr Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameMgr>();
            }
            return _instance;
        }
    }

    public float timer;
    int points;
    public Train player;

    public bool isOnStation;
    public bool isRewardGot;
    public bool freeCam = false;
    public GameObject mainCam;
    public MouseLook m_MouseLook;

    void Start()
    {
        AddListener();
        Time.timeScale = 1;
        timer = 300f;
        points = 0;
    }

    public void AddListener()
    {
        GuiMgr.Instance.slider.onValueChanged.AddListener(delegate { player.ApplySpeed(GuiMgr.Instance.slider.value); });
    }

    public void GetPoint()
    {
        points++;
        if (!SndGameManager.Instance.sndPoint.isPlaying)
        {
            SndGameManager.Instance.sndPoint.Play();
        }
        isRewardGot = true;
        GuiMgr.Instance.get.SetActive(false);
        GuiMgr.Instance.points.text = "Points: " + points;
    }

    public void ChangeCam()
    {
        freeCam = !freeCam;
    }

    public void Gudok()
    {
        //звук
        if (!SndGameManager.Instance.sndGudok.isPlaying)
        {
            SndGameManager.Instance.sndGudok.Play();
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
        //SceneManager.LoadSceneAsync(0);
        //Application.LoadLevel("Scebe");
    }

    public void Update()
    {
        timer -= Time.deltaTime;
        GuiMgr.Instance.timer.text = ((int)timer / 60).ToString() + ":" + ((int)timer - ((int)timer / 60)*60).ToString("00");
        if(timer<=0)
        {
            Time.timeScale = 0;
            //конец игры
            GuiMgr.Instance.endGame.SetActive(true);
            GuiMgr.Instance.totalPoints.text = "Your Reward: " + points;
        }

    }


}
