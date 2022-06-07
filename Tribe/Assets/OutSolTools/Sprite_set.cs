using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class Sprite_set : ScriptableObject
{
    public Sprite[] SPRITES;
    public int repeat_times; // -1 for infinity
}
