﻿using System.Collections;
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

    //private static int finalScore;



    void Awake()
    {
    }

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
        Debug.Log("finalscore:" + ThrowScript.finalScore);
        Debug.Log("point:" + point);
        Debug.Log("meshtext:" + scoreText.name);

        //ThrowScript.temp = ThrowScript.finalScore;
        ThrowScript.finalScore = ThrowScript.finalScore - point;

        if (ThrowScript.finalScore == 0)
        {
            scoreText.GetComponent<TextMesh>().text = "Finished";
        }
        else if (ThrowScript.finalScore > 0)
        {
            messageText.GetComponent<TextMesh>().text = "Play" + ThrowScript.playNum.ToString();
            scoreText.GetComponent<TextMesh>().text = ThrowScript.finalScore.ToString();
        }
        else if (ThrowScript.finalScore < 0)
        {
            scoreText.GetComponent<TextMesh>().text = "Finished";
        }

        ThrowScript.isDisplayed = true;
    }
}
