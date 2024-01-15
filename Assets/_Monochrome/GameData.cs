using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObjects/GameData", order = 1)]
public class GameData : ScriptableObject
{
    [SerializeField] List<LevelData> levels;

    public List<LevelData> Levels { get => levels; set => levels = value; }
}
