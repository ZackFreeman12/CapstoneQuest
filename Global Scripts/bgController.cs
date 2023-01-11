using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bgController : MonoBehaviour
{
    public SpriteRenderer spr;
    public Sprite bg1;
    public Sprite bg2;
    public Sprite bg3;
    public int num;
    // Start is called before the first frame update
    void Start()
    {
        num = Random.Range(0,3);
        if(num == 0){
            spr.sprite = bg1;
        }
        if(num == 1){
            spr.sprite = bg2;
        }
        if(num == 2){
            spr.sprite = bg3;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
