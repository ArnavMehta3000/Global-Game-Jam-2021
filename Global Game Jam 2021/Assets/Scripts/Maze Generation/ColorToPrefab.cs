using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable] [CreateAssetMenu(fileName = "Maze", menuName = "New Maze")
    ]
public class ColorToPrefab : ScriptableObject
{
    public string prefabName;
    public Color color;
    public GameObject prefab;
}
