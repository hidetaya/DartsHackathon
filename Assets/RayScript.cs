using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayScript : MonoBehaviour
{

    public Camera camera;

    static public Vector3 handPosition;
    static public Ray ray;
    static public RaycastHit hit;

    private LineRenderer laserLine;

    // Use this for initialization

    void Awake()
    {
        laserLine = GetComponent<LineRenderer>();
        Vector3 v = camera.transform.position;
        handPosition = new Vector3(v.x, v.y, v.z);
    }

    void Start()
    {
        laserLine.SetPosition(1, handPosition);
    }

    // Update is called once per frame
    void Update()
    {
        ray = new Ray(camera.transform.position, camera.transform.forward);

        if (Physics.Raycast(ray, out hit))
        {
            laserLine.SetPosition(0, hit.point);

            if (hit.collider.gameObject.tag == "DartsObject")
            {
                LoadimationAnimation.touched = true;
            }

            else
            {
                LoadimationAnimation.stopAnim = true;
            }

            //Debug.Log(hit.collider.gameObject.name);
            //Debug.Log(hit.collider.gameObject.tag);
        }

    }
}
