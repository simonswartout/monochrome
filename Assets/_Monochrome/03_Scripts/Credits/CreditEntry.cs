using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "CreditEntry", menuName = "ScriptableObjects/CreditEntry", order = 1)]
public class CreditEntry : ScriptableObject
{
    [SerializeField] private string name;
    [SerializeField] private string attribution;
    [SerializeField] private string url;

    public string Name { get { return name; } }
    public string Attribution { get { return attribution; } }
    public string Url { get { return url; } }


}
