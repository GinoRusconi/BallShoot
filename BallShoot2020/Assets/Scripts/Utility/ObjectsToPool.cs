using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ObjectToPool", menuName = "ScriptableObjects/ObjectToPool", order = 1)]
public class ObjectsToPool : ScriptableObject
{
    public string tag;
    public GameObject prefabObject;
    public int sizePool;
   

}
