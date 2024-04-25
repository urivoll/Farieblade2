using Spine;
using Spine.Unity;
using UnityEngine;

public class ModelPanel : MonoBehaviour
{
    public GameObject[] Skins;
    [SerializeField] private GameObject[] effects;
    private int CurrentSkin;
    public void SetSkin(int skin)
    {
        CurrentSkin = skin;
        if (effects.Length != 0)
        {
            for (int i = 0; i < effects.Length; i++)
            {
                effects[i].GetComponent<BoneFollower>().SkeletonRenderer = Skins[CurrentSkin].GetComponent<SkeletonRenderer>();
            }
        }
        for (int i = 0; i < Skins.Length; i++)
        {
            if (i != skin)
                Skins[i].SetActive(false);
            else
                Skins[i].SetActive(true);
        }

    }
}
