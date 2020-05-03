using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureDirection : MonoBehaviour
{
    public Vector3 direction;
    public MovingFigure figure;

    private void Awake()
    {
        figure = transform.parent.parent.GetComponent<MovingFigure>();
    }

    public void Move()
    {
        if (direction == Vector3.up)
        {
            figure.GoUp();
        }
        if (direction == Vector3.down)
        {
            figure.GoDown();
        }
    }
}
