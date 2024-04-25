using UnityEngine;
using UnityEngine.UI;
public class SkillSlot : MonoBehaviour
{
    public GameObject FrameActive;
    public Image picActive;
    public GameObject FramePassive;
    public Image picPassive;
    public GameObject FrameAura;
    public Image picAura;
    public void Off()
    {
        FrameActive.SetActive(false);
        FrameAura.SetActive(false);
        FramePassive.SetActive(false);
        gameObject.SetActive(false);
    }
}
