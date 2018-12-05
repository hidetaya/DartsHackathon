using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalcScore : MonoBehaviour
{

    static int score;
    static bool isFirstHit;
    static bool isFirstMulti;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public static int Execute(string objectName)
    {
        if (isFirstHit) return score;
        if (objectName == "Trx2" || objectName == "Trx3") return score;
        
        //    isFirstMulti = true;
        //    if (isFirstHit)
        //    {
        //        int multi = int.Parse(objectName.Substring(3));
        //        score = score * multi;
        //    }
        //}

        else
        {
            score = int.Parse(objectName.Substring(2));
            isFirstHit = true;
        }

        return score;
    }
}
