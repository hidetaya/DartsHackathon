using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class ThrowScript : MonoBehaviour
{
    public Camera camera;
    public static bool isDisplayed = true;
    public static bool isResult;

    public ParticleSystem hitParticlePrefab; // 的に当たったときのパーティクル

    public GameObject hand;
    public Animator throwHand;

    private AudioSource hit;        // AudioSorceを格納する変数の宣言.
    public AudioClip hitSound;             // 効果音を格納する変数の宣言.

    private AudioSource kansei;        // AudioSorceを格納する変数の宣言.
    public AudioClip kanseiSound;             // 効果音を格納する変数の宣言.

    static Rigidbody r;

    private CalcScore calcScore;

    public static bool isThrowReady = false;

    public static int finalScore;
    //public static int temp;

    private bool isDartsBack = true;

    public static int playNum;


    void Awake()
    {
        playNum = 0;
        //m_throwExecute.ThrowTrigger += ThrowExecute;
        finalScore = 101;
        calcScore = new CalcScore();
    }

    // Use this for initialization
    void Start()
    {
        transform.position = RayScript.handPosition;

        throwHand = hand.GetComponent<Animator>();
        throwHand.SetBool("Play", false);

        hit = gameObject.AddComponent<AudioSource>();   // AudioSorceコンポーネントを追加し、変数に代入
        hit.clip = hitSound;       // 鳴らす音(変数)を格納
        hit.loop = false;       // 音のループなし

        kansei = gameObject.AddComponent<AudioSource>();   // AudioSorceコンポーネントを追加し、変数に代入
        kansei.clip = kanseiSound;       // 鳴らす音(変数)を格納
        kansei.loop = false;       // 音のループなし


        transform.position = RayScript.handPosition;
        r = GetComponent<Rigidbody>();

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

        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
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
        r.AddForce(direction.normalized * 120);

        playNum++;

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

            // 矢が当たったとき、光らせる
            Instantiate(hitParticlePrefab, transform.position, transform.rotation);

            // 矢が当たったとき、音を鳴らす
            hit.Play();
            kansei.Play();

            // 手の動きを再生
            throwHand.SetBool("Play", true);

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
