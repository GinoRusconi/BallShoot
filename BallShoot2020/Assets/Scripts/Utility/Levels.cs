using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/Levels", order = 1)]
public class Levels : ScriptableObject
{
    public int Level;
    public GameObject[] Enemys;
    public int[] CountEnemy;
}
