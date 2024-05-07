using UnityEngine;

[CreateAssetMenu(menuName = "SO/ExpNeeder")]
public class ExpNeeder : ScriptableObject
{
    public int[] Exp => _exp;

    [SerializeField] private int[] _exp;
}
