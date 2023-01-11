using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CombatIconScript : MonoBehaviour
{
    public GameObject icon;
    private int branch;

    void Awake()
    {
        if (MainManager.instance.newGame)
        {
            MainManager.instance.combatIcons.Add(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update() { }

    void OnMouseDown()
    {
        if (this.enabled)
        {
            switch (gameObject.tag)
            {
                case "1":
                    branch = 1;
                    icon = GameObject.FindWithTag("4");
                    break;
                case "2":
                    branch = 2;
                    icon = GameObject.FindWithTag("5");
                    break;
                case "3":
                    branch = 3;
                    icon = GameObject.FindWithTag("6");
                    break;
            }
            MainManager.instance.branch = branch;

            if (icon != null)
            {
                icon.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                icon.GetComponent<MapIconController>().enabled = true;
            }
            foreach (GameObject icon in MainManager.instance.combatIcons)
            {
                DontDestroyOnLoad(icon);
                icon.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);
                icon.SetActive(false);
            }
            MainManager.instance.level += 1;

            foreach (GameObject icon in MainManager.instance.icons)
            {
                icon.SetActive(false);
            }
            AudioManager.instance.Play(0);
            SceneManager.LoadScene("Combat");
        }
    }
}
