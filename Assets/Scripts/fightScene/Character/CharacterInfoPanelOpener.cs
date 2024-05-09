using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class CharacterInfoPanelOpener : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Coroutine workCoroutine;
    private bool workProperties = false;
    private UnitProperties _unitProperties;
    [Inject] private AbstractPanelProperties _panelProperties;
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

        Dictionary<string, int> data = new();
        data["id"] = _unitProperties.Id;
        data["level"] = _unitProperties.Level;
        data["grade"] = _unitProperties.Grade;
        data["damageFight"] = _unitProperties.Weapon.Damage;
        data["hpFight"] = _unitProperties.HpCharacter.Hp;
        _panelProperties.gameObject.SetActive(true);
        _panelProperties.GetComponent<PanelPropertiesFight>().SetValue(data);
    }
    private void Update()
    {
        if (workProperties == true) 
            StartIni.circleProperties.fillAmount = Mathf.Lerp(StartIni.circleProperties.fillAmount, 1, 8f * Time.deltaTime);
    }
}
