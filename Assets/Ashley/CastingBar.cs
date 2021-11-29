using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CastingBar : MonoBehaviour
{
    [SerializeField]
    private Image imageProgressUp;

    private bool isCasting = false;
    private bool isDirectionRight = true;
    private float progressAmt = 0.0f;
    private float progressSpeed = 0.8f;

    private bool bite = false;      // when player gets a bit
    private bool lineInWater = false;

    public GameObject fishingGame;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && lineInWater && bite)
        {
            fishingGame.SetActive(true);
        }// idk how the fishing game win system works we need a public win bool - amiya
        // if(fishing game win){fishingGame.SetActive(false);}

        if (Input.GetMouseButtonDown(0))
        {
            StartProgress();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            EndProgress();
        }

        if (isCasting)
        {
            CastingActive();
        }
    }

    // when first throwing in the line
    void CastingActive()
    {
        if (isDirectionRight)
        {
            progressAmt += Time.deltaTime * progressSpeed;

            if (progressAmt > 1f)
            {
                isDirectionRight = false;
                progressAmt = 1f;
            }
        }

        else
        {
            progressAmt -= Time.deltaTime * progressSpeed;

            if (progressAmt < 0f)
            {
                isDirectionRight = true;
                progressAmt = 0.0f;
            }
        }

        imageProgressUp.fillAmount = progressAmt;
    }

    public void StartProgress()
    {
        isCasting = true;
        progressAmt = 0.0f;
        isDirectionRight = true;
    }

    public void EndProgress()
    {
        isCasting = false;

        if (progressAmt < 0.3f)
        {
            Debug.Log("Weak Casting");
        }
        else if (progressAmt > 0.3f && progressAmt < 0.7f)
        {
            Debug.Log("Average Casting");
        }
        else if (progressAmt > 0.7f)
        {
            Debug.Log("Strong Casting");
        }
    }

    private IEnumerator WaitForBite(float maxWaitTime)
    {
        yield return new WaitForSeconds(Random.Range(maxWaitTime * 0.25f, maxWaitTime)); //Wait between 25% of maxWaitTime and the maxWaitTime
        Debug.Log("Hit!"); // animation for alert here
        
        //thoughtBubbles.GetComponent<Animator>().SetTrigger("Alert"); //Show the alert thoughtbubble
        
        bite = true;
        StartCoroutine(LineBreak(2)); // if no clickings in 2 seconds break the line
    }
    
    // waiting for a bite while the line is in the water
    private void CastWait()
    {
        lineInWater = true;
        StartCoroutine(WaitForBite(10));
    }

    private IEnumerator LineBreak(float lineBreakTime)
    {
        yield return new WaitForSeconds(lineBreakTime);
        Debug.Log("Line Broke!");

        lineInWater = false;
        bite = false;
    }
}
