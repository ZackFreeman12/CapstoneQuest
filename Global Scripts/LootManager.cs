using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootManager : MonoBehaviour
{
public static LootManager instance;
public List<int> allLoot = new List<int>();

private void Awake()
    {
        if (LootManager.instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            allLoot.Add(1);
            allLoot.Add(2);
            allLoot.Add(3);
            allLoot.Add(4);
            allLoot.Add(5);
            allLoot.Add(6);
            allLoot.Add(7);
        }

    }
     public string[] LootToString(int ID){
        string[] nameDesc = new string [2];

        switch(ID){
            case 1:
                nameDesc[0] = "Vampiric Touch";
                nameDesc[1] = "You can no longer use rest or hibernate, increase healing and damage of vampiric attacks by 10";
                return nameDesc;
            
            case 2:
                nameDesc[0] = "Travelers Gear";
                nameDesc[1] = "Reduce damage taken by 25%";
                

                return nameDesc;
            case 3:
                nameDesc[0] = "Potion Vial";
                nameDesc[1] = "Use to restore 50 resolve or health once per battle";
                
                return nameDesc;
            case 4:
                nameDesc[0] = "Snack Bar";
                nameDesc[1] = "Increase max health by 50";
                
                return nameDesc;
            case 5:
                nameDesc[0] = "Bigger Sword";
                nameDesc[1] = "Increase Strength by 5";
                return nameDesc;
            case 6:
                nameDesc[0] = "Hurricane Blade";
                nameDesc[1] = "All strikes damage all enemies (AoE)";
                return nameDesc;
            case 7:
                nameDesc[0] = "Smoke Bomb";
                nameDesc[1] = "Use to skip non boss combat, receive no rewards";
                return nameDesc;
        
        }
        nameDesc[0] = "Invalid ID";
        nameDesc[1] = "Invalid ID";
        return nameDesc;
    }
}
