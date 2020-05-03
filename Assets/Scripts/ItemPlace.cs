using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPlace : MonoBehaviour
{
    public Item parentItem;

    // Start is called before the first frame update
    void Awake()
    {
        parentItem = transform.parent.GetComponent<Item>();
    }

    public void AddObject(GameObject obj)
    {
        transform.parent.parent.GetComponent<Figure>().AddItem(transform.parent.transform.position + transform.localPosition);
    }
}
