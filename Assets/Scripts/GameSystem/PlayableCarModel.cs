using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewCarData", menuName = "Car/Create New Car Data", order = 1)]
public class PlayableCarModel : ScriptableObject
{
    public int Id;
    public string carName;
    public string carDescription;
    public GameObject prefab;
    public Sprite sprite;
    public int price;
    public bool unlocked;
}
