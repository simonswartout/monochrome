using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CreditData", menuName = "ScriptableObjects/CreditData", order = 1)]
public class CreditData : ScriptableObject 
{
    [SerializeField] List<CreditEntry> creditEntries;

    public List<CreditEntry> CreditEntries { get { return creditEntries; } }
}
