using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineFitThrowAndRay : MonoBehaviour {

    private LineRenderer laserLine;

    // Use this for initialization
    void Start () {
        laserLine = GetComponent<LineRenderer>();
        laserLine.SetPosition(1, RayScript.handPosition);
    }
	
	// Update is called once per frame
	void Update () {

        laserLine.SetPosition(0, RayScript.hit.point);
    }
}
