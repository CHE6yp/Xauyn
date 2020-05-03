using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Item : MonoBehaviour
{
    public Vector3 coordinates;
    public List<Material> materials; 
    // Start is called before the first frame update
    void Awake()
    {
        Vector3[] c = new Vector3[5];
        c[0] = new Vector3(0, 0, 0);
        c[1] = new Vector3(241, 42, 81);
        c[2] = new Vector3(171, 24, 57);
        c[3] = new Vector3(87, 10, 47);
        c[4] = new Vector3(48, 10, 43);

        Vector3 ct = c[Random.Range(0, 5)];
        Debug.Log(ct);
        //GetComponent<MeshRenderer>().material.SetColor("_Color", new Color(ct.x, ct.y, ct.z));
        GetComponent<MeshRenderer>().material = materials[Random.Range(1, 5)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
