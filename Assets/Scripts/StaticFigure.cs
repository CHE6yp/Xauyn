using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticFigure : Figure
{
    public GameObject goal;
    public GameObject goalPref;

    private void Start()
    {
        //CreateGoal();
    }

    public override void CreateFigure(FigureInfo i)
    {
        ClearGoal();
        base.CreateFigure(i);
        CreateGoal();
    }

    void CreateGoal()
    {
        goal = Instantiate(goalPref);

        goal.transform.SetParent(transform);
        goal.transform.localPosition = info.goalCoordinates;
    }

    void ClearGoal()
    {
        Destroy(goal);
    }
}