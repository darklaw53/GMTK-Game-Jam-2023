using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newItem", menuName = "ScriptableObjects/Item", order = 1)]
public class Items : ScriptableObject
{
    public string itemName;
    public Sprite sprite;
}
