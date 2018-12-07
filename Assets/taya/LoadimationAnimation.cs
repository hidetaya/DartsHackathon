using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;
public class LoadimationAnimation : MonoBehaviour
{
    public Texture multiSpriteTexture;
    public Sprite[] sprites;

    [Range(0.0f, 0.99f)]
    public float animationSpeed = .9f;
    public bool reverse;
    public bool pingpong;

    public bool rotate;
    public float rotation = 15;
    public float rotationTick = .1f;


    int animateVariationsCounter;
    SpriteRenderer cacheRenderer;
    int countAdd = 1;
    bool pong;

    static public bool touched;//TayaAdded
    static public bool animReady = true;//TayaAdded
    static public bool stopAnim = false;//TayaAdded

    void Start()
    {
        if (!multiSpriteTexture) return;
        cacheRenderer = GetComponent<SpriteRenderer>();
        cacheRenderer.sprite = null;
    }

    void Update()
    {
        if (touched && animReady)
        {
            StartCoroutine("HideWaitAnim");

            animReady = false;
            touched = false;
            Invoke("Animate", 1f - animationSpeed);
            Invoke("RotateSprite", rotationTick);
        }
    }

    void Animate()
    {
        //Ray���I����O�ꂽ�ꍇ
        if (stopAnim)
        {
            animateVariationsCounter = 0;
            if (stopAnim) cacheRenderer.sprite = null;

            RayScript.isAnim = true;
            stopAnim = false;
            animReady = true;
            return;
        }

        //�A�j���[�V�������I�������ꍇ
        if (cacheRenderer.sprite == sprites[8])
        {
            animateVariationsCounter = 0;
            cacheRenderer.sprite = null;

            //������
            ThrowScript.isThrowReady = true;

            stopAnim = false;
            animReady = true;

            return;
        }

        if (sprites.Length == 0 || animationSpeed == 0) goto anim;
        if (pingpong && (animateVariationsCounter == sprites.Length || animateVariationsCounter == 0))
        {
            if (!pong) reverse = pong = true;
            else reverse = pong = false;
        }

        if (reverse)
        {
            if (animateVariationsCounter == 0) animateVariationsCounter = sprites.Length;
            countAdd = -1;
        }

        else countAdd = 1;
        Sprite s = sprites[animateVariationsCounter % sprites.Length];
        cacheRenderer.sprite = s;
        animateVariationsCounter += countAdd;
        anim: Invoke("Animate", 1f - animationSpeed);
    }

    //�A�j���[�V�����\���҂�
    IEnumerator HideWaitAnim()
    {
        RayScript.isAnim = false;
        yield return new WaitForSeconds(5); // num�b�ҋ@
        RayScript.isAnim = true;
    }

    void RotateSprite()
    {
        if (!rotate || rotation == 0) goto anim;
        transform.Rotate(Vector3.forward * rotation);
        anim: Invoke("RotateSprite", rotationTick);
    }

#if UNITY_EDITOR
    // Bug fix
    // Hides the SpriteRenderer, it's causing memory to build in Unity Editor.
    void Awake()
    {
        GetComponent<SpriteRenderer>().hideFlags = HideFlags.HideInInspector;
    }

    void OnApplicationQuit()
    {
        GetComponent<SpriteRenderer>().hideFlags = HideFlags.None;
    }
#endif
}


#if UNITY_EDITOR
[CustomEditor(typeof(LoadimationAnimation))]
[CanEditMultipleObjects]
public class LoadimationAnimationEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (EditorApplication.isPlaying) GUI.enabled = false;
        if (GUILayout.Button("Update Sprite Array"))
        {
            UpdateSpriteArray();
        }
        GUILayout.Space(10);
    }

    public void UpdateSpriteArray()
    {
        LoadimationAnimation tar = (target as LoadimationAnimation);
        string spriteSheet = AssetDatabase.GetAssetPath(tar.multiSpriteTexture);
        Object[] objs = AssetDatabase.LoadAllAssetsAtPath(spriteSheet);

        ArrayList alist = new ArrayList();
        foreach (Object o in objs)
        {
            if (o as Sprite != null) alist.Add(o as Sprite);
        }
        tar.sprites = (Sprite[])alist.ToArray(typeof(Sprite));
        //	objs = null;
        tar.GetComponent<SpriteRenderer>().sprite = tar.sprites[0];
        EditorUtility.SetDirty(target);
    }
}
#endif