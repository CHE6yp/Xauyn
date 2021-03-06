﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "FigureData", menuName = "ScriptableObjects/FigureInfo", order = 1)]
public class FigureInfo : ScriptableObject
{
    public Vector3 coordinates;

    public Vector3 goalCoordinates;
    public List<Vector3> items;
}
