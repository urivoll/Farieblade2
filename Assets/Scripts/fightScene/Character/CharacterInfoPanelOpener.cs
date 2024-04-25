using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterInfoPanelOpener : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Coroutine workCoroutine;
    private bool workProperties = false;
    private UnitProperties _unitProperties;
    private void Start()
    {
        _unitProperties = GetComponent<UnitProperties>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Turns.unitChoose = _unitProperties;
        workCoroutine = StartCoroutine(PointDown());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (workCoroutine == null) return;
        StopCoroutine(workCoroutine);
        StartIni.circleProperties.gameObject.SetActive(false);
        workProperties = false;
    }

    private IEnumerator PointDown()
    {
        yield return new WaitForSeconds(0.2f);
        workProperties = true;
        StartIni.circleProperties.gameObject.SetActive(true);
        StartIni.circleProperties.transform.position = _unitProperties.PathBulletTarget.position;
        StartIni.circleProperties.fillAmount = 0;
        yield return new WaitForSeconds(0.3f);
        workProperties = false;
        StartIni.circleProperties.gameObject.SetActive(false);
        workCoroutine = null;
        StartIni.unitProperties.SetActive(true);
        StartIni.unitProperties.GetComponent<PanelPropertiesFight>().SetValue(_unitProperties);
    }
    private void Update()
    {
        if (workProperties == true) 
            StartIni.circleProperties.fillAmount = Mathf.Lerp(StartIni.circleProperties.fillAmount, 1, 8f * Time.deltaTime);
    }
}
