using System.Collections;
using UnityEngine;
public class MultiplayerSearch : MonoBehaviour
{
    [SerializeField] private GameObject buttonCancel;
    [HideInInspector]public Coroutine search;
    [SerializeField] private GameObject multiplayerFight;
    [SerializeField] private GameObject particle;
    [SerializeField] private MusicMainMenu menu;
    private void OnEnable()
    {
        menu.Stop();
        search = StartCoroutine(SearchAsync());
    }
    private IEnumerator SearchAsync()
    {
        yield return new WaitForSeconds(0.5f);
        buttonCancel.SetActive(true);
    }
}
