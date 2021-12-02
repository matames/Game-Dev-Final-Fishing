using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform followTransform;
    public BoxCollider2D worldBounds;

    public float xMin, xMax, yMin, yMax;
    float camY, camX;

    float camSize;
    float camRatio;

    Camera mainCam;
    Vector3 smoothPos;

    //how quickly smoothing happens
    public float smoothRate;

    // Start is called before the first frame update
    void Start()
    {
        xMin = worldBounds.bounds.min.x;
        xMax = worldBounds.bounds.max.x;
        yMin = worldBounds.bounds.min.y;
        yMax = worldBounds.bounds.max.y;

        mainCam = gameObject.GetComponent<Camera>();

        //projection: perspective vs orthographic - perspective is a triangle prism 3D field of vision
        //vs orthographic is rectangular box
        camSize = mainCam.orthographicSize;

        //magic number 8.0f
        camRatio = (xMax + camSize) / 8.0f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        //math and trig are good for calculations/changing numbers (properties)
        //that being said, clamp numbers of our range (limit them)
        //clamp the transform follow pos between ymin etc and y max etc
        camY = Mathf.Clamp(followTransform.position.y, yMin + camSize, yMax - camSize);
        camX = Mathf.Clamp(followTransform.position.x, xMin + camRatio, xMax - camRatio);

        //lerping to where the player is
        smoothPos = Vector3.Lerp(gameObject.transform.position, new Vector3(camX, camY, gameObject.transform.position.z), smoothRate);
        //player position = smooth pos
        gameObject.transform.position = smoothPos;

        //Debug.Log(gameObject.transform.position);
    }
}
