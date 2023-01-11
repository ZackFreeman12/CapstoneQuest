using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 5f;

    // Start is called before the first frame update
    void Awake()
    {
        MainManager.instance.mapCamera = gameObject;
        if (MainManager.instance.newGame)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Map")
        {
            if (Input.GetKey(KeyCode.W))
                if (transform.position.y >= 10.8) { }
                else
                {
                    transform.position += Vector3.up * moveSpeed * Time.deltaTime;
                }
            else if (Input.GetKey(KeyCode.S))
                if (transform.position.y <= 0) { }
                else
                {
                    transform.position += -Vector3.up * moveSpeed * Time.deltaTime;
                }
        }
    }
}
