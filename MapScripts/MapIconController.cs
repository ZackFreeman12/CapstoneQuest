using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapIconController : MonoBehaviour
{
    public string[] tagsArray = new string[4];

    public SpriteRenderer spr;
    public Sprite Loot;
    public Sprite Buff;
    public Sprite Shop;
    public Sprite Combat;
    string newTag;
    public int level;
    public int branch;

    void Awake()
    {
        if (MainManager.instance.newGame)
        {
            MainManager.instance.icons.Add(gameObject);
            int randomIndex = Random.Range(0, 4);
            string randomTag = tagsArray[randomIndex];
            newTag = randomTag;

            switch (randomTag)
            {
                case "Loot":
                    spr.sprite = Loot;
                    break;
                case "Buff":
                    spr.sprite = Buff;
                    break;
                case "Shop":
                    spr.sprite = Shop;
                    break;
                case "Combat":
                    spr.sprite = Combat;
                    break;
            }

            DontDestroyOnLoad(gameObject);
        }
        if (!MainManager.instance.newGame)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        
    }

    void OnMouseDown()
    {
        if (this.enabled)
        {
            branch = MainManager.instance.branch;
            level = MainManager.instance.level;

            switch (newTag)
            {
                case "Loot":
                    SceneManager.LoadScene("Loot");
                    MainManager.instance.level += 1;
                    break;
                case "Buff":
                    SceneManager.LoadScene("Buff");
                    MainManager.instance.level += 1;
                    break;
                case "Shop":
                    SceneManager.LoadScene("Shop");
                    MainManager.instance.level += 1;
                    break;
                case "Combat":
                    SceneManager.LoadScene("Combat");
                    MainManager.instance.level += 1;
                    break;
            }
            AudioManager.instance.Play(0);

            if (level == 1)
            {
                this.enabled = false;
                gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);

                switch (branch)
                {
                    case 1:

                        GameObject.FindWithTag("7").GetComponent<SpriteRenderer>().color =
                            new Color(1f, 1f, 1f, 1f);
                        GameObject.FindWithTag("7").GetComponent<MapIconController>().enabled =
                            true;
                        break;
                    case 2:

                        GameObject.FindWithTag("8").GetComponent<SpriteRenderer>().color =
                            new Color(1f, 1f, 1f, 1f);
                        GameObject.FindWithTag("8").GetComponent<MapIconController>().enabled =
                            true;
                        break;
                    case 3:

                        GameObject.FindWithTag("9").GetComponent<SpriteRenderer>().color =
                            new Color(1f, 1f, 1f, 1f);
                        GameObject.FindWithTag("9").GetComponent<MapIconController>().enabled =
                            true;
                        break;
                }
            }

            if (level == 2)
            {
                this.enabled = false;
                gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);
                switch (branch)
                {
                    case 1:

                        GameObject.FindWithTag("10").GetComponent<SpriteRenderer>().color =
                            new Color(1f, 1f, 1f, 1f);
                        GameObject.FindWithTag("10").GetComponent<MapIconController>().enabled =
                            true;
                        break;
                    case 2:

                        GameObject.FindWithTag("10").GetComponent<SpriteRenderer>().color =
                            new Color(1f, 1f, 1f, 1f);
                        GameObject.FindWithTag("10").GetComponent<MapIconController>().enabled =
                            true;
                        GameObject.FindWithTag("11").GetComponent<SpriteRenderer>().color =
                            new Color(1f, 1f, 1f, 1f);
                        GameObject.FindWithTag("11").GetComponent<MapIconController>().enabled =
                            true;
                        break;
                    case 3:

                        GameObject.FindWithTag("11").GetComponent<SpriteRenderer>().color =
                            new Color(1f, 1f, 1f, 1f);
                        GameObject.FindWithTag("11").GetComponent<MapIconController>().enabled =
                            true;
                        break;
                }
            }

            if (level == 3)
            {
                GameObject.FindWithTag("11").GetComponent<MapIconController>().enabled = false;
                GameObject.FindWithTag("11").GetComponent<SpriteRenderer>().color = new Color(
                    1f,
                    1f,
                    1f,
                    0.5f
                );
                GameObject.FindWithTag("10").GetComponent<MapIconController>().enabled = false;
                GameObject.FindWithTag("10").GetComponent<SpriteRenderer>().color = new Color(
                    1f,
                    1f,
                    1f,
                    0.5f
                );

                GameObject.FindWithTag("12").GetComponent<SpriteRenderer>().color = new Color(
                    1f,
                    1f,
                    1f,
                    1f
                );
                GameObject.FindWithTag("12").GetComponent<Boss1>().enabled = true;
            }

            foreach (GameObject icon in MainManager.instance.icons)
            {
                icon.SetActive(false);
            }
            foreach (GameObject icon in MainManager.instance.combatIcons)
            {
                icon.SetActive(false);
            }
        }
    }
}
