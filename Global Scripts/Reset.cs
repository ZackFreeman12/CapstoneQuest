using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset : MonoBehaviour
{
    void Start()
    {
        if (MainManager.instance != null)
        {
            if (!MainManager.instance.newGame)
            {
                MainManager.instance.Reset();
                PlayerManager.instance.Reset();
                return;
            }
        }
    }

    // Update is called once per frame
    void Update() { }
}
