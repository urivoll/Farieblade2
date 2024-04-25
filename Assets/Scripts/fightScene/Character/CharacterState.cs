using UnityEngine;
using Zenject;

public class CharacterState
{
    public bool paralize => _paralize;
    public bool went => _went;
    public bool allowHit => _allowHit;
    public bool silence => _silence;

    private bool _paralize = false;
    private bool _went = false;
    private bool _allowHit = false;
    private bool _silence = false;
    private UnitProperties _unitProperties;

    [Inject] private Turns _turns;

    public CharacterState(UnitProperties unitProperties)
    {
        _unitProperties = unitProperties;
        if(_turns != null)
        _turns.TurnOver += TurnOver;
    }

    private void TurnOver(UnitProperties unitProperties)
    {
        if (unitProperties != _unitProperties) return;
        _paralize = false;
        _went = false;
    }

    public void MakeStep(bool step)
    {
        _went = step;
    }

    public void ChangeAllowHit(bool allow)
    {
        _allowHit = allow;
    }

    public void ChangeSilence(bool silence)
    {
        _silence = silence;
    }

    public void ChangeParalize(bool paralize)
    {
        _paralize = paralize;
    }

}
