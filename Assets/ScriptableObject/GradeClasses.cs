using UnityEngine;

[CreateAssetMenu(menuName = "SO/Grade Classes")]
public class GradeClasses : ScriptableObject
{
    public string[] Name => _name;
    public Sprite Icon => _icon;
    public int Id => _id;
    public Sprite Frame => _frame;
    public Sprite Deck => _deck;

    [SerializeField] private string[] _name;
    [SerializeField] private Sprite _icon;
    [SerializeField] private int _id;
    [SerializeField] private Sprite _frame;
    [SerializeField] private Sprite _deck;
}
