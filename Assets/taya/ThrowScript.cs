using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class ThrowScript : MonoBehaviour
{
    public static bool isDisplayed = true;
    public static bool isResult;
    public static bool isThrowReady = false;

    //private Rigidbody r;
    private bool isDartsBack = true;

    private CalcScore calcScore;

    // Use this for initialization
    void Start()
    {
        transform.position = RayScript.handPosition;
        //r = GetComponent<Rigidbody>();

        calcScore = new CalcScore();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            StartCoroutine("HideWaitAnim");
            ThrowExecute();
        }

        if (isThrowReady)
        {
            isThrowReady = false;
            //投げる
            ThrowExecute();
        }
    }

    public void ThrowExecute()
    {
        Vector3 direction = RayScript.ray.direction;
        GetComponent<Rigidbody>().AddForce(direction.normalized * 60);

        StartCoroutine("ReturnDarts", 4);
    }

    //ダーツを手元に戻す
    IEnumerator ReturnDarts(int sec)
    {
        yield return new WaitForSeconds(sec); // 待機
        transform.position = RayScript.handPosition;
    }

    //オブジェクトが衝突したとき
    void OnTriggerEnter(Collider collider)
    {
        ////効いてない！？
        //GetComponent<Rigidbody>().velocity = Vector3.zero;
        //GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

        HitTarget(collider);

        isThrowReady = true;
    }

    void HitTarget(Collider collider)
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
            else
            {
                calcScore.ResultText();
            }

        }
    }


}
