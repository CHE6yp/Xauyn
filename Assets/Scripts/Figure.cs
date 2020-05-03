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


    public List<Vector3> GetClearDown()
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

    public List<Vector3> GetClearUp()
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

    /// <summary>
    /// Метод выдает список координат для предметов, у которых нету соседнего предмета в сторону, которая пришла в аргументе
    /// </summary>
    /// <param name="dir">В какую сторону искать соседа</param>
    /// <returns></returns>
    public List<Vector3> GetClearDirection(Vector3 dir)
    {
        List<Vector3> coords = new List<Vector3>();
        foreach (Item item in items)
        {
            System.Predicate<Item> predicate = new System.Predicate<Item>(i => 
                i.coordinates.x == item.coordinates.x + dir.x && 
                i.coordinates.y == item.coordinates.y + dir.y && 
                i.coordinates.z == item.coordinates.z + dir.z
            );

            if (items.Find(predicate) == null)
            {
                coords.Add(item.coordinates + coordinates);
            }
        }
        return coords;
    }


    //Someday :'(
    public List<Vector3> GetClearDirection(System.Predicate<Vector3> predicate)
    {
        List<Vector3> coords = new List<Vector3>();
        foreach (Item item in items)
        {
            if (items.Find(i => i.coordinates.x == item.coordinates.x && i.coordinates.z == item.coordinates.z && i.coordinates.y == item.coordinates.y - 1) == null)
            {
                coords.Add(item.coordinates + coordinates);
            }
        }
        return coords;
    }
}
