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
        direction = transform.localPosition;
    }

    public void Move()
    {
        figure.Move(direction);
    }
}
