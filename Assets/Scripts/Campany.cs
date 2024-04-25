using UnityEngine;
public class Campany : MonoBehaviour
{
    public static int[] enemy = new int[6];
    public static int[] enemyLevel = new int[6];
    public static int[] enemyGrade = new int[6];

    public static int battleField = 1;
    public static int campanyProgress;
    public static int currentPlace;
    public static int enemyGrades;
    public static int anIsProgress;
    public static int anIsCurrentPlace;
    public static int[] anIsTimeStart = new int[3];
    public static int[] anIsTimeNeed = new int[3];
    public static int[] anIsLocation = new int[3];
    public static int[] anIsProsent = new int[3];
    public static string enemyNick;
    public static int enemyPortraitInt;
    public static Sprite enemyPortraitSprite;
    public static bool changeProgress = false;
}
