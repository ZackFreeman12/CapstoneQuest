using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using System;

public class BossCombatUI : MonoBehaviour
{
    public static BossCombatUI instance;
    public Button attackBtn;
    public VisualElement attackMenu;
    public VisualElement items;
    public Button itemb;
    public Button ib1;
    public Button abandon;
    public Button ab1;
    public Button ab2;
    public Button ab3;
    public Button ab4;
    public Label playHP;
    public Label playRES;
    public Label playAP;
    public Label playDoom;
    public Label winAB;
    public Label playWeak;
    public Label playConfuse;
    public Label playSpikes;
    public Label eWeak1;
    public Label eWeak2;
    public Label eWeak3;
    public Label eSpikes1;
    public Label eSpikes2;
    public Label eSpikes3;
    public Label bossWeak;
    public Button endTurn;
    public Button backToMain;
    public VisualElement winScreenFinal;
    public VisualElement deathScreen;
    public GameObject SelectText;

    public VisualElement winScreen;

    public Button nextFloor;
    public Button backToMain2;

    // Start is called before the first frame update
    void Start()
    {
        
        instance = this;
        var root = GetComponent<UIDocument>().rootVisualElement;

        attackBtn = root.Q<Button>("attackBtn");
        attackMenu = root.Q<VisualElement>("attackMenu");
        winScreenFinal = root.Q<VisualElement>("winScreenFinal");
        ab1 = root.Q<Button>("ab1");
        ib1 = root.Q<Button>("ib1");
        itemb = root.Q<Button>("itemb");
        abandon = root.Q<Button>("abandon");
        ab2 = root.Q<Button>("ab2");
        ab3 = root.Q<Button>("ab3");
        ab4 = root.Q<Button>("ab4");
        backToMain2 = root.Q<Button>("backToMain2");
        playHP = root.Q<Label>("playHP");
        playAP = root.Q<Label>("playAP");
        playRES = root.Q<Label>("playRES");
        winAB = root.Q<Label>("winAB");
        playWeak = root.Q<Label>("playWeak");
        playConfuse = root.Q<Label>("playConfuse");
        playSpikes = root.Q<Label>("playSpikes");
        playDoom = root.Q<Label>("playDoom");
        bossWeak = root.Q<Label>("bossWeak");
        eWeak1 = root.Q<Label>("eWeak1");
        eWeak2 = root.Q<Label>("eWeak2");
        eWeak3 = root.Q<Label>("eWeak3");
        eSpikes3 = root.Q<Label>("eSpikes3");
        eSpikes1 = root.Q<Label>("eSpikes1");
        eSpikes2 = root.Q<Label>("eSpikes2");
        endTurn = root.Q<Button>("endTurn");
        backToMain = root.Q<Button>("backToMain");
        deathScreen = root.Q<VisualElement>("deathScreen");
        items = root.Q<VisualElement>("items");
        nextFloor = root.Q<Button>("nextFloor");
        winScreen = root.Q<VisualElement>("winScreen");

        attackBtn.clicked += attackBtnPressed;
        ab1.clicked += ab1Pressed;
        ab2.clicked += ab2Pressed;
        ab3.clicked += ab3Pressed;
        ab4.clicked += ab4Pressed;
        endTurn.clicked += endTurnPressed;
        backToMain.clicked += backToMainPressed;
        nextFloor.clicked += nextFloorPressed;
        itemb.clicked += itembPressed;
        ib1.clicked += ib1Pressed;
        abandon.clicked += abandonPressed;
        backToMain2.clicked += backToMain2Pressed;

        SelectText = GameObject.FindWithTag("9");
        SelectText.SetActive(false);
        refreshUI();
        eWeak1.style.display = DisplayStyle.None;
        eWeak2.style.display = DisplayStyle.None;
        eWeak3.style.display = DisplayStyle.None;
        eSpikes1.style.display = DisplayStyle.None;
        eSpikes2.style.display = DisplayStyle.None;
        eSpikes3.style.display = DisplayStyle.None;
        playWeak.style.display = DisplayStyle.None;
        playConfuse.style.display = DisplayStyle.None;
        playSpikes.style.display = DisplayStyle.None;
        bossWeak.style.display = DisplayStyle.None;

        ab1.text = AbilityManager.instance.AbilityToString(PlayerManager.instance.equippedAbilities[0])[0];
        ab2.text = AbilityManager.instance.AbilityToString(PlayerManager.instance.equippedAbilities[1])[0];
        ab3.text = AbilityManager.instance.AbilityToString(PlayerManager.instance.equippedAbilities[2])[0];
        ab4.text = AbilityManager.instance.AbilityToString(PlayerManager.instance.equippedAbilities[3])[0];
    }

