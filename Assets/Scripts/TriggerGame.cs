using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TriggerGame : MonoBehaviour
{

    public GameObject fishingGame;
    // Start is called before the first frame update
    void Start()
    {
        fishingGame.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.X))
        {
            fishingGame.SetActive(true);
        }
        
    }
}
