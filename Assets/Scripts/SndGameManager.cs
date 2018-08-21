using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SndGameManager : MonoBehaviour {
    private static SndGameManager _instance;
    public static SndGameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SndGameManager>();
            }
            return _instance;
        }
    }

    public AudioSource sndGudok;
    public AudioSource sndPoint;
}
