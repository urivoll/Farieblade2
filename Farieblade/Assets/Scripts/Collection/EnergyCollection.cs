using UnityEngine;

public class EnergyCollection : MonoBehaviour
{
    private GameObject[] units;
    [SerializeField] private Sorting sort;
    private void Awake()
    {
        //units = sort.units;
    }
    private void OnEnable()
    {
        foreach (GameObject item in units) 
        {

        }
    }
}
