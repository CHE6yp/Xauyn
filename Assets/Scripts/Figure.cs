using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Figure : MonoBehaviour
{
    public FigureInfo info;
    public Vector3 coordinates;
    public List<Item> items;
    public GameObject itemPref;

    // Start is called before the first frame update
    void Awake()
    {
        CreateFigure();
        //items = GetComponentsInChildren<Item>().ToList();
    }

    void CreateFigure()
    {
        transform.position = coordinates = info.coordinates;
        foreach(Vector3 crd in info.items)
        {
            AddItem(crd);
        }
    }

    public void AddItem(Vector3 coordinates)
    {
        GameObject obj = Instantiate(itemPref);

        obj.transform.SetParent(transform);
        obj.transform.localPosition = coordinates;
        Item i = obj.GetComponent<Item>();
        i.coordinates = obj.transform.localPosition;
        items.Add(i);
    }


    public List<Vector3> GetClearDownY()
    {
        List<Vector3> coords = new List<Vector3>();
        foreach (Item item in items){
            if (items.Find(i => i.coordinates.x == item.coordinates.x && i.coordinates.z == item.coordinates.z && i.coordinates.y == item.coordinates.y-1) == null)
            {
                coords.Add(item.coordinates + coordinates);
            }
        }
        return coords;
    }

    public List<Vector3> GetClearUpY()
    {
        List<Vector3> coords = new List<Vector3>();
        foreach (Item item in items)
        {
            if (items.Find(i => i.coordinates.x == item.coordinates.x && i.coordinates.z == item.coordinates.z && i.coordinates.y == item.coordinates.y + 1) == null)
            {
                coords.Add(item.coordinates + coordinates);
            }
        }
        return coords;
    }

    public List<Vector3> GetLowestY()
    {
        List<Vector3> coords = new List<Vector3>();
        foreach (Item item in items)
        {
            int index = coords.FindIndex(i => i.x == item.coordinates.x && i.z == item.coordinates.z);

            if (index < 0)
            {
                coords.Add(item.coordinates);
            }
            else if (coords[index].y > item.coordinates.y)
            { 
                coords[index] = item.coordinates;
            }

        }
        foreach (Vector3 c in coords.OrderBy(o => o.x).ThenBy(o => o.z))
        {
            Debug.Log(c);
        }
        return coords;
    }

    public List<Vector3> GetHighestY()
    {
        List<Vector3> coords = new List<Vector3>();
        foreach (Item item in items)
        {
            int index = coords.FindIndex(i => i.x == item.coordinates.x && i.z == item.coordinates.z);

            if (index < 0)
            {
                coords.Add(item.coordinates);
            }
            else if (coords[index].y < item.coordinates.y)
            {
                coords[index] = item.coordinates;
            }
        }
        foreach (Vector3 c in coords.OrderBy(o => o.x).ThenBy(o => o.z))
        {
            Debug.Log(c);
        }
        return coords;
    }
}
