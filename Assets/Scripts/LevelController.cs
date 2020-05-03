using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public static LevelController instance;
    public List<Level> levels;
    public MovingFigure movingFigure;
    public StaticFigure staticFigure;
    public int levelIndex = 0;

    private void Awake()
    {
        instance = this;
        NextLevel();
    }

    public void NextLevel()
    {
        movingFigure.CreateFigure(levels[levelIndex].movingInfo);
        staticFigure.CreateFigure(levels[levelIndex].staticInfo);
        levelIndex = (levelIndex == levels.Count - 1) ? 0 : levelIndex + 1;
    }

    [System.Serializable]
    public class Level
    {
        public FigureInfo staticInfo;
        public FigureInfo movingInfo;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            levelIndex = (levelIndex == 0) ? levels.Count - 1 : levelIndex-1;
            NextLevel();
        }
    }
}


