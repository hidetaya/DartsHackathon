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


    // Use this for initialization
    void Start()
    {
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
            ThrowExecute();
        }
    }

    public static void ThrowExecute()
    {
        Vector3 direction = RayScript.ray.direction;
        r.AddForce(direction.normalized * 120);
    }

    //オブジェクトが衝突したとき
    void OnTriggerEnter(Collider collider)
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

            // 矢が当たったとき、光らせる
            Instantiate(hitParticlePrefab, transform.position, transform.rotation);

            // 矢が当たったとき、音を鳴らす
            hit.Play();
            kansei.Play();

            // 手の動きを再生
            throwHand.SetBool("Play", true);

            //Debug.Log(collider.gameObject.name);
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

        }
        else
        {
            //検討
            //Invoke("CalcScore.DelayResultText", 3f);
        }

    }
}
