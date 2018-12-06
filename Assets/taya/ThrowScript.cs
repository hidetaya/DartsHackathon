using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowScript : MonoBehaviour
{
    public Camera camera;
    public static bool isDisplayed = true;
    public static bool isResult;
    static Rigidbody r;

    private CalcScore calcScore;


    // Use this for initialization
    void Start()
    {
        transform.position = RayScript.handPosition;
        r = GetComponent<Rigidbody>();

        calcScore = new CalcScore();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ThrowExecute();
        }
    }

    public static void ThrowExecute()
    {
        Vector3 direction = RayScript.ray.direction;
        r.AddForce(direction.normalized * 120);
    }

    //オブジェクトが衝突したとき
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.layer == 9 && isDisplayed)
        {
            if (isResult) return;
            int score = calcScore.Execute(collider.gameObject.name);

            if (score != 0)
            {
                isResult = true;
                isDisplayed = false;
                calcScore.DisplayText(score);
                //Debug.Log(score);
            }

            //Debug.Log(collider.gameObject.name);
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
    }
}
