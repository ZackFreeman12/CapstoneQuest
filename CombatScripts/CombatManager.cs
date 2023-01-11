using System.ComponentModel.Design.Serialization;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CombatManager : MonoBehaviour
{
    
    public static CombatManager instance;
    public Animator playerAnim;
    public Animator enemy1Anim;
    public Animator enemy2Anim;
    public Animator enemy3Anim;
    public bool es1;
    public bool es2;
    public bool es3;
    public bool myTurn;
    public bool targetSelect;
    public bool selfTarget;
    public int abilitySelect;
    public int AP;
    public List<int> loot = new List<int>();
    public List<GameObject> enemies = new List<GameObject>();
    public bool vampire;
    bool vampiricTouch;
    public bool defend;
    public Text enemyHPtext;
    int dead;
    int toBeHealed;
    public int spikes;
    int baseAPCost;
    float STR;

    GameObject target;

    void Awake()
    {
        myTurn = true;
        instance = this;
        es1 = false;
        es2 = false;
        es3 = false;
        STR = PlayerManager.instance.STR;
        AP = PlayerManager.instance.AP;
        loot = PlayerManager.instance.equippedLoot;
        vampire = PlayerManager.instance.vampire;
        PlayerManager.instance.weak = false;
        PlayerManager.instance.confuse = false;
        PlayerManager.instance.doom = 0;
        
        baseAPCost = 1;
        playerAnim = GameObject.FindWithTag("Player").GetComponent<Animator>();
        if (MainManager.instance.difficulty == 1){
            GameObject.FindWithTag("0").SetActive(false);
            GameObject.FindWithTag("ehp0").SetActive(false);
            GameObject.FindWithTag("2").SetActive(false);
            GameObject.FindWithTag("ehp2").SetActive(false);
        }
        else if (MainManager.instance.difficulty == 2){
            GameObject.FindWithTag("2").SetActive(false);
            GameObject.FindWithTag("ehp2").SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindWithTag("0") != null)
        {
            enemies.Add(GameObject.FindWithTag("0"));
            enemy1Anim = GameObject.FindWithTag("0").GetComponentInChildren<Animator>();
        }
        if (GameObject.FindWithTag("1") != null)
        {
            enemies.Add(GameObject.FindWithTag("1"));
            enemy2Anim = GameObject.FindWithTag("1").GetComponentInChildren<Animator>();
        }

        if (GameObject.FindWithTag("2") != null)
        {
            enemies.Add(GameObject.FindWithTag("2"));
            enemy3Anim = GameObject.FindWithTag("2").GetComponentInChildren<Animator>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (myTurn)
        {
            if(playerAnim.GetCurrentAnimatorStateInfo(0).IsName("attack")){
                
            }
            else{
                if (targetSelect)
                {
                    Clicktarget();
                }
                if(selfTarget){
                    SelfTarget();
                }
                if(checkWin() == enemies.Count){

                    CombatUIController.instance.refreshUI();
                    CombatUIController.instance.win();
                }
            }
            if(AP < baseAPCost){
                if(PlayerManager.instance.weak){
                    handleWeak(PlayerManager.instance,false);
                }
                
                CombatUIController.instance.endTurn.style.display = DisplayStyle.Flex;
            }
            
            
            
        }
        else
        {
            
           /*  if(PlayerManager.instance.confuse){
                PlayerManager.instance.confuse = false;
                baseAPCost = 1;
            } */

            if(checkWin() == enemies.Count){

                CombatUIController.instance.refreshUI();
                CombatUIController.instance.win();
            }
            if(PlayerManager.instance.confuse){
                    handleConfuse(false);
                    PlayerManager.instance.confuse = false;
                }
            enemyTurn();
            if(PlayerManager.instance.HP <= 0){
                DeathAnimCall(playerAnim);
            }
            if(PlayerManager.instance.doom >= 10){
                DeathAnimCall(playerAnim);
            }
            myTurn = true;
            
            if(defend){
                defend = false;
            }
            
        }
    }
    public void AttackAnimCall(Animator anim){
        anim.SetTrigger("attack");              
    }
    public void DeathAnimCall(Animator anim){

        if(anim == playerAnim){
            anim.SetBool("playIsDead", true);
            AudioManager.instance.Play(12);
        }
        else{
            if(anim.GetBool("isDead") == false){
                Debug.Log("fire");
                AudioManager.instance.Play(8);
            }
           anim.SetBool("isDead",true); 
           
           
        }
        
    }
    public void HitAnimCall(Animator anim){
        if(anim == playerAnim){
            anim.SetTrigger("hitplay");
        }
        else{
           anim.SetTrigger("hit"); 
        }
        
    }
    public void handleWeak(PlayerManager p , bool onOff){
        p.weak = onOff;
        if(onOff){
            p.STR = p.STR/2;
        }
        else{
            p.STR = p.STR * 2;
        }
    }

    public int checkWin(){
        dead = 0;
        foreach(GameObject enemy in enemies){
            if(enemy.GetComponent<EnemyManager>().HP <= 0 ){
                if(enemy.tag == "0"){
                    DeathAnimCall(enemy1Anim);
                     
                }
                else if(enemy.tag == "1"){
                    DeathAnimCall(enemy2Anim);  
                }
                else{
                    DeathAnimCall(enemy3Anim);      
                }
                dead += 1;
                
                enemy.GetComponent<EnemyManager>().enabled = false;
                
                
                
               
            }
           
        }
        return dead;
    }

    public void SelfTarget(){
        
        
        if(Action(abilitySelect)){
           if(abilitySelect == 1 || abilitySelect == 3 || abilitySelect == 5 || abilitySelect == 6 || abilitySelect == 9 ){
                foreach(GameObject enemy in enemies){
                    if(enemy.GetComponent<EnemyManager>().enabled == true){
                        if(enemy.GetComponent<EnemyManager>().spikes > 0){
                            PlayerManager.instance.HP -= enemy.GetComponent<EnemyManager>().spikes;
                            enemy.GetComponent<EnemyManager>().spikes -= 2;
                        }
                        if(enemy.tag == "0"){
                        HitAnimCall(enemy1Anim);  
                        }
                        else if(enemy.tag == "1"){
                            HitAnimCall(enemy2Anim);
                        }
                        else{
                            HitAnimCall(enemy3Anim);
                        }
                    }
                }
                AudioManager.instance.Play(3);
            } 
            
        }
        selfTarget = false;
        CombatUIController.instance.refreshUI();
}
    public void Clicktarget()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, 512);
            if (hit.collider != null)
            {
                target = hit.transform.gameObject;
                targetSelect = false;
                if(Action(abilitySelect)){
                    
                    if(target.GetComponent<EnemyManager>().spikes > 0){
                        PlayerManager.instance.HP -= target.GetComponent<EnemyManager>().spikes;
                        target.GetComponent<EnemyManager>().spikes -= 2;
                    }
                    CombatUIController.instance.SelectText.SetActive(false);
                    CombatUIController.instance.refreshUI();
                    if(target.tag == "0"){
                        HitAnimCall(enemy1Anim);  
                    }
                    else if(target.tag == "1"){
                        HitAnimCall(enemy2Anim);
                    }
                    else{
                        HitAnimCall(enemy3Anim);
                    }    
                    AudioManager.instance.Play(3);
                }
                else{
                    
                }
            }
        }
    }
    public void handleEnemyWeak(EnemyManager e, bool onOff){
        e.weak = onOff;
        if(onOff){
            e.STR = e.STR/2;
        }
        else{
            e.STR = e.STR * 2;
        }
    }
    public void handleConfuse(bool onOff){
        if(onOff){
            baseAPCost = 2;
        }
        else{
            baseAPCost = 1;
        }
    }
    public void enemyTurn()
    {
        float[] DMG;
        foreach (GameObject enemy in enemies)
        {
            if (enemy.GetComponent<EnemyManager>().type == "grunt" && enemy.GetComponent<EnemyManager>().enabled == true)
            {
                DMG = enemy.GetComponent<EnemyManager>().Grunt();
                if(defend){
                    PlayerManager.instance.RES -= DMG[0];
                    enemy.GetComponent<EnemyManager>().HP -= spikes;
                    if(spikes > 0){
                        spikes -= 2;
                    }
                }
                else{
                    PlayerManager.instance.HP -= DMG[0];
                    enemy.GetComponent<EnemyManager>().HP -= spikes;
                    if(spikes > 0){
                        spikes -= 2;
                    }
                }
                if(enemy.GetComponent<EnemyManager>().weak){
                    handleEnemyWeak(enemy.GetComponent<EnemyManager>(),false);
                }
                if(enemy.tag == "0"){
                    AttackAnimCall(enemy1Anim);  
                }
                else if(enemy.tag == "1"){
                    AttackAnimCall(enemy2Anim);
                }
                else{
                    AttackAnimCall(enemy3Anim);
                }
                AudioManager.instance.Play(4);
                
                CombatUIController.instance.refreshUI();
                //HitAnimCall(playerAnim);
            }
            else if (enemy.GetComponent<EnemyManager>().type == "bruiser" && enemy.GetComponent<EnemyManager>().enabled == true)
            {
                DMG = enemy.GetComponent<EnemyManager>().Bruiser();
                if(defend){
                    PlayerManager.instance.RES -= DMG[0];
                    enemy.GetComponent<EnemyManager>().HP -= spikes;
                    if(spikes > 0){
                        spikes -= 2;
                    }
                }
                else{
                    PlayerManager.instance.HP -= DMG[0];  
                    enemy.GetComponent<EnemyManager>().HP -= spikes;
                    if(spikes > 0){
                        spikes -= 2;
                    }
                }
                if (DMG[1] == 1)
                {
                    handleWeak(PlayerManager.instance,true);
                }
                if(enemy.GetComponent<EnemyManager>().weak){
                    handleEnemyWeak(enemy.GetComponent<EnemyManager>(),false);
                }
                if(enemy.tag == "0"){
                    AttackAnimCall(enemy1Anim);  
                }
                else if(enemy.tag == "1"){
                    AttackAnimCall(enemy2Anim);
                }
                else{
                    AttackAnimCall(enemy3Anim);
                }
                AudioManager.instance.Play(5);
                CombatUIController.instance.refreshUI();
                //HitAnimCall(playerAnim);
            }
            else if(enemy.GetComponent<EnemyManager>().type == "wizard" && enemy.GetComponent<EnemyManager>().enabled == true)
            {
                DMG = enemy.GetComponent<EnemyManager>().Wizard();
                if(defend){
                    PlayerManager.instance.RES -= DMG[0];
                    if(DMG[1] == 3){
                        PlayerManager.instance.confuse = true;
                        handleConfuse(true);
                        AudioManager.instance.Play(10);
                    }
                    else{
                        AudioManager.instance.Play(6);
                    }
                    if(DMG[1] == 4){
                        PlayerManager.instance.doom += 2;
                    }
                    enemy.GetComponent<EnemyManager>().HP -= spikes;
                    if(spikes > 0){
                        spikes -= 2;
                    }
                }
                else{
                    PlayerManager.instance.HP -= DMG[0];
                    if(DMG[1] == 3){
                        PlayerManager.instance.confuse = true;
                        handleConfuse(true);
                        AudioManager.instance.Play(10);
                    }
                    else{
                        AudioManager.instance.Play(6);
                    }
                    if(DMG[1] == 4){
                        PlayerManager.instance.doom += 2;
                    }
                    enemy.GetComponent<EnemyManager>().HP -= spikes;
                    if(spikes > 0){
                        spikes -= 2;
                    }
                }
                if(enemy.GetComponent<EnemyManager>().weak){
                    handleEnemyWeak(enemy.GetComponent<EnemyManager>(),false);
                }
                if(enemy.tag == "0"){
                    AttackAnimCall(enemy1Anim);  
                }
                else if(enemy.tag == "1"){
                    AttackAnimCall(enemy2Anim);
                }
                else{
                    AttackAnimCall(enemy3Anim);
                }
                
                CombatUIController.instance.refreshUI();
                
            }
        }
        HitAnimCall(playerAnim);
    }

    public bool Action(int option)
    {

        //if weak was not applied last turn and you have it take it off
        //if it was put it on for your turn
        switch (option)
        {
            //cast strike
            case 1:
                if(PlayerManager.instance.hurricane){
                    if(AP >= baseAPCost){
                        foreach (GameObject enemy in enemies){
                            if(PlayerManager.instance.deathTouch){
                                if(enemy.GetComponent<EnemyManager>().enabled == true){
                                    enemy.GetComponent<EnemyManager>().HP = 0;
                                    
                                }
                            }
                            else{
                                if(enemy.GetComponent<EnemyManager>().enabled == true){
                                    enemy.GetComponent<EnemyManager>().HP -= 10 * PlayerManager.instance.STR;
                                }
                            }
                        
                        }
                        if(PlayerManager.instance.deathTouch){
                            PlayerManager.instance.deathTouch = false;
                        }
                        AP -= baseAPCost;
                        AttackAnimCall(playerAnim);
                        return true;
                    }
                    
                }
                else{
                   if (AP >= baseAPCost && target.GetComponent<EnemyManager>().enabled == true)
                    {
                        if(PlayerManager.instance.deathTouch){
                            target.GetComponent<EnemyManager>().HP = 0;
                            PlayerManager.instance.deathTouch = false;
                        }
                        else{
                            target.GetComponent<EnemyManager>().HP -= 10 * PlayerManager.instance.STR;
                        }
                        AP -= baseAPCost;
                        AttackAnimCall(playerAnim);
                        return true;
                        
                    }
                }
            return false;
            case 2:
                if(AP >= baseAPCost && !defend && PlayerManager.instance.RES > 0){

                    defend = true;
                    AP -= baseAPCost;
                    //cast defend
                    return true;
                }
                
                return false;
                
            case 3:
                if(AP >= baseAPCost && PlayerManager.instance.RES >= 2){
                    //cast sweeping strike
                    foreach (GameObject enemy in enemies){

                         if(PlayerManager.instance.deathTouch){
                                if(enemy.GetComponent<EnemyManager>().enabled == true){
                                    enemy.GetComponent<EnemyManager>().HP = 0;
                                    
                                }
                            }
                            else{
                                if(enemy.GetComponent<EnemyManager>().enabled == true){
                                    enemy.GetComponent<EnemyManager>().HP -= 5 * PlayerManager.instance.STR;
                                }
                            }
                    }
                    if(PlayerManager.instance.deathTouch){
                        PlayerManager.instance.deathTouch = false;
                    }
                    AP -= baseAPCost;
                    AttackAnimCall(playerAnim);
                    PlayerManager.instance.RES -= 2;
                    return true;
                }
                return false;
            case 4:
                //cast rest
                if(AP >= baseAPCost + 2){
                    PlayerManager.instance.RES += 10;
                    AP -= baseAPCost + 2;
                    return true;
                }
                return false;
                
            case 5:
                //cast vampiric strike
                if(PlayerManager.instance.hurricane){
                    if(AP >= baseAPCost ){
                        if(PlayerManager.instance.vampiricTouch){
                            toBeHealed = 15 * enemies.Count;
                        }
                        else{
                            toBeHealed = 5 * enemies.Count;
                        }
                        

                        int missingHP = (int)PlayerManager.instance.maxHP - (int)PlayerManager.instance.HP ;
                        if(toBeHealed > missingHP){
                            toBeHealed = missingHP;
                        }
                        

                       foreach (GameObject enemy in enemies){
                            if(PlayerManager.instance.deathTouch){
                                if(enemy.GetComponent<EnemyManager>().enabled == true){
                                    enemy.GetComponent<EnemyManager>().HP = 0;
                                    PlayerManager.instance.HP += toBeHealed;
                                }
                            }
                            else{
                                if(enemy.GetComponent<EnemyManager>().enabled == true){
                                    if(PlayerManager.instance.vampiricTouch){
                                        enemy.GetComponent<EnemyManager>().HP -= 20 * PlayerManager.instance.STR;
                                        PlayerManager.instance.HP += toBeHealed;
                                    }
                                    else{
                                        enemy.GetComponent<EnemyManager>().HP -= 10 * PlayerManager.instance.STR;
                                        PlayerManager.instance.HP += toBeHealed;
                                    }
                                }
                            }
                        
                        }
                        if(PlayerManager.instance.deathTouch){
                        PlayerManager.instance.deathTouch = false;
                        }
                        AttackAnimCall(playerAnim);
                        AP -= baseAPCost;
                        return  true;
                    }
                }

                else{
                    if(AP >= baseAPCost && target.GetComponent<EnemyManager>().enabled == true ){
                        if(PlayerManager.instance.vampiricTouch){
                            toBeHealed = 15;
                        }
                        else{
                            toBeHealed = 5;
                        }

                        int missingHP = (int)PlayerManager.instance.maxHP - (int)PlayerManager.instance.HP ;
                        if(toBeHealed > missingHP){
                            toBeHealed = missingHP;
                        }
                        PlayerManager.instance.HP += toBeHealed;

                        if(PlayerManager.instance.deathTouch){
                            target.GetComponent<EnemyManager>().HP = 0;
                            AP -= baseAPCost;
                            PlayerManager.instance.deathTouch = false;
                        }
                        else{
                            if(PlayerManager.instance.vampiricTouch){
                                target.GetComponent<EnemyManager>().HP -= 20 * PlayerManager.instance.STR;
                            }
                            else{
                                target.GetComponent<EnemyManager>().HP -= 10 * PlayerManager.instance.STR;
                            }
                            AP -= baseAPCost;
                        }
                        AttackAnimCall(playerAnim);
                        return true;
                    }
                    
                }
                return false;
            case 6:
                //cast Bolstering Strike
                if(PlayerManager.instance.hurricane){
                    if(AP >= baseAPCost){
                        toBeHealed = 5 * enemies.Count;

                        int missingHP = (int)PlayerManager.instance.maxRES - (int)PlayerManager.instance.RES ;
                        if(toBeHealed > missingHP){
                            toBeHealed = missingHP;
                        }
                        PlayerManager.instance.RES += toBeHealed;

                       foreach (GameObject enemy in enemies){
                            if(PlayerManager.instance.deathTouch){
                                if(enemy.GetComponent<EnemyManager>().enabled == true){
                                    enemy.GetComponent<EnemyManager>().HP = 0;
                                }
                            }
                            else{
                                if(enemy.GetComponent<EnemyManager>().enabled == true){
                                    enemy.GetComponent<EnemyManager>().HP -= 5 * PlayerManager.instance.STR;
                                }
                            }
                        
                        }
                        if(PlayerManager.instance.deathTouch){
                        PlayerManager.instance.deathTouch = false;
                        }
                        AttackAnimCall(playerAnim);
                        AP -= baseAPCost;
                        return true;
                    }
                }

                else{
                    if(AP >= baseAPCost && target.GetComponent<EnemyManager>().enabled == true ){
                        toBeHealed = 5;

                        int missingHP = (int)PlayerManager.instance.maxRES - (int)PlayerManager.instance.RES ;
                        if(toBeHealed > missingHP){
                            toBeHealed = missingHP;
                        }
                        PlayerManager.instance.RES += toBeHealed;

                        if(PlayerManager.instance.deathTouch){
                            target.GetComponent<EnemyManager>().HP = 0;
                            AP -= baseAPCost;
                            PlayerManager.instance.deathTouch = false;
                        }
                        else{
                            target.GetComponent<EnemyManager>().HP -= 5 * PlayerManager.instance.STR;
                            AP -= baseAPCost;
                        }
                        AttackAnimCall(playerAnim);
                        return true;
                    }
                }
                
                return false;
            case 7:
                //cast Spike Defense
                if(AP >= baseAPCost && spikes != 10 && PlayerManager.instance.RES > 0){
                    defend = true;
                    spikes = 10;
                    AP -= baseAPCost;
                    AudioManager.instance.Play(13);
                    return true;
                }
                return false;
            case 8:
                //cast Hibernate
                
                if(AP >= baseAPCost + 2){
                    toBeHealed = 25;
                    int missingHP = (int)PlayerManager.instance.maxRES - (int)PlayerManager.instance.RES ;
                    if(toBeHealed > missingHP){
                        toBeHealed = missingHP;
                    }
                    PlayerManager.instance.RES += toBeHealed;
                    AP -= baseAPCost + 2;
                    return true;
                }
                return false;
            case 9:
                //cast Weak Strike
                 if(PlayerManager.instance.hurricane){
                    if(AP >= baseAPCost){
                        foreach (GameObject enemy in enemies){
                            if(PlayerManager.instance.deathTouch){
                                if(enemy.GetComponent<EnemyManager>().enabled == true){
                                    enemy.GetComponent<EnemyManager>().HP = 0;
                                }
                            }
                            else{
                                if(enemy.GetComponent<EnemyManager>().enabled == true){
                                    enemy.GetComponent<EnemyManager>().HP -= 10 * PlayerManager.instance.STR;
                                    handleEnemyWeak(enemy.GetComponent<EnemyManager>(),true);
                
                                }
                            }
                        
                        }
                        if(PlayerManager.instance.deathTouch){
                        PlayerManager.instance.deathTouch = false;
                        }
                        AP -= baseAPCost;
                        AttackAnimCall(playerAnim);
                        return true;
                    }
                }
                else{
                   if (AP >= baseAPCost && target.GetComponent<EnemyManager>().enabled == true)
                    {
                        if(PlayerManager.instance.deathTouch){
                            target.GetComponent<EnemyManager>().HP = 0;
                            AP -= baseAPCost;
                            PlayerManager.instance.deathTouch = false;
                        }
                        else{
                            target.GetComponent<EnemyManager>().HP -= 10 * PlayerManager.instance.STR;
                            if(target.GetComponent<EnemyManager>().weak == false){
                                handleEnemyWeak(target.GetComponent<EnemyManager>(),true);
                            }
                            
                            AP -= baseAPCost;
                        }

                        AttackAnimCall(playerAnim);
                        return true;
                        
                    } 
                }
                return false;
            case 10:
                //cast Death Touch
                if(AP >= baseAPCost + 2){
                    PlayerManager.instance.deathTouch = true;
                    AP -= baseAPCost + 2;
                    return true;
                }
               return false;

        }
        return false;
    }
}
