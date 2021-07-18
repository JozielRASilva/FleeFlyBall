using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "CharacterStatus", order = 1)]
public class CharacterStatusSO : ScriptableObject
{

    #region  Consts
    public const int MIN_STATUS = 2;
    public const int MAX_STATUS = 10;
    #endregion

    public string Name;


    #region Status

    [Range(MIN_STATUS, MAX_STATUS), SerializeField]
    private int balance = 5;
    public int Balance { get => balance; }



    [Range(MIN_STATUS, MAX_STATUS), SerializeField]
    private int shoot = 5;
    public int Shoot { get => shoot; }


    [Range(MIN_STATUS, MAX_STATUS), SerializeField]
    private int pass = 5;
    public int Pass { get => pass; }


    [Range(MIN_STATUS, MAX_STATUS), SerializeField]
    private int speed = 5;
    public int Speed { get => speed; }

    #endregion
}
