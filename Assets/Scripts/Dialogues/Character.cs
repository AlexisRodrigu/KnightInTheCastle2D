using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute(fileName = "Character", menuName = "Dialogue System / New Character", order = 0)]
public class Character : ScriptableObject
{
    public string nameCharacter;
    public Sprite imgSprite;
}
