using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class CalcScore : MonoBehaviour
{
    static int score;
    //static bool isFirstHit;
    static bool isFirstMulti;

    private static GameObject scoreText;
    private static GameObject messageText;

    private static int finalScore = 101;
    private int temp;

    // Use this for initialization
    void Start()
    {
        scoreText = GameObject.Find("ScoreText");
        messageText = GameObject.Find("ResultText");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public int Execute(string objectName)
    {
        //if (isFirstHit) DisplayText(score);
        if (objectName == "Trx2" || objectName == "Trx3")
        {
            score = 0;
        }
        else
        {
            score = int.Parse(objectName.Substring(2));
            //isFirstHit = true;
        }

        if (score == 0)
        {
            ResultText();
        }

        return score;
    }

    public void ResultText()
    {
        //Message    
    }

    static void DelayResultText()
    {
        messageText.GetComponent<TextMesh>().text = "Miss!";
    }

    public void DisplayText(int point)
    {
        Debug.Log("score:" + score);
        Debug.Log("point:" + point);
        Debug.Log("meshtext:" + scoreText.name);

        temp = finalScore - point;

        if (temp == 0)
        {
            scoreText.GetComponent<TextMesh>().text = "Finished";
        }
        else if (temp > 0)
        {
            scoreText.GetComponent<TextMesh>().text = temp.ToString();
        }
        else if (temp < 0)
        {
            scoreText.GetComponent<TextMesh>().text = temp.ToString();
            scoreText.GetComponent<TextMesh>().text = finalScore.ToString();
        }

        ThrowScript.isDisplayed = true;
    }
}
