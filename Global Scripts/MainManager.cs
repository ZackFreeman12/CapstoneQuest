using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager instance;
    public int level;
    public int branch;
    public bool newGame = true;
    public List<GameObject> icons = new List<GameObject>();
    public List<GameObject> combatIcons = new List<GameObject>();
    public GameObject mapCamera;
    public int difficulty;

    private void Awake()
    {
        if (MainManager.instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        Reset();
    }

    public void Reset()
    {
        level = 0;
        branch = 0;
        newGame = true;
        icons = new List<GameObject>();
        combatIcons = new List<GameObject>();
        difficulty = 2;
        AudioManager.instance.Play(1);
        AudioManager.instance.sounds[2].Stop();
    }
}
