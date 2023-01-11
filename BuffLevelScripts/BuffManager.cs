using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class BuffManager : MonoBehaviour
{
    public VisualElement buffMenu;
    public Button strb;
    public Button wisb;
    public Button nextFloor;

    public Label strl;
    public Label wisl;

    void Awake(){
        var root = GameObject.Find("UIDocument").GetComponent<UIDocument>().rootVisualElement;

        buffMenu = root.Q<VisualElement>("buffMenu");

        strb = root.Q<Button>("strb");
        wisb = root.Q<Button>("wisb");
        nextFloor = root.Q<Button>("nextFloor");
        strl = root.Q<Label>("strl");
        wisl = root.Q<Label>("wisl");

        strb.clicked += strbPressed;
        wisb.clicked += wisbPressed;
        nextFloor.clicked += onnextFloorPressed;
        refreshUI();
    }
    void refreshUI(){
        strl.text = "STR: " + PlayerManager.instance.STR;
        wisl.text = "WIS: " +  PlayerManager.instance.WIS;
    }
    void strbPressed(){
        PlayerManager.instance.STR += 1;
        refreshUI();
         nextFloor.style.display = DisplayStyle.Flex;
         strb.SetEnabled(false);
         wisb.SetEnabled(false);
         AudioManager.instance.Play(0);
    }
    void wisbPressed(){
        PlayerManager.instance.WIS += 1;
        refreshUI();
        AudioManager.instance.Play(0);
        wisb.SetEnabled(false);
        strb.SetEnabled(false);
        nextFloor.style.display = DisplayStyle.Flex;
    }
    void onnextFloorPressed(){
        SceneManager.LoadScene("Map");
        AudioManager.instance.Play(0);
    }

    void  OnMouseUp()
    {
        buffMenu.style.display = DisplayStyle.Flex;
        AudioManager.instance.Play(0);
    }

}
