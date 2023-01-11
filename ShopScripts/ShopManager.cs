using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class ShopManager : MonoBehaviour
{
    public Button ib1;
    public Button ib2;
    public Button ib3;
    public Button nextFloor;
    public Label il1;
    public Label il2;
    public Label il3;
    public Label gp;
    int choice1;
    string[] nameDesc1;
     int choice2;
    string[] nameDesc2;
    int choice3;
    string[] nameDesc3;

    void Awake(){
        
        var root = GameObject.Find("UIDocument").GetComponent<UIDocument>().rootVisualElement;

        ib1 = root.Q<Button>("ib1");
        ib2 = root.Q<Button>("ib2");
        ib3 = root.Q<Button>("ib3");
        il1 = root.Q<Label>("il1");
        il2 = root.Q<Label>("il2");
        il3 = root.Q<Label>("il3");
        gp = root.Q<Label>("gp");
        nextFloor = root.Q<Button>("nextFloor");

        ib1.clicked += onlb1Pressed;
        ib2.clicked += onlb2Pressed;
        ib3.clicked += onlb3Pressed;
        nextFloor.clicked += onnextFloorPressed;

        choice1 = Random.Range(1,8);
        nameDesc1 = LootManager.instance.LootToString(choice1);
        choice2 = Random.Range(1,8);
        nameDesc2 = LootManager.instance.LootToString(choice2);
        choice3 = Random.Range(1,8);
        nameDesc3 = LootManager.instance.LootToString(choice3);

        il1.text = nameDesc1[0];
        il2.text = nameDesc2[0];
        il3.text = nameDesc3[0];
        
        gp.text = "Your GP: " + PlayerManager.instance.GP;

    }

    void onnextFloorPressed(){
        SceneManager.LoadScene("Map");
        AudioManager.instance.Play(0);
    }
    void onlb1Pressed(){
        if(PlayerManager.instance.GP >= 50){
            PlayerManager.instance.GP -= 50;
            PlayerManager.instance.newItem(choice1);
            nextFloor.style.display = DisplayStyle.Flex;  
            gp.text = "Your GP: " + PlayerManager.instance.GP;
        }
        AudioManager.instance.Play(0);
        
    }
    void onlb2Pressed(){
        if(PlayerManager.instance.GP >= 50){
            PlayerManager.instance.GP -= 50;
            PlayerManager.instance.newItem(choice2);
            nextFloor.style.display = DisplayStyle.Flex;  
            gp.text = "Your GP: " + PlayerManager.instance.GP;
        }
        AudioManager.instance.Play(0);
    }
    void onlb3Pressed(){
        if(PlayerManager.instance.GP >= 50){
            PlayerManager.instance.GP -= 50;
            PlayerManager.instance.newItem(choice3);
            nextFloor.style.display = DisplayStyle.Flex;  
            gp.text = "Your GP: " + PlayerManager.instance.GP;
        }
        AudioManager.instance.Play(0);
    }


}
