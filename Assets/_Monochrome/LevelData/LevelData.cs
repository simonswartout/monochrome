using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/LevelData", order = 1)]
public class LevelData : ScriptableObject
{
    [SerializeField] string levelName;
    [SerializeField] int levelNumber;
    [SerializeField] GameObject levelPrefab;
}
