using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public float HP;
    public float STR;
    public int AP;
    public string type;
    private int turns;
    public int spikes;


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
    public bool stun;
    public bool boss;


    void Awake()
    {
        spikes = 0;
        //difficulty scaling based of 1-5 based on the level you are on
        //used in different ways to hopefully make later level combats more challenging
        int difficultyScaling = MainManager.instance.level;

        
        if(boss == true){
            
            HP = 600;
            STR = 1;
            type = "boss";
            turns = 0;
            stun = false;
            alt = false;
            weak = false;
        }
        else{
            int randomInt = Random.Range(0, 10);
            if (randomInt <= 6 - difficultyScaling)
            {
            
                HP = 50f * difficultyScaling;
                STR = 1f;
                AP = 1;
                type = "grunt";
                if( GameObject.FindWithTag("Grunt") != null && GameObject.FindWithTag("Grunt").transform.position.y != gameObject.transform.position.y){
                    
                    GameObject.FindWithTag("Grunt").transform.parent = gameObject.transform;
                    GameObject.FindWithTag("Grunt").transform.position = gameObject.transform.position;
                }
                else if(GameObject.FindWithTag("Grunt2") != null &&  GameObject.FindWithTag("Grunt2").transform.position.y != gameObject.transform.position.y){
                    
                    GameObject.FindWithTag("Grunt2").transform.parent = gameObject.transform;
                    GameObject.FindWithTag("Grunt2").transform.position = gameObject.transform.position;
                }
                else{
                   
                    GameObject.FindWithTag("Grunt3").transform.parent = gameObject.transform;
                    GameObject.FindWithTag("Grunt3").transform.position = gameObject.transform.position;
                }
                
            }
            else if (randomInt % 2 == 0)
            {
                HP = 150f * difficultyScaling;
                STR = 1f;
                AP = 1;
                type = "bruiser";
                if(GameObject.FindWithTag("Bruiser") != null && GameObject.FindWithTag("Bruiser").transform.position.y != gameObject.transform.position.y){
                    
                    GameObject.FindWithTag("Bruiser").transform.parent = gameObject.transform;
                    GameObject.FindWithTag("Bruiser").transform.position = gameObject.transform.position;
                }
                else if( GameObject.FindWithTag("Bruiser2") != null && GameObject.FindWithTag("Bruiser2").transform.position.y != gameObject.transform.position.y){
                    
                    GameObject.FindWithTag("Bruiser2").transform.parent = gameObject.transform;
                    GameObject.FindWithTag("Bruiser2").transform.position = gameObject.transform.position;
                }
                else{
                    
                    GameObject.FindWithTag("Bruiser3").transform.parent = gameObject.transform;
                    GameObject.FindWithTag("Bruiser3").transform.position = gameObject.transform.position;
                }
            }
            else
            {
                HP = 100f * difficultyScaling;
                STR = 1f;
                AP = 2;
                type = "wizard";
                if( GameObject.FindWithTag("Wizard") != null && GameObject.FindWithTag("Wizard").transform.position.y != gameObject.transform.position.y){
                    
                    GameObject.FindWithTag("Wizard").transform.parent = gameObject.transform;
                    GameObject.FindWithTag("Wizard").transform.position = gameObject.transform.position;
                }
                else if( GameObject.FindWithTag("Wizard2") != null && GameObject.FindWithTag("Wizard2").transform.position.y != gameObject.transform.position.y){
                    
                    GameObject.FindWithTag("Wizard2").transform.parent = gameObject.transform;
                    GameObject.FindWithTag("Wizard2").transform.position = gameObject.transform.position;
                }
                else{
                    
                    GameObject.FindWithTag("Wizard3").transform.parent = gameObject.transform;
                    GameObject.FindWithTag("Wizard3").transform.position = gameObject.transform.position;
                }
            }
        }
        
        alt = false;
    }
    void Start(){

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
                DMG[1] = 0f;
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
                DMG[0] = 5f * STR;
                DMG[1] = 4f;
                alt = false;
                turns += 1;
            }

            if(PlayerManager.instance.equippedLoot.Contains(2) && DMG[0] > 0){
                DMG[0] *= PlayerManager.instance.dmgResistance;
            } 
        }
        
        return DMG;
    }
    public float[] Grunt()
    {
        
        if (turns % 2 == 0)
        {
            DMG[0] = 10f * STR;
            DMG[1] = 0f;
            turns += 1;
        }
        else
        {
            DMG[0] = 0;
            DMG[1] = 0f;
            STR += 1f;
            turns += 1;
        }
        if(PlayerManager.instance.equippedLoot.Contains(2) && DMG[0] > 0){
            DMG[0] *= PlayerManager.instance.dmgResistance;
        }
        return DMG;
    }

    public float[] Bruiser()
    {
        
        if (turns % 2 == 0)
        {
            DMG[0] = 10f * STR;
            DMG[1] = 0f;
            turns += 1;
        }
        else if (alt == false)
        {
            DMG[0] = 8f * STR;
            DMG[1] = 1f;
            alt = true;
            turns += 1;
        }
        else
        {
            DMG[0] = 0f;
            DMG[1] = 0f;
            spikes += 10;
            alt = false;
            turns += 1;
            AudioManager.instance.Play(13);
        }
        if(PlayerManager.instance.equippedLoot.Contains(2) && DMG[0] > 0){
            DMG[0] *= PlayerManager.instance.dmgResistance;
        }
        return DMG;
    }

    public float[] Wizard()
    {
       
        if (turns % 2 == 0)
        {
            DMG[0] = 15f * STR;
            DMG[1] = 0f;
            turns += 1;
        }
        else if (alt == false)
        {
            DMG[0] = 0f;
            DMG[1] = 3f;
            alt = true;
            turns += 1;
        }
        else
        {
            DMG[0] = 0f;
            DMG[1] = 4f;
            alt = false;
            turns += 1;
        }

        if(PlayerManager.instance.equippedLoot.Contains(2) && DMG[0] > 0){
            DMG[0] *= PlayerManager.instance.dmgResistance;
        }

        return DMG;
    }
    /* public void SpawnMinions()
    {
        if( GameObject.FindWithTag("1") != null){
            if( GameObject.FindWithTag("1").GetComponent<EnemyManager>().enabled){

            }
            else{
                 GameObject.FindWithTag("1").GetComponent<EnemyManager>().enabled = true;
                 if(GameObject.FindWithTag("1").GetComponent<EnemyManager>().type.Equals("grunt")){
                    GameObject.FindWithTag("1").GetComponent<EnemyManager>().HP = 50f;
                    GameObject.FindWithTag("1").GetComponent<EnemyManager>().STR = 1f;
                    GameObject.FindWithTag("1").GetComponent<EnemyManager>().AP = 1;
                 }
                 else if(GameObject.FindWithTag("1").GetComponent<EnemyManager>().type.Equals("bruiser")){
                    GameObject.FindWithTag("1").GetComponent<EnemyManager>().HP = 150f;
                    GameObject.FindWithTag("1").GetComponent<EnemyManager>().STR = 1f;
                    GameObject.FindWithTag("1").GetComponent<EnemyManager>().AP = 1;
                 }
                 else if(GameObject.FindWithTag("1").GetComponent<EnemyManager>().type.Equals("Wizard")){
                    GameObject.FindWithTag("1").GetComponent<EnemyManager>().HP = 100f;
                    GameObject.FindWithTag("1").GetComponent<EnemyManager>().STR = 1f;
                    GameObject.FindWithTag("1").GetComponent<EnemyManager>().AP = 1;
                 }
            }
        }
        if( GameObject.FindWithTag("2") != null){
            if( GameObject.FindWithTag("2").GetComponent<EnemyManager>().enabled){

            }
            else{
                 GameObject.FindWithTag("2").GetComponent<EnemyManager>().enabled = true;
                 if(GameObject.FindWithTag("2").GetComponent<EnemyManager>().type.Equals("grunt")){
                    GameObject.FindWithTag("2").GetComponent<EnemyManager>().HP = 50f;
                    GameObject.FindWithTag("2").GetComponent<EnemyManager>().STR = 1f;
                    GameObject.FindWithTag("2").GetComponent<EnemyManager>().AP = 1;
                 }
                 else if(GameObject.FindWithTag("2").GetComponent<EnemyManager>().type.Equals("bruiser")){
                    GameObject.FindWithTag("2").GetComponent<EnemyManager>().HP = 150f;
                    GameObject.FindWithTag("2").GetComponent<EnemyManager>().STR = 1f;
                    GameObject.FindWithTag("2").GetComponent<EnemyManager>().AP = 1;
                 }
                 else if(GameObject.FindWithTag("2").GetComponent<EnemyManager>().type.Equals("Wizard")){
                    GameObject.FindWithTag("2").GetComponent<EnemyManager>().HP = 100f;
                    GameObject.FindWithTag("2").GetComponent<EnemyManager>().STR = 1f;
                    GameObject.FindWithTag("2").GetComponent<EnemyManager>().AP = 1;
                 }
                 GameObject.FindWithTag("2").GetComponent<Animator>().SetBool("isDead",false);
            }
        }
    } */
}
