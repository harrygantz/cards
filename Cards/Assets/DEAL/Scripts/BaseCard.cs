using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Cards/Base")]
public class BaseCard : ScriptableObject
{


    public new string name;
    public string type;
    public string description;

    public Sprite artwork;

    
}
