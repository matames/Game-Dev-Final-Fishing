using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TriggerGame : MonoBehaviour
{

    public GameObject fishingGame;
    public GameObject castingBar;
    // Start is called before the first frame update
    void Start()
    {
        fishingGame.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (fishingGame.activeSelf)
        {
            castingBar.SetActive(false);
        }

        if (fishingGame.activeSelf == false)
        {
            castingBar.SetActive(true);
        }

        if (Input.GetKey(KeyCode.X))
        {
            fishingGame.SetActive(true);
        }
        
    }
}
