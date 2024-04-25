using UnityEngine;

public class UnitModel
{
    public int Side => _side;
    public int Place => _place;

    private int _side;
    private int _place;
    [HideInInspector] public int hp;
    [HideInInspector] public int hpBase;
    [HideInInspector] public float hpProsent = 100;

}
