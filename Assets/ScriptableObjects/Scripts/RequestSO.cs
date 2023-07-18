using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newRequest", menuName = "ScriptableObjects/Request", order = 1)]
public class RequestSO : ScriptableObject
{
    public List<Items> itemList;
}
