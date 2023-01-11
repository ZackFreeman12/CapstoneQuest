using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss1 : MonoBehaviour
{
    void Awake()
    {
        if (MainManager.instance.newGame)
        {
            DontDestroyOnLoad(gameObject);
            this.enabled = false;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnMouseDown()
    {
        if (this.enabled)
        {
            SceneManager.LoadScene("BossCombat1");
            foreach (GameObject icon in MainManager.instance.icons)
            {
                icon.SetActive(false);
            }
            foreach (GameObject icon in MainManager.instance.combatIcons)
            {
                icon.SetActive(false);
            }
        }
        AudioManager.instance.Play(0);
        AudioManager.instance.Play(11);
        AudioManager.instance.sounds[2].Stop();
        
    }
}
