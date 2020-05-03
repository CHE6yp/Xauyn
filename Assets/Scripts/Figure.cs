using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Figure : MonoBehaviour
{

    public Vector3 fallDirection;
    public float moveSpeed = 0.1f;
    Vector3 destination;
    bool falling;
    public List<Item> items;
    public GameObject itemPref;

    // Start is called before the first frame update
    void Awake()
    {
        items = GetComponentsInChildren<Item>().ToList();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            destination = transform.position + fallDirection;
            falling = true;
            StartCoroutine(Drop());
        }

    }

    void Fall()
    {
        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector3.Lerp(transform.position, destination, step);
        if (transform.position == destination)
            falling = false;
    }



    /// <summary>
    /// Процедурно создаем анимацию падения предмета в зависимости от высоты. И дропаем
    /// </summary>
    public IEnumerator Drop()
    {
        Animation anim = GetComponent<Animation>();
        //StartCoroutine(Drop(animator));

        AnimationCurve curveX, curveY, curveZ;

        AnimationClip clip = new AnimationClip();
        clip.legacy = true;

        Keyframe[] keys;
        keys = new Keyframe[2];
        keys[0] = new Keyframe(0.0f, transform.position.x);
        keys[1] = new Keyframe(0.5f, destination.x);
        curveX = new AnimationCurve(keys);

        clip.SetCurve("", typeof(Transform), "localPosition.x", curveX);

        keys = new Keyframe[2];
        keys[0] = new Keyframe(0.0f, transform.position.y);
        keys[1] = new Keyframe(0.5f, destination.y);
        curveY = new AnimationCurve(keys);
        
        clip.SetCurve("", typeof(Transform), "localPosition.y", curveY);

        keys = new Keyframe[2];
        keys[0] = new Keyframe(0.0f, transform.position.z);
        keys[1] = new Keyframe(0.5f, destination.z);
        curveZ = new AnimationCurve(keys);

        clip.SetCurve("", typeof(Transform), "localPosition.z", curveZ);

        anim.AddClip(clip, "tmp");
        anim.Play("tmp");
        while (anim.isPlaying)
            yield return null;
        falling = true;
    }

    public void AddItem(Vector3 coordinates)
    {
        GameObject obj = Instantiate(itemPref);
        obj.transform.position = coordinates;
        obj.transform.SetParent(transform);
        items.Add(obj.GetComponent<Item>());
    }
}
