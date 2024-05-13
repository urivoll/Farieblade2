using UnityEngine;
using Zenject;

public class CharacterState : MonoBehaviour
{
    public bool Paralize => _paralize;
    public bool Went => _went;
    public bool AllowHit => _allowHit;
    public bool Silence => _silence;

    private bool _paralize = false;
    private bool _went = false;
    private bool _allowHit = false;
    private bool _silence = false;
    private UnitProperties _unitProperties;

    [Inject] private Turns _turns;

    public void Init(UnitProperties unitProperties)
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