    public void refreshUI()
    {
        
         if(GameObject.FindWithTag("ehp1") != null && GameObject.FindWithTag("1") != null  ){
            
            if(GameObject.FindWithTag("1").GetComponent<EnemyManager>().weak){
                eWeak1.style.display = DisplayStyle.Flex;
                Debug.Log("Weak");
            }
            else{
                eWeak1.style.display = DisplayStyle.None; 
                Debug.Log("unweak");
            }
            if(GameObject
            .FindWithTag("1")
            .GetComponent<EnemyManager>().spikes > 0){
                eSpikes1.style.display = DisplayStyle.Flex;
                eSpikes1.text = "Spikes: " + GameObject.FindWithTag("1").GetComponent<EnemyManager>().spikes;
            }
            else{
                eSpikes1.style.display = DisplayStyle.None; 
            }
            GameObject
            .FindWithTag("1")
            .GetComponent<EnemyManager>()
            .HP = (float)Math.Round((Decimal)GameObject
            .FindWithTag("1")
            .GetComponent<EnemyManager>()
            .HP);
            GameObject.FindWithTag("ehp1").GetComponent<TextMesh>().text = GameObject
            .FindWithTag("1")
            .GetComponent<EnemyManager>()
            .HP.ToString();

            if(GameObject.FindWithTag("1").GetComponent<EnemyManager>().HP <= 0){
                GameObject.FindWithTag("ehp1").GetComponent<TextMesh>().text = "0";
            }
        }
        if(GameObject.FindWithTag("ehp0") != null && GameObject.FindWithTag("0") != null  ){
            if(GameObject.FindWithTag("0").GetComponent<EnemyManager>().boss){
                if(GameObject
            .FindWithTag("0")
            .GetComponent<EnemyManager>().weak){
                bossWeak.style.display = DisplayStyle.Flex;
                Debug.Log("Weak");
            }
            else{
                bossWeak.style.display = DisplayStyle.None; 
                Debug.Log("unweak");
                }
            }
            else{
                if(GameObject
            .FindWithTag("0")
            .GetComponent<EnemyManager>().weak){
                eWeak1.style.display = DisplayStyle.Flex;
                Debug.Log("Weak");
            }
            else{
            eWeak1.style.display = DisplayStyle.None; 
            Debug.Log("unweak");
            }
            }
             
             
            if(GameObject
            .FindWithTag("0")
            .GetComponent<EnemyManager>().spikes > 0){
                eSpikes1.style.display = DisplayStyle.Flex;
                eSpikes1.text = "Spikes: " + GameObject.FindWithTag("0").GetComponent<EnemyManager>().spikes;
            }
            else{
                eSpikes1.style.display = DisplayStyle.None; 
            }
            GameObject
            .FindWithTag("0")
            .GetComponent<EnemyManager>()
            .HP = (float)Math.Round((Decimal)GameObject
            .FindWithTag("0")
            .GetComponent<EnemyManager>()
            .HP);
            GameObject.FindWithTag("ehp0").GetComponent<TextMesh>().text = GameObject
            .FindWithTag("0")
            .GetComponent<EnemyManager>()
            .HP.ToString();

            if(GameObject.FindWithTag("0").GetComponent<EnemyManager>().HP <= 0){
                GameObject.FindWithTag("ehp0").GetComponent<TextMesh>().text = "0";
            }
        }
        
       
        if(GameObject.FindWithTag("ehp2") != null && GameObject.FindWithTag("2") != null  ){
            if(GameObject
            .FindWithTag("2")
            .GetComponent<EnemyManager>().weak){
                eWeak2.style.display = DisplayStyle.Flex;
            }
            else{
                eWeak2.style.display = DisplayStyle.None; 
            }
            if(GameObject
            .FindWithTag("2")
            .GetComponent<EnemyManager>().spikes > 0){
                eSpikes2.style.display = DisplayStyle.Flex;
                eSpikes2.text = "Spikes: " + GameObject.FindWithTag("2").GetComponent<EnemyManager>().spikes;
            }
            else{
                eSpikes2.style.display = DisplayStyle.None; 
            }
            GameObject
            .FindWithTag("2")
            .GetComponent<EnemyManager>()
            .HP = (float)Math.Round((Decimal)GameObject
            .FindWithTag("2")
            .GetComponent<EnemyManager>()
            .HP);
            GameObject.FindWithTag("ehp2").GetComponent<TextMesh>().text = GameObject
            .FindWithTag("2")
            .GetComponent<EnemyManager>()
            .HP.ToString();

            if(GameObject.FindWithTag("2").GetComponent<EnemyManager>().HP <= 0){
                GameObject.FindWithTag("ehp2").GetComponent<TextMesh>().text = "0";
            }
        }
        
        
        
        PlayerManager.instance.HP = (float)Math.Round((Decimal)PlayerManager.instance.HP);
        playHP.text = PlayerManager.instance.HP.ToString();
        playRES.text = PlayerManager.instance.RES.ToString();
        playAP.text = BossCombatManager.instance.AP.ToString() + "/3";

        if(PlayerManager.instance.weak){
            playWeak.style.display = DisplayStyle.Flex;
        }
        else{
           playWeak.style.display = DisplayStyle.None; 
        }
        if(PlayerManager.instance.confuse){
            playConfuse.style.display = DisplayStyle.Flex;
        }
        else{
           playConfuse.style.display = DisplayStyle.None; 
        }
        if(BossCombatManager.instance.spikes > 10){
            playSpikes.style.display = DisplayStyle.Flex;
            playSpikes.text = "Spikes: " + BossCombatManager.instance.spikes;
        }
        else{
           playSpikes.style.display = DisplayStyle.None; 
        }
        if(PlayerManager.instance.doom > 0){
            playDoom.style.display = DisplayStyle.Flex;
            playDoom.text = "Doom:  " + PlayerManager.instance.doom;
        }
        else{
            playDoom.style.display = DisplayStyle.None;
        }
        if (BossCombatManager.instance.AP == 0)
        {
           
            endTurn.style.display = DisplayStyle.Flex;
            ab2.SetEnabled(true);
        }
        if(PlayerManager.instance.HP <= 0){
            playHP.text = "0";
            deathScreen.style.display = DisplayStyle.Flex;
            BossCombatManager.instance.gameObject.SetActive(false);
        }
        if(PlayerManager.instance.RES <= 0){
            playRES.text = "0";
            
        }
        if(PlayerManager.instance.doom >= 10){
            playHP.text = "0";
            deathScreen.style.display = DisplayStyle.Flex;
            BossCombatManager.instance.gameObject.SetActive(false);
        }
    }

