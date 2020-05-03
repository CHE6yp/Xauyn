using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class MovingFigure : Figure
{
    public Vector3 fallDirection;
    public float moveSpeed = 0.1f;
    Vector3 destination;
    bool falling;
    public Figure staticFigure;

    // Update is called once per frame
    void Update()
    {
        if (!falling)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                fallDirection = CountDistanceDown();
                destination = transform.position + fallDirection;
                falling = true;
                StartCoroutine(Drop());
                coordinates = destination;
            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                fallDirection = CountDistanceUp();
                destination = transform.position + fallDirection;
                falling = true;
                StartCoroutine(Drop());
                coordinates = destination;
            }
        }

    }


    Vector3 CountDistanceUp()
    {
        List<Vector3> f = GetClearUpY();
        List<Vector3> s = staticFigure.GetClearDownY();
        float minDist = Mathf.Infinity;

        foreach (Vector3 fc in f)
        {
            float dist;
            List<Vector3> underItems = s.FindAll(c => c.x == fc.x && c.z == fc.z && c.y > fc.y);
            if (underItems.Count > 0)
            {
                float minY = underItems.Min(c => c.y);
                dist = fc.y - minY;
                minDist = Mathf.Min(dist, minDist);
            }
        }

        if (minDist == Mathf.Infinity)
        {
            Debug.Log("Ouch");
            return new Vector3(0, 15, 0);
        }
        return new Vector3(0, -minDist - 1, 0);
    }

    Vector3 CountDistanceDown()
    {
        List<Vector3> f = GetClearDownY();
        List<Vector3> s = staticFigure.GetClearUpY();
        float minDist = Mathf.Infinity;

        foreach(Vector3 fc in f)
        {
            float dist;
            List<Vector3> underItems = s.FindAll(c => c.x == fc.x && c.z == fc.z && c.y < fc.y);
            if (underItems.Count>0)
            {
                float maxY = underItems.Max(c => c.y);
                dist = fc.y - maxY;
                minDist = Mathf.Min(dist, minDist);
            }
        }

        if (minDist == Mathf.Infinity)
        {
            Debug.Log("Ouch");
            return new Vector3(0, -15, 0);
        }
        return new Vector3(0, -minDist + 1, 0);
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
        falling = false;
    }
}
