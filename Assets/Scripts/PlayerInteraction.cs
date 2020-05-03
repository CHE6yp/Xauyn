using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public GameObject itemPref;

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        int layer = 1 << 9;

        if (Physics.Raycast(ray, out hit, 100, layer))
        {
            //Debug.DrawLine(ray.origin, hit.point);
            //Debug.Log(hit.collider.name);
            if (Input.GetButtonDown("Fire1"))
            {
                //hit.collider.GetComponent<ItemPlace>().AddObject(itemPref);
                hit.collider.GetComponent<FigureDirection>().Move();
            }
        }
    }
}
