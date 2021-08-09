using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "CharacterVisual", order = 1)]
public class CharacterVisualSO : ScriptableObject
{

    [Header("Visual info")]
    public List<Material> SkinMaterial = new List<Material>();
    public List<Material> BodyMaterial = new List<Material>();


}
