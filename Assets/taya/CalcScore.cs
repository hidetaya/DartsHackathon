using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class CalcScore : MonoBehaviour
{
    static int score;
    //static bool isFirstHit;
    static bool isFirstMulti;

    private static GameObject messageText;
    private int finalScore = 101;
    private int temp;

    // Use this for initialization
    void Start()
    {
        messageText = GameObject.Find("ScoreText");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public int  Execute(string objectName)
    {
        //if (isFirstHit) DisplayText(score);
        if (objectName == "Trx2" || objectName == "Trx3")
            return 0;

        else
        {
            score = int.Parse(objectName.Substring(2));
            //isFirstHit = true;
        }

        return score;
    }

    public void DisplayText(int point)
    {
        Debug.Log("score:" + score);
        Debug.Log("point:" + point);
        Debug.Log("meshtext:" + messageText.name);

        temp = finalScore - point;

        if (temp == 0)
        {
            messageText.GetComponent<TextMesh>().text = "Finished";
        }
        else if (temp > 0)
        {
            messageText.GetComponent<TextMesh>().text = temp.ToString();
        }
        else if (temp < 0)
        {
            messageText.GetComponent<TextMesh>().text = temp.ToString();
            Thread.Sleep(300);
            messageText.GetComponent<TextMesh>().text = finalScore.ToString();
        }

        ThrowScript.isDisplayed = true;
    }
}
