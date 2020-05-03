using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticFigure : Figure
{
    public GameObject goal;
    public GameObject goalPref;

    private void Start()
    {
        CreateGoal();
    }

    void CreateGoal()
    {
        goal = Instantiate(goalPref);

        goal.transform.SetParent(transform);
        goal.transform.localPosition = info.goalCoordinates;
    }
}