    public void win(){
        winScreenFinal.style.display = DisplayStyle.Flex;
        PlayerManager.instance.GP += 50;
        var winABInt = UnityEngine.Random.Range(1,8);
        string[] nameDesc = LootManager.instance.LootToString(winABInt);
        winAB.text = nameDesc[0];
        PlayerManager.instance.newItem(winABInt);
        AudioManager.instance.Play(9);
        AudioManager.instance.sounds[2].Stop();
        AudioManager.instance.sounds[11].Stop();
        BossCombatManager.instance.gameObject.SetActive(false);
    }

    void backToMain2Pressed(){
        AudioManager.instance.Play(0);
        AudioManager.instance.sounds[9].Stop();
        SceneManager.LoadScene("MainMenu");
        Destroy(GameObject.FindWithTag("12"));
         foreach (GameObject icon in MainManager.instance.combatIcons)
            {

                Destroy(icon);

            }
        foreach (GameObject icon in MainManager.instance.icons)
            {

                Destroy(icon);

            }
    }
    void abandonPressed (){

        playHP.text = "0";
        deathScreen.style.display = DisplayStyle.Flex;
        BossCombatManager.instance.gameObject.SetActive(false);
        AudioManager.instance.Play(0);
        AudioManager.instance.sounds[9].Stop();
        AudioManager.instance.sounds[11].Stop();
        

    }

    void nextFloorPressed(){
        
        SceneManager.LoadScene("Map");
        AudioManager.instance.Play(0);
        AudioManager.instance.Play(7);
        AudioManager.instance.Play(2);
        AudioManager.instance.sounds[9].Stop();
    }
    void backToMainPressed(){
        AudioManager.instance.Play(0);
        AudioManager.instance.sounds[11].Stop();
        SceneManager.LoadScene("MainMenu");
        Destroy(GameObject.FindWithTag("12"));
         foreach (GameObject icon in MainManager.instance.combatIcons)
            {

                Destroy(icon);

            }
        foreach (GameObject icon in MainManager.instance.icons)
            {

                Destroy(icon);

            }
    }

