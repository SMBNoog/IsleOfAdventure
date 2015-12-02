using UnityEngine;
using System.Collections;


static class GameInfo {

    public enum Area { TutorialArea, World, Forest, Town, Castle }

    public static Area areaToTeleportTo = Area.World; // calling this everytime the game loads
                                                             // Need to make this defualt World
        
    public static string playerName;

    public static bool TutorialCompleted = false; // save as pref

    public static void setArea(Area area)
    {
        areaToTeleportTo = area;
    }

    public static string sceneToLoad
    {
        get
        {
            // Logic wrong here
            if (TutorialCompleted && areaToTeleportTo == Area.World)
                return "SceneLoader";
            else if (!TutorialCompleted)
                return "World";
            else
                return areaToTeleportTo + "";
        }
    }

    public static float PlayerMaxHP { get { return PlayerPrefs.GetFloat("MaxHP"); } set { PlayerPrefs.SetFloat("MaxHP", value); PlayerPrefs.Save(); } }
    public static float PlayerAtk { get { return PlayerPrefs.GetFloat("Atk"); } set { PlayerPrefs.SetFloat("Atk", value); PlayerPrefs.Save(); } }
    public static float PlayerDef { get { return PlayerPrefs.GetFloat("Def"); } set { PlayerPrefs.SetFloat("Def", value); PlayerPrefs.Save(); } }
    public static float PlayerSpeed { get { return PlayerPrefs.GetFloat("Speed"); } set { PlayerPrefs.SetFloat("Speed", value); PlayerPrefs.Save(); } }
    public static WeaponType CurrentWeapon { get { return (WeaponType)PlayerPrefs.GetInt("WeaponType"); } set { PlayerPrefs.SetInt("WeaponType", (int)value); PlayerPrefs.Save(); } }
    
}
