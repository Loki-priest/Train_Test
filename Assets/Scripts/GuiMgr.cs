using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuiMgr : MonoBehaviour {
    private static GuiMgr _instance;
    public static GuiMgr Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GuiMgr>();
            }
            return _instance;
        }
    }

    void Start()
    {
        points.text = "Points: " + 0;
    }

    [Header("Тексты в игре")]
    public Text timer;
    public Text points;
    public Text speed;

    [Header("Конец игры")]
    public GameObject endGame;
    public Text totalPoints;

    [Header("Взаимодействие")]
    public GameObject stop;
    public GameObject get;
    public Slider slider;






}
