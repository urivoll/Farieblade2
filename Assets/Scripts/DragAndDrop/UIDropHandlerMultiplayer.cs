using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;
public class UIDropHandlerMultiplayer : MonoBehaviour, IDropHandler
{
    [HideInInspector] public GameObject newObject = null;
    private MultiplayerSorting sorting;
    [SerializeField] private Transform content;
    [SerializeField] private int place;
    [SerializeField] private int side;
    private void Start()
    {
        sorting = GetComponentInParent<MultiplayerSorting>();
        MultiplayerDraft.exitMultiplayer += Exit;
    }
    private void OnDestroy()
    {
        MultiplayerDraft.exitMultiplayer -= Exit;
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag.name == "Card")
        {
            Unit unit = eventData.pointerDrag.transform.parent.GetComponent<Unit>();
            int stateOfunit = eventData.pointerDrag.transform.parent.GetComponent<Unit>().state;
            if (unit.level != 0 && newObject == null &&
                ((tag == "circleShooters" && (stateOfunit == 1 || stateOfunit == 3 || stateOfunit == 4 || stateOfunit == 2)) ||
                (tag == "circleMelee" && stateOfunit == 0)))
            {
                newObject = unit.gameObject;
                unit.transform.SetParent(gameObject.transform);
                transform.SetSiblingIndex(5);
                newObject.GetComponent<Animator>().SetTrigger("multi");
                StartIni.multiplayerDraft.unit[BattleNetwork.sideOnBattle] = newObject;
                StartIni.multiplayerDraft.blockCircle[BattleNetwork.sideOnBattle].SetActive(true);
                StartIni.multiplayerDraft.network.PickQuery(unit.ID, side, place, BattleNetwork.ident);
                StartIni.multiplayerDraft.blockContent.SetActive(true);
                int ran = Random.Range(0, 2);
                if (ran == 0) Sound.sound.PlayOneShot(StartIni.multiplayerDraft.put2);
                else Sound.sound.PlayOneShot(StartIni.multiplayerDraft.put1);
            }
            //Возвращение в предыдущий слот
            else
            {
                unit.transform.SetParent(eventData.pointerDrag.GetComponent<UIDragHandlerMultiplayer>()._previousParent);
                sorting.Sort();
            }
        }
    }
    private void Exit()
    {
        if (newObject != null)
        {
            newObject.transform.SetParent(content);
            newObject = null;
        }
    }
}