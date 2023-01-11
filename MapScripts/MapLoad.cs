using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLoad : MonoBehaviour
{
    public int level;
    public int branch;

    void Start()
    {
        MainManager.instance.newGame = false;
        if (MainManager.instance.icons != null && MainManager.instance.combatIcons != null)
        {
            branch = MainManager.instance.branch;
            level = MainManager.instance.level;

            foreach (GameObject icon in MainManager.instance.combatIcons)
            {
                icon.SetActive(true);
                if (level > 0)
                {
                    icon.GetComponent<CombatIconScript>().enabled = false;
                }
            }
            foreach (GameObject icon in MainManager.instance.icons)
            {
                icon.SetActive(true);
                if (level == 0)
                {
                    icon.GetComponent<MapIconController>().enabled = false;
                }
            }
        }
    }
}
