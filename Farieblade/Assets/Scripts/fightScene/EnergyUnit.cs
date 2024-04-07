using UnityEngine;

public class EnergyUnit : MonoBehaviour
{
    [SerializeField] private GameObject[] energyArray;
    public int energy = 0;
    public int how;
    public char sign;
    private void Awake()
    {
        Buffer();
        Turns.newTurn += Buffer;
    }
    public void SetEnergy(int energy)
    {
        if (this.energy > energy)
        {
            how = this.energy - energy;
            sign = '-';
        }
        else if (this.energy < energy)
        {
            how = energy - this.energy;
            sign = '+';
        }
        else return;
        this.energy = energy;
        Turns.getEnergy?.Invoke(how, transform.parent.parent.parent.gameObject, sign);
        GetComponent<Animator>().SetTrigger("use");
        for (int i = 0; i < energyArray.Length; i++)
            energyArray[i].SetActive(false);
        for (int i = 0; i < this.energy; i++)
            energyArray[i].SetActive(true);
    }
    private void Buffer()
    {
        if (transform.parent.transform.parent.transform.parent.gameObject.GetComponent<Unit>().state == 4 ||
            transform.parent.transform.parent.transform.parent.gameObject.GetComponent<Unit>().state == 3)
        {
            SetEnergy(energy + 1);
        }
    }
    private void OnDestroy()
    {
        Turns.newTurn -= Buffer;
    }
}