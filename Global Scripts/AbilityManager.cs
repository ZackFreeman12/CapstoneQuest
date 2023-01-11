using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
public static AbilityManager instance;

public List<int> allAbilities = new List<int>();

   private void Awake()
    {
        if (AbilityManager.instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            allAbilities.Add(1);
            allAbilities.Add(2);
            allAbilities.Add(3);
            allAbilities.Add(4);
            allAbilities.Add(5);
            allAbilities.Add(6);
            allAbilities.Add(7);
            allAbilities.Add(8);
            allAbilities.Add(9);
            allAbilities.Add(10);
        }

    }

    public string[] AbilityToString(int ID){
        string[] nameDesc = new string [3];

        switch(ID){
            case 1:
                nameDesc[0] = "Strike";
                nameDesc[1] = "1 AP: Strike any target for " + (PlayerManager.instance.STR * 10) + " damage.";
                nameDesc[2] = "target";
                return nameDesc;
            
            case 2:
                nameDesc[0] = "Defend";
                nameDesc[1] = "1 AP: Defend against enemies attacks, attacks deal damage to your resolve for 1 turn";
                nameDesc[2] = "self";

                return nameDesc;
            case 3:
                nameDesc[0] = "Sweeping Strike";
                nameDesc[1] = "1 AP: Strike all targets for " + (PlayerManager.instance.STR * 5) + " damage each. Costs 2 RES";
                nameDesc[2] = "self";
                return nameDesc;
            case 4:
                nameDesc[0] = "Rest";
                nameDesc[1] = "3 AP: Restore 10 RES";
                nameDesc[2] = "self";
                return nameDesc;
            case 5:
                nameDesc[0] = "Vampiric Strike";
                nameDesc[1] = " 1 AP: Strike any target for " + (PlayerManager.instance.STR * 10) + " damage, restoring 5 HP.";
                nameDesc[2] = "target";
                return nameDesc;
            case 6:
                nameDesc[0] = "Bolstering Strike";
                nameDesc[1] = "1 AP: Strike any target for " + (PlayerManager.instance.STR * 5) + " damage, restoring 5 RES";
                nameDesc[2] = "target";
                return nameDesc;
            case 7:
                nameDesc[0] = "Spike Defense";
                nameDesc[1] = "1 AP: Defend against enemies attacks, attacks deal damage to your resolve for 1 turn. Add 10 Spikes (deal dmg equal to spikes to attackers).";
                nameDesc[2] = "self";
                return nameDesc;
            case 8:
                nameDesc[0] = "Hibernate";
                nameDesc[1] = "3 AP: Restore 25 RES";
                nameDesc[2] = "self";
                return nameDesc;
            case 9:
                nameDesc[0] = "Weak Strike";
                nameDesc[1] = "Strike any target for " + (PlayerManager.instance.STR * 10) + " damage. Enemies struck do 50% less DMG next turn";
                nameDesc[2] = "target";
                return nameDesc;
            case 10:
                nameDesc[0] = "Death Touch";
                nameDesc[1] = "Your next attack will kill any targets";
                nameDesc[2] = "self";
                return nameDesc;
        }
        nameDesc[0] = "Invalid ID";
        nameDesc[1] = "Invalid ID";
        return nameDesc;
    }
}
