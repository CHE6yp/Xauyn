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

    public void Move(Vector3 dir)
    {
        if (falling) return;

        fallDirection = CountDistance(dir);
        Debug.Log(fallDirection);
        destination = transform.position + fallDirection;
        falling = true;
        StartCoroutine(Drop());
        coordinates = destination;
    }

    Vector3 CountDistance(Vector3 dir)
    {
        List<Vector3> f = GetClearDirection(dir);
        List<Vector3> s = staticFigure.GetClearDirection(dir*-1);
        float minDist = Mathf.Infinity;
        Debug.Log("f");
        foreach (Vector3 v in f)
        {
            Debug.Log(v);
        }
        Debug.Log("s");
        foreach (Vector3 v in s)
        {
            Debug.Log(v);
        }


        float dist;
        List<Vector3> underItems = new List<Vector3>();
        float corner;
        Debug.Log("UnderItems");
        foreach (Vector3 fc in f)
        {
            Debug.Log("All for " + fc);
            if (dir == Vector3.up)
            {
                underItems = s.FindAll(c => c.x == fc.x && c.y > fc.y && c.z == fc.z);
                if (underItems.Count > 0)
                {
                    corner = underItems.Min(c => c.y);
                    dist = fc.y - corner;
                    minDist = Mathf.Min(Mathf.Abs(dist), minDist);
                }
            }
            if (dir == Vector3.down)
            {
                underItems = s.FindAll(c => c.x == fc.x && c.y < fc.y && c.z == fc.z);
                if (underItems.Count > 0)
                {
                    corner = underItems.Max(c => c.y);
                    dist = fc.y - corner;
                    minDist = Mathf.Min(Mathf.Abs(dist), minDist);
                }
            }
            if (dir == Vector3.right)
            {
                underItems = s.FindAll(c => c.x > fc.x && c.y == fc.y && c.z == fc.z);
                if (underItems.Count > 0)
                {
                    corner = underItems.Min(c => c.x);
                    dist = fc.x - corner;
                    minDist = Mathf.Min(Mathf.Abs(dist), minDist);
                }
            }
            if (dir == Vector3.left)
            {
                underItems = s.FindAll(c => c.x < fc.x && c.y == fc.y && c.z == fc.z);
                if (underItems.Count > 0)
                {
                    corner = underItems.Max(c => c.x);
                    dist = fc.x - corner;
                    minDist = Mathf.Min(Mathf.Abs(dist), minDist);
                }
            }
            if (dir == Vector3.forward)
            {
                underItems = s.FindAll(c => c.x == fc.x && c.y == fc.y && c.z > fc.z);
                if (underItems.Count > 0)
                {
                    corner = underItems.Min(c => c.z);
                    dist = fc.z - corner;
                    minDist = Mathf.Min(Mathf.Abs(dist), minDist);
                }
            }
            if (dir == Vector3.back)
            {
                underItems = s.FindAll(c => c.x == fc.x && c.y == fc.y && c.z < fc.z);
                if (underItems.Count > 0)
                {
                    corner = underItems.Max(c => c.z);
                    dist = fc.z - corner;
                    minDist = Mathf.Min(Mathf.Abs(dist), minDist);
                }
            }
            foreach (Vector3 v in underItems)
            {
                Debug.Log(v);
            }
        }

        
        
        Debug.Log("==="); 
        Debug.Log(dir);
        Debug.Log(minDist);
        if (minDist == Mathf.Infinity)
        {
            Debug.Log("Ouch");
            return dir * 15;
        }
        return dir*(Mathf.Abs(minDist)-1);
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
        CheckSuccess();
    }


    void CheckSuccess()
    {
        if (coordinates == staticFigure.info.goalCoordinates)
        {
            LevelController.instance.NextLevel();
        }
    }
}
