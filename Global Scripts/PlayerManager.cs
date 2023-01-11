using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public float HP;
    public float maxHP;
    public float RES;
    public float maxRES;
    public int AP;
    public float STR;
    public int WIS;
    public int GP;
    public List<int> equippedAbilities = new List<int>();
    public List<int> availableAbilities = new List<int>();
    public List<int> equippedLoot = new List<int>();
    public bool vampire;
    public bool vampiricTouch;
    public bool deathTouch;
    public bool weak;
    public bool confuse;
    public int doom;
    public float dmgResistance;
    public bool potion;
    public bool hurricane;
    public int smokeBomb;
    public bool bleed;

    private void Awake()
    {
        if (PlayerManager.instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        Reset();
    }

    public void newItem(int ID){
        switch(ID){
            case 1:
                vampiricTouch = true;
                if(availableAbilities.Contains(5)){
                    vampire = true;
                }
                break;
            case 2:
               dmgResistance -= 0.25f;
                break;
            case 3:
                potion = true;
                break;
            case 4:
                maxHP += 50;
                HP += 50;
                break;
            case 5:
                STR += 5;
                break;
            case 6:
                hurricane = true;
                break;
            case 7:
                smokeBomb += 1;
                break;
        }
        equippedLoot.Add(ID);
    }

    public void Reset()
    {
        HP = 100f;
        maxHP = 100f;
        RES = 100f;
        maxRES = 100f;
        AP = 3;
        STR = 1f;
        WIS = 1;
        GP = 50;
        vampire = false;
        vampiricTouch = false;
        deathTouch = false;
        weak = false;
        confuse = false;
        doom = 0;
        dmgResistance = 1;
        potion = false;
        smokeBomb = 0;
        hurricane = false;
        equippedAbilities.Clear();
        availableAbilities.Clear();
        equippedLoot.Clear();

       //availableAbilities.Add(1);
       //availableAbilities.Add(2);
        //availableAbilities.Add(3);
        //availableAbilities.Add(4);
        //availableAbilities.Add(5);
       //availableAbilities.Add(6);
        //availableAbilities.Add(7);
       //availableAbilities.Add(8);
       //availableAbilities.Add(9);
       //availableAbilities.Add(10);

        equippedAbilities.Add(1);
        equippedAbilities.Add(2);
        equippedAbilities.Add(3);
        equippedAbilities.Add(4);
    }
}
