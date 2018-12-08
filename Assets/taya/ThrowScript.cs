using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System;

public class ThrowScript : MonoBehaviour
{
    public static bool isDisplayed = true;
    public static bool isResult;
    public static bool isThrowReady = false;

    public static int finalScore;
    public static int temp;

    private bool isDartsBack = true;

    private CalcScore calcScore;

    //設定されない
    //private LoadimationAnimation m_throwExecute;
    // Use this for initialization

    //反映されない
    void Awake()
    {
        //m_throwExecute.ThrowTrigger += ThrowExecute;
        finalScore = 30;
        calcScore = new CalcScore();
    }

    void Start()
    {
        transform.position = RayScript.handPosition;

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

        StartCoroutine("ReturnDarts", 3);
    }

    IEnumerator Sleep()
    {
        yield return new WaitForSeconds(3);

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
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

        HitTarget(collider);

    }

    void HitTarget(Collider collider)
    {
        Debug.Log("1");
        Debug.Log(collider.gameObject.name);


        if (LayerMask.LayerToName(collider.gameObject.layer) == "Target" && isDisplayed)
        {
            Debug.Log("2");

            if (isResult) return;
            int score = calcScore.Execute(collider.gameObject.name);

            if (score != 0)
            {
                isResult = true;
                isDisplayed = false;
                calcScore.DisplayText(score);
                isResult = false;
                Debug.Log(score);
            }
            else
            {
                calcScore.ResultText();
            }

        }
    }


}
