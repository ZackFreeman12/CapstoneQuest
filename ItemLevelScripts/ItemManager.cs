using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class ItemManager : MonoBehaviour
{
    public VisualElement tomeMenu;
    public VisualElement lootMenu;
    public VisualElement changeAbilityMenu;
    public CircleCollider2D cc;
    public Label ll1;
    public Label ll2;
    public Label ll3;
    public Label lootl1;
    public Label lootl2;
    public Label lootl3;
    public Label al1;
    public Label al2;
    public Label al3;
    public Label al4;

    public Button lb1;
    public Button lb2;
    public Button lb3;
    public Button lootb1;
    public Button lootb2;
    public Button lootb3;
    public Button nextFloor;
    public Button ab1;
    public Button ab2;
    public Button ab3;
    public Button ab4;
    public Button aab1;
    public Button aab2;
    public Button aab3;
    public Button aab4;
    public Button aab5;
    public Button aab6;
    public Button aab7;
    public Button aab8;
    public Button aab9;
    public Button aab10;
    public Button close;
    public int itemType; // 0 for an ability tome 1 for a passive effect item
    int choice1;
    string[] nameDesc1;
     int choice2;
    string[] nameDesc2;
     int choice3;
    string[] nameDesc3;
    int abilityIndex;
    public List<Button> availableAbilitiesButtons = new List<Button>();
    bool changeState;
    public int abilityToBeChanged;
    public int abilityToReplace;

    void Awake()
    {
        itemType = Random.Range(0, 2); 
        //itemType = 1;  

        var root = GameObject.Find("UIDocument").GetComponent<UIDocument>().rootVisualElement;

        tomeMenu = root.Q<VisualElement>("tomeMenu");
        lootMenu = root.Q<VisualElement>("lootMenu");
        changeAbilityMenu = root.Q<VisualElement>("changeAbilityMenu");

        lb1 = root.Q<Button>("lb1");
        lb2 = root.Q<Button>("lb2");
        lb3 = root.Q<Button>("lb3");
        lootb1 = root.Q<Button>("lootb1");
        lootb2 = root.Q<Button>("lootb2");
        lootb3 = root.Q<Button>("lootb3");
        ab1 = root.Q<Button>("ab1");
        ab2 = root.Q<Button>("ab2");
        ab3 = root.Q<Button>("ab3");
        ab4 = root.Q<Button>("ab4");
        aab1 = root.Q<Button>("aab1");
        aab2 = root.Q<Button>("aab2");
        aab3 = root.Q<Button>("aab3");
        aab4 = root.Q<Button>("aab4");
        aab5 = root.Q<Button>("aab5");
        aab6 = root.Q<Button>("aab6");
        aab10 = root.Q<Button>("aab10");
        aab7 = root.Q<Button>("aab7");
        aab8 = root.Q<Button>("aab8");
        aab9 = root.Q<Button>("aab9");
        close = root.Q<Button>("close");
        nextFloor = root.Q<Button>("nextFloor");

        availableAbilitiesButtons.Add(aab1);
        availableAbilitiesButtons.Add(aab2);
        availableAbilitiesButtons.Add(aab3);
        availableAbilitiesButtons.Add(aab4);
        availableAbilitiesButtons.Add(aab5);
        availableAbilitiesButtons.Add(aab6);
        availableAbilitiesButtons.Add(aab7);
        availableAbilitiesButtons.Add(aab8);
        availableAbilitiesButtons.Add(aab9);
        availableAbilitiesButtons.Add(aab10);

        ll1 = root.Q<Label>("ll1");
        ll2 = root.Q<Label>("ll2");
        ll3 = root.Q<Label>("ll3");
        lootl1 = root.Q<Label>("lootl1");
        lootl2 = root.Q<Label>("lootl2");
        lootl3 = root.Q<Label>("lootl3");
        al1 = root.Q<Label>("al1");
        al2 = root.Q<Label>("al2");
        al3 = root.Q<Label>("al3");
        al4 = root.Q<Label>("al4");

        

        lb1.clicked += onlb1Pressed;
        lb2.clicked += onlb2Pressed;
        lb3.clicked += onlb3Pressed;
        lootb1.clicked += onlootb1Pressed;
        lootb2.clicked += onlootb2Pressed;
        lootb3.clicked += onlootb3Pressed;
        ab1.clicked += onab1Pressed;
        ab2.clicked += onab2Pressed;
        ab3.clicked += onab3Pressed;
        ab4.clicked += onab4Pressed;
        aab1.clicked += onaab1Pressed;
        aab2.clicked += onaab2Pressed;
        aab3.clicked += onaab3Pressed;
        aab4.clicked += onaab4Pressed;
        aab5.clicked += onaab5Pressed;
        aab6.clicked += onaab6Pressed;
        aab7.clicked += onaab7Pressed;
        aab8.clicked += onaab8Pressed;
        aab9.clicked += onaab9Pressed;
        aab10.clicked += onaab10Pressed;
        close.clicked += onclosePressed;
        
        nextFloor.clicked += onnextFloorPressed;

        

    }

    void Update(){
        
    }
    void onclosePressed(){
        changeAbilityMenu.style.display = DisplayStyle.None;
    }
    void RefreshUI(){
        if(itemType == 0){
            al1.text = AbilityManager.instance.AbilityToString(PlayerManager.instance.equippedAbilities[0])[0];
            al2.text = AbilityManager.instance.AbilityToString(PlayerManager.instance.equippedAbilities[1])[0];
            al3.text = AbilityManager.instance.AbilityToString(PlayerManager.instance.equippedAbilities[2])[0];
            al4.text = AbilityManager.instance.AbilityToString(PlayerManager.instance.equippedAbilities[3])[0];
            var count = 0;
            foreach(Button button in availableAbilitiesButtons){
                if(!PlayerManager.instance.availableAbilities.Contains(AbilityManager.instance.allAbilities[count])){
                    Debug.Log("fire");
                    button.style.display = DisplayStyle.None;   
                }
                else{
                button.style.display = DisplayStyle.Flex;    
                }
                button.text = AbilityManager.instance.AbilityToString(AbilityManager.instance.allAbilities[count])[0];
                count ++;
            }
        }
        else{

        }
        
    }
    void  OnMouseUp()
    {
        if (itemType == 0){

            choice1 = Random.Range(5,11);
            nameDesc1 = AbilityManager.instance.AbilityToString(choice1);
            choice2 = Random.Range(5,11);
            nameDesc2 = AbilityManager.instance.AbilityToString(choice2);
            choice3 = Random.Range(5,11);
            nameDesc3 = AbilityManager.instance.AbilityToString(choice3);

            ll1.text = nameDesc1[0];
            ll2.text = nameDesc2[0];
            ll3.text = nameDesc3[0];

            tomeMenu.style.display = DisplayStyle.Flex;
            cc.enabled = false;
        }
        if (itemType == 1){

            choice1 = Random.Range(1,8);
            nameDesc1 = LootManager.instance.LootToString(choice1);
            choice2 = Random.Range(1,8);
            nameDesc2 = LootManager.instance.LootToString(choice2);
            choice3 = Random.Range(1,8);
            nameDesc3 = LootManager.instance.LootToString(choice3);

            lootl1.text = nameDesc1[0];
            lootl2.text = nameDesc2[0];
            lootl3.text = nameDesc3[0];

            lootMenu.style.display = DisplayStyle.Flex;
            cc.enabled = false;
        }
        AudioManager.instance.Play(0);
    }

    void onnextFloorPressed(){
        SceneManager.LoadScene("Map");
        AudioManager.instance.Play(0);
    }
    void handleItemAdd(int ID){
        PlayerManager.instance.newItem(ID);
        AudioManager.instance.Play(0);
    }
    void onlootb1Pressed(){
        handleItemAdd(choice1);
        lootMenu.style.display = DisplayStyle.None;
        nextFloor.style.display = DisplayStyle.Flex;
        AudioManager.instance.Play(0);
    }
    void onlootb2Pressed(){
        handleItemAdd(choice2);
        lootMenu.style.display = DisplayStyle.None;
        nextFloor.style.display = DisplayStyle.Flex;
        AudioManager.instance.Play(0);
    }
    void onlootb3Pressed(){
        handleItemAdd(choice3);
        lootMenu.style.display = DisplayStyle.None;
        nextFloor.style.display = DisplayStyle.Flex;
        AudioManager.instance.Play(0);
    }
    void onlb1Pressed(){
        if(PlayerManager.instance.availableAbilities.Contains(choice1)){

        }
        else{
            PlayerManager.instance.availableAbilities.Add(choice1);
            tomeMenu.style.display = DisplayStyle.None;
            nextFloor.style.display = DisplayStyle.Flex;
            changeAbilityMenu.style.display = DisplayStyle.Flex;
            RefreshUI();
            if(choice1 == 5){
                if(PlayerManager.instance.vampiricTouch){
                    PlayerManager.instance.vampire = true;
                } 
            }
        
        }
        AudioManager.instance.Play(0);
    }
    void onlb2Pressed(){
        if(PlayerManager.instance.availableAbilities.Contains(choice2)){

        }
        else{
            PlayerManager.instance.availableAbilities.Add(choice2);
            tomeMenu.style.display = DisplayStyle.None;
            nextFloor.style.display = DisplayStyle.Flex;
            changeAbilityMenu.style.display = DisplayStyle.Flex;
            RefreshUI();
            if(choice2 == 5){
                if(PlayerManager.instance.vampiricTouch){
                    PlayerManager.instance.vampire = true;
                } 
            }
        
        }
        AudioManager.instance.Play(0);
    }
    void onlb3Pressed(){
        if(PlayerManager.instance.availableAbilities.Contains(choice3)){

        }
        else{
            PlayerManager.instance.availableAbilities.Add(choice3);
            tomeMenu.style.display = DisplayStyle.None;
            nextFloor.style.display = DisplayStyle.Flex;
            changeAbilityMenu.style.display = DisplayStyle.Flex;
            RefreshUI();
            if(choice3 == 5){
                if(PlayerManager.instance.vampiricTouch){
                    PlayerManager.instance.vampire = true;
                } 
            }
        }
        AudioManager.instance.Play(0);
    }

    void onab1Pressed(){
        abilityIndex = 1;
        ab1.style.display = DisplayStyle.None;
        ab2.style.display = DisplayStyle.None;
        ab3.style.display = DisplayStyle.None;
        ab4.style.display = DisplayStyle.None;
        
        changeState = true;
        abilityToBeChanged = 0;
        AudioManager.instance.Play(0);
    }
    void onab2Pressed(){
        abilityIndex = 1;
        ab1.style.display = DisplayStyle.None;
        ab2.style.display = DisplayStyle.None;
        ab3.style.display = DisplayStyle.None;
        ab4.style.display = DisplayStyle.None;
        
        changeState = true;
        abilityToBeChanged = 1;
        AudioManager.instance.Play(0);
    }
    void onab3Pressed(){
        abilityIndex = 1;
        ab1.style.display = DisplayStyle.None;
        ab2.style.display = DisplayStyle.None;
        ab3.style.display = DisplayStyle.None;
        ab4.style.display = DisplayStyle.None;
        
        changeState = true;
        abilityToBeChanged = 2;
        AudioManager.instance.Play(0);
    }
    void onab4Pressed(){
        abilityIndex = 1;
        ab1.style.display = DisplayStyle.None;
        ab2.style.display = DisplayStyle.None;
        ab3.style.display = DisplayStyle.None;
        ab4.style.display = DisplayStyle.None;
        
        changeState = true;
        abilityToBeChanged = 3;
        AudioManager.instance.Play(0);
    }
    void handleSwap(){
        if(changeState){
            
            
            PlayerManager.instance.equippedAbilities.Insert(abilityToBeChanged,PlayerManager.instance.availableAbilities[abilityToReplace]);
            PlayerManager.instance.availableAbilities.Insert(abilityToReplace,PlayerManager.instance.equippedAbilities[abilityToBeChanged + 1]);
            PlayerManager.instance.equippedAbilities.RemoveAt(abilityToBeChanged + 1);
            PlayerManager.instance.availableAbilities.RemoveAt(abilityToReplace + 1);
            changeState = false;
            RefreshUI();
            close.style.display = DisplayStyle.Flex;
        }
        AudioManager.instance.Play(0);
    }
void onaab1Pressed(){
        abilityToReplace = PlayerManager.instance.availableAbilities.IndexOf(AbilityManager.instance.allAbilities[0]);

        handleSwap();
        AudioManager.instance.Play(0);
    }
    void onaab2Pressed(){
        abilityToReplace = PlayerManager.instance.availableAbilities.IndexOf(AbilityManager.instance.allAbilities[1]);
        handleSwap();
        AudioManager.instance.Play(0);
    }
    void onaab3Pressed(){
abilityToReplace = PlayerManager.instance.availableAbilities.IndexOf(AbilityManager.instance.allAbilities[2]);        handleSwap();
AudioManager.instance.Play(0);
    }
    void onaab4Pressed(){
abilityToReplace = PlayerManager.instance.availableAbilities.IndexOf(AbilityManager.instance.allAbilities[3]);        handleSwap();
    }
    void onaab5Pressed(){
abilityToReplace = PlayerManager.instance.availableAbilities.IndexOf(AbilityManager.instance.allAbilities[4]);        handleSwap();
    AudioManager.instance.Play(0);}
    void onaab6Pressed(){
abilityToReplace = PlayerManager.instance.availableAbilities.IndexOf(AbilityManager.instance.allAbilities[5]);        handleSwap();
    AudioManager.instance.Play(0);}
    void onaab7Pressed(){
abilityToReplace = PlayerManager.instance.availableAbilities.IndexOf(AbilityManager.instance.allAbilities[6]);        handleSwap();
    AudioManager.instance.Play(0);}
    void onaab8Pressed(){
abilityToReplace = PlayerManager.instance.availableAbilities.IndexOf(AbilityManager.instance.allAbilities[7]);        handleSwap();
    AudioManager.instance.Play(0);}
    void onaab9Pressed(){
abilityToReplace = PlayerManager.instance.availableAbilities.IndexOf(AbilityManager.instance.allAbilities[8]);        handleSwap();
    AudioManager.instance.Play(0);}
    void onaab10Pressed(){
abilityToReplace = PlayerManager.instance.availableAbilities.IndexOf(AbilityManager.instance.allAbilities[9]);        handleSwap();
    AudioManager.instance.Play(0);}
    
    


    
}
