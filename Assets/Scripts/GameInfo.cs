using UnityEngine;
using System.Collections;


static class GameInfo {

    public enum Area { TutorialArea, World, Forest, Town, Castle }

    public static Area currentArea = Area.TutorialArea;

    public static string sceneToLoad { get { return sceneToLoad; } set { sceneToLoad = currentArea + ""; } }

    public static string playerName;

    public static bool TutorialCompleted = false;

    public static void setArea(Area area)
    {
        currentArea = area;
    }
        
    public static float PlayerMaxHP { get { return PlayerPrefs.GetFloat("MaxHP"); } set { PlayerPrefs.SetFloat("MaxHP", value); } }
    public static float PlayerAtk { get { return PlayerPrefs.GetFloat("Atk"); } set { PlayerPrefs.SetFloat("Atk", value); } }
    public static float PlayerDef { get { return PlayerPrefs.GetFloat("Def"); } set { PlayerPrefs.SetFloat("Def", value); } }
    public static float PlayerSpeed { get { return PlayerPrefs.GetFloat("Speed"); } set { PlayerPrefs.SetFloat("Speed", value); } }
    public static WeaponType CurrentWeapon { get { return (WeaponType)PlayerPrefs.GetInt("WeaponType"); } set { PlayerPrefs.SetInt("WeaponType", (int)value); } }
    
}
