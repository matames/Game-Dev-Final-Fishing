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

    // Update is called once per frame
    void Update()
    {
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
}
