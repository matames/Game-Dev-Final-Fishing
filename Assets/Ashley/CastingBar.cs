using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CastingBar : MonoBehaviour
{
    [SerializeField]
    private Image imageProgressUp;

    private bool isCasting = false;     // when first throwing in line
    private bool isDirectionRight = true;
    private float progressAmt = 0.0f;
    private float progressSpeed = 0.8f;

    private bool bite = false;      // when player gets a bit
    private bool lineInWater = false;       // when waiting for a bite
    private bool reeling = false;           // when playing the mini game

    public GameObject fishingGame;
    public GameObject castBarVisible;   // object for the casting progress bar

    public FishingMiniGame miniGame;

    public AudioSource mySource;
    public AudioClip castBarSFX;
    public AudioClip clinkSFX;
    public AudioClip lineCastWooshSplooshSFX;
    public AudioClip biteAlertSFX;
    public AudioClip hitAlertSFX;

    void Start()
    {
        fishingGame.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && lineInWater && bite && !reeling)
        {
            reeling = true;
            fishingGame.SetActive(true);
            StopAllCoroutines(); //Stops the LineBreak timer
            mySource.PlayOneShot(hitAlertSFX);
        }

        if (miniGame.caughtFish > 0)
        {
            reeling = false;
            lineInWater = false;
            bite = false;

            fishingGame.SetActive(false);
            miniGame.caughtFish = 0;
        }
        
        if (Input.GetMouseButtonDown(0) && !lineInWater && !reeling)
        {
            StartProgress();
            mySource.PlayOneShot(castBarSFX);
        }
        else if (Input.GetMouseButtonUp(0) && !lineInWater && !reeling)
        {
            EndProgress();
            mySource.PlayOneShot(clinkSFX);
        }

        if (isCasting && !lineInWater && !reeling)
        {
            CastingActive();
            castBarVisible.SetActive(true);
        }
        else
        {
            castBarVisible.SetActive(false);
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

        CastWait();
    }

    // waiting for a bite while the line is in the water
    private void CastWait()
    {
        lineInWater = true;
        StartCoroutine(WaitForBite(5));
        mySource.PlayOneShot(lineCastWooshSplooshSFX);
    }

    private IEnumerator WaitForBite(float maxWaitTime)
    {
        yield return new WaitForSeconds(Random.Range(maxWaitTime * 0.25f, maxWaitTime)); //Wait between 25% of maxWaitTime and the maxWaitTime
        Debug.Log("Hit!"); // animation for alert here

        //thoughtBubbles.GetComponent<Animator>().SetTrigger("Alert"); //Show the alert thoughtbubble

        bite = true;

        mySource.PlayOneShot(biteAlertSFX);

        StartCoroutine(LineBreak(2)); // if no clickings in 2 seconds break the line
    }

    private IEnumerator LineBreak(float lineBreakTime)
    {
        yield return new WaitForSeconds(lineBreakTime);
        Debug.Log("Line Broke!");

        lineInWater = false;
        bite = false;
        fishingGame.SetActive(false);
    }
}