    void attackBtnPressed()
    {
        AudioManager.instance.Play(0);
        attackMenu.style.display = DisplayStyle.Flex;
        items.style.display = DisplayStyle.None;
    }

   
    void itembPressed(){
        items.style.display = DisplayStyle.Flex;
        attackMenu.style.display = DisplayStyle.None;
        AudioManager.instance.Play(0);
    }
    void ib1Pressed(){
        if(PlayerManager.instance.potion){
            var toBeHealed = 50;
            PlayerManager.instance.maxHP += 25;
            int missingHP = (int)PlayerManager.instance.maxHP - (int)PlayerManager.instance.HP ;
            if(toBeHealed > missingHP){
                toBeHealed = missingHP;
            }
            PlayerManager.instance.HP += toBeHealed;
            ib1.style.display = DisplayStyle.None;
            AudioManager.instance.Play(0);
            refreshUI();
            
                
        }
    }
    void ab1Pressed()
    {
         if(BossCombatManager.instance.playerAnim.GetCurrentAnimatorStateInfo(0).IsName("attack")){
                
        }
        else{
            if (BossCombatManager.instance.targetSelect != true || BossCombatManager.instance.targetSelect != true)
            {
                if( PlayerManager.instance.hurricane || AbilityManager.instance.AbilityToString(PlayerManager.instance.equippedAbilities[0])[2].Equals("self")){
                    BossCombatManager.instance.selfTarget = true;
                }

                else if(AbilityManager.instance.AbilityToString(PlayerManager.instance.equippedAbilities[0])[2].Equals("target")){
                    
                        BossCombatManager.instance.targetSelect = true; 
                        SelectText.SetActive(true);
                    
                }
        
            
            
            }
            BossCombatManager.instance.abilitySelect = PlayerManager.instance.equippedAbilities[0];
            AudioManager.instance.Play(0);
        }
        
    }

    void ab2Pressed() {  
         if(BossCombatManager.instance.playerAnim.GetCurrentAnimatorStateInfo(0).IsName("attack")){
                
        }
        else{
            if (BossCombatManager.instance.targetSelect != true || BossCombatManager.instance.targetSelect != true)
            {
                if( PlayerManager.instance.hurricane || AbilityManager.instance.AbilityToString(PlayerManager.instance.equippedAbilities[1])[2].Equals("self")){
                    BossCombatManager.instance.selfTarget = true;
                }

                else if(AbilityManager.instance.AbilityToString(PlayerManager.instance.equippedAbilities[1])[2].Equals("target")){
                    
                        BossCombatManager.instance.targetSelect = true; 
                        SelectText.SetActive(true);
                    
                }
        
            
            
            }
            BossCombatManager.instance.abilitySelect = PlayerManager.instance.equippedAbilities[1];
            AudioManager.instance.Play(0);
        }
        
        
        
        }

    void ab3Pressed() {
        if(BossCombatManager.instance.playerAnim.GetCurrentAnimatorStateInfo(0).IsName("attack")){
                
        }
        else{
           if (BossCombatManager.instance.targetSelect != true || BossCombatManager.instance.targetSelect != true){
                if( PlayerManager.instance.hurricane || AbilityManager.instance.AbilityToString(PlayerManager.instance.equippedAbilities[2])[2].Equals("self")){
                    BossCombatManager.instance.selfTarget = true;
                }

                else if(AbilityManager.instance.AbilityToString(PlayerManager.instance.equippedAbilities[2])[2].Equals("target")){
                
                    BossCombatManager.instance.targetSelect = true; 
                    SelectText.SetActive(true);
                
                }    
            }   
        
        
            
            BossCombatManager.instance.abilitySelect = PlayerManager.instance.equippedAbilities[2];
            AudioManager.instance.Play(0);
        }
        

     }

    void ab4Pressed() {
         if(BossCombatManager.instance.playerAnim.GetCurrentAnimatorStateInfo(0).IsName("attack")){
                
        }
        else{
            if (BossCombatManager.instance.targetSelect != true || BossCombatManager.instance.targetSelect != true)
            {
                if( PlayerManager.instance.hurricane || AbilityManager.instance.AbilityToString(PlayerManager.instance.equippedAbilities[3])[2].Equals("self")){
                    BossCombatManager.instance.selfTarget = true;
                }

                else if(AbilityManager.instance.AbilityToString(PlayerManager.instance.equippedAbilities[3])[2].Equals("target")){
                
                    BossCombatManager.instance.targetSelect = true; 
                    SelectText.SetActive(true);
                
                }
        
            
            
            }
            BossCombatManager.instance.abilitySelect = PlayerManager.instance.equippedAbilities[3];
            AudioManager.instance.Play(0);
        }

       

     }

    void endTurnPressed()
    {
         if(BossCombatManager.instance.targetSelect == false &&  BossCombatManager.instance.selfTarget == false){
            BossCombatManager.instance.myTurn = false;
            endTurn.style.display = DisplayStyle.None;
            BossCombatManager.instance.AP = 3;
         }
         AudioManager.instance.Play(0);
        
    }
}

