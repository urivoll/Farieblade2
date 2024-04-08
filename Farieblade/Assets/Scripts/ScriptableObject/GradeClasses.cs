using UnityEngine;

[CreateAssetMenu(menuName = "SO/Grade Classes")]
public class GradeClasses : ScriptableObject
{
    [SerializeField] private string[] _name;
    [SerializeField] private Sprite _icon;
    [SerializeField] private int _id;
    [SerializeField] private Sprite _frame;
    [SerializeField] private Sprite _deck;
}
