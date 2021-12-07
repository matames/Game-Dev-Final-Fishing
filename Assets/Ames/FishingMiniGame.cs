 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingMiniGame : MonoBehaviour
{
    public GameObject fishingGame;

    [SerializeField] Transform topPivot;
    [SerializeField] Transform bottomPivot;

    [SerializeField] Transform fish;

    float fishPosition;
    float fishDestination;

    float fishTimer;
    [SerializeField] float timerMultiplicator = 3f;

    float fishSpeed;
    [SerializeField] float smoothMotion = 1f;


    [SerializeField] Transform hook;
    float hookPosition;
    [SerializeField] float hookSize = 0.1f;
    [SerializeField] float hookPower = 5f;
    float hookProgress;
    float hookPullVelocity;
    [SerializeField] float hookPullPower = 0.01f;
    [SerializeField] float hookGravityPower = 0.005f;
    [SerializeField] float hookProgressDegradationPower = 0.1f;


    [SerializeField] SpriteRenderer hookSpriteRenderer;



    [SerializeField] Transform progressBarContainer;


    bool pause = false;
    public static bool win = false;

    [SerializeField] float failTimer = 10f;

    public int caughtFish = 0;      // win/lose check; 0 = not fishing anything

    public AudioSource lineTugSource;

    public AudioSource mySource;
    public AudioClip playerOutOfBoundsSFX;
    public AudioClip fishCaughtSFX;
    //public AudioClip fishPulledOutSFX; --> couldn't put this audio in since there isn't
    //a pause between when the fish has been caught and when it's stored in the inventory

    private bool hasPlayed = false;

    // Start is called before the first frame update

    // Update is called once per frame

    private void Start()
    {
        Resize();
    }

    private void Resize()
    {
        Bounds b = hookSpriteRenderer.bounds;
        float ySize = b.size.y;
        Vector3 ls = hook.localScale;
        float distance = Vector3.Distance(topPivot.position, bottomPivot.position);
        ls.y = (distance / ySize * hookSize);
        hook.localScale = ls;

    }


    private void Update()
    {
        if (pause) { return; }
        Fish();
        Hook();
        ProgressCheck();
    }


    private void ProgressCheck()
    {
        Vector3 ls = progressBarContainer.localScale;
        ls.y = hookProgress;
        progressBarContainer.localScale = ls;

        float min = hookPosition - hookSize / 2;
        float max = hookPosition + hookSize / 2;

        if (min < fishPosition && fishPosition < max)
        {
            hookProgress += hookPower * Time.deltaTime;
            hasPlayed = false;

        }
        else
        {
            hookProgress -= hookProgressDegradationPower * Time.deltaTime;
            failTimer -= Time.deltaTime;

            if (!hasPlayed)
            {
                mySource.PlayOneShot(playerOutOfBoundsSFX);
                hasPlayed = true;
            }

            if (failTimer < 0f)
            {
                Lose();
            }

        }

        


        if (hookProgress >= 1f)
        {
            mySource.PlayOneShot(fishCaughtSFX);
            Win();
        }

        hookProgress = Mathf.Clamp(hookProgress, 0f, 1f);
    }


    public void Lose()
    {
        win = false;

        caughtFish = 2;     // 2 = lose
        pause = true;
        //fishingGame.SetActive(false);
        Debug.Log("YOU LOSE! NO FISH FOR YOU!!!");

        lineTugSource.Stop();
    }


    public void Win()
    {
        win = true;

        caughtFish = 1;

        hookProgress = 0;

        // 1 = win
        //pause = true;
        //fishingGame.SetActive(false);
        Debug.Log("YOU WIN! FISH CAUGHT!");

        lineTugSource.Stop();
    }


    void Hook()
    {
        if (Input.GetMouseButton(0))
        {
            hookPullVelocity += hookPullPower * Time.deltaTime;
            lineTugSource.Play();
        }

        hookPullVelocity -= hookGravityPower * Time.deltaTime;

        hookPosition += hookPullVelocity;

        if (hookPosition - hookSize/2 < 0f && hookPullVelocity < 0f)
        {
            hookPullVelocity = 0f;
        }

        if (hookPosition + hookSize/2 > 1f && hookPullVelocity > 0f)
        {
            hookPullVelocity = 0f;
        }

        hookPosition = Mathf.Clamp(hookPosition, hookSize/2, 1 - hookSize/2);
        hook.position = Vector3.Lerp(bottomPivot.position, topPivot.position, hookPosition);



    }
     void Fish()
    {
        fishTimer -= Time.deltaTime;
        if (fishTimer < 0f)
        {
            fishTimer = UnityEngine.Random.value * timerMultiplicator;

            fishDestination = UnityEngine.Random.value;
        }

        fishPosition = Mathf.SmoothDamp(fishPosition, fishDestination, ref fishSpeed, smoothMotion);
        fish.position = Vector3.Lerp(bottomPivot.position, topPivot.position, fishPosition);
    }

}
