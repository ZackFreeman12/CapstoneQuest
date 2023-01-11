using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public float HP;
    public float STR;
    public int AP;
    public string type;
    private int turns;
    public int spikes;
    public bool stun;


    /* public SpriteRenderer sprBody;
    public SpriteRenderer sprRightLeg;
    public SpriteRenderer sprLeftLeg;
    public SpriteRenderer sprArms;
    public SpriteRenderer sprHead;
    public SpriteRenderer sprRightArm;
    public SpriteRenderer sprLeftArm;
    public Sprite gruntBody;
    public Sprite gruntRightLeg;
    public Sprite gruntLeftLeg;
    public Sprite bruiserBody;
    public Sprite bruiserRightLeg;
    public Sprite bruiserLeftLeg;
    public Sprite bruiserHead;
    public Sprite bruiserRightArm;
    public Sprite bruiserLeftArm;
    public Sprite wizardHead;
    public Sprite wizardArms;
    public Sprite wizardArmRight;

    public Sprite wizardBody; */

    public float[] DMG = new float[2];
    public bool alt;
    public bool weak;

    void Awake()
    {
        spikes = 0;
        HP = 600;
        STR = 1;
        type = "boss";
        turns = 0;
        stun = false;
        alt = false;
        weak = false;
        //difficulty scaling based of 1-5 based on the level you are on
        //used in different ways to hopefully make later level combats more challenging
        int difficultyScaling = MainManager.instance.level;

        
        alt = false;
    }

    /**
    Methods on each enemy object to handle different enemy type behaviors
    
    DMG int array carry's a damage number to be dealt to the player in [0]
    and a number referring to a status effect in [1]
    adding a legend for reference to status effects.
    
    1 = Weak
    2 = Spikes
    3 = Confuse
    4 = Doom
    5 = Bleed
    **/
    public float[] BossTurn()
    {
        if(!stun){
           if(weak){
                STR = STR * 0.5f;
            }
            if (turns % 2 == 0)
            {
                DMG[0] = 10f * STR;
                DMG[1] = 5f;
                turns += 1;
            }
            else if (alt == false)
            {
                DMG[0] = 0;
                DMG[1] = 4f;
                STR += 1f;
                turns += 1;
                alt = true;
            }
            else{
                SpawnMinions();
                alt = false;
            }

            if(PlayerManager.instance.equippedLoot.Contains(2) && DMG[0] > 0){
                DMG[0] *= PlayerManager.instance.dmgResistance;
            } 
        }
        
        return DMG;
    }

    public void SpawnMinions()
    {
        
    }
}

