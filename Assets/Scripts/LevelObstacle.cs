using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Level/Create")]
public class LevelObstacle : ScriptableObject
{
    [SerializeField] private Sprite levelSource;

    public Color32 GetColor(int w, int h)
    {
        return levelSource.texture.GetPixel(w, h);
    }

    public int ItemsCount()
    {
        if (levelSource)
        {
            return levelSource.texture.GetPixels32().Count(c=>c.a != 0);
        }

        return 0;
    }

    public bool IsNormalLevel()
    {
        if (levelSource == null)
        {
            return false;
        }

        return levelSource.texture.GetPixels32().Length == Constants.LevelSize;
    }
}
