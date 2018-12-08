using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayScript : MonoBehaviour
{

    public new Camera camera;

    public static Vector3 handPosition;
    public static Ray ray;
    public static RaycastHit hit;

    //投擲～n秒後までレイが出ないようにする
    public static bool isAnim = true;

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
        SplashRay();
    }

    void SplashRay()
    {
        ray = new Ray(camera.transform.position, camera.transform.forward);

        if (Physics.Raycast(ray, out hit))
        {
            laserLine.SetPosition(0, hit.point);

            if (hit.collider.gameObject.tag == "DartsObject")
            {
                //投げ中はダーツオブジェクトにあたってもアニメーションがスタートにならないようにする
                if (isAnim)
                {
                    isAnim = false;
                    //アニメーションスタート
                    LoadimationAnimation.touched = true;
                }
            }
            else
            {
                LoadimationAnimation.stopAnim = true;
            }

        }
    }
}
