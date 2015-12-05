using UnityEngine;

static class GameInfo {

    public enum Area { TutorialArea, World, Forest, Town, Castle, MainMenu }
                        
    // Called by NPC's and Play Button
    public static string sceneLoader { get { return "SceneLoader"; } }

    public static string PlayerName {
        get { return PlayerPrefs.GetString("PlayerName"); }
        set { PlayerPrefs.SetString("PlayerName", value); } }
    public static bool StartTutorial {
        get { return PlayerPrefs.HasKey("TutorialCompleted") ? (PlayerPrefs.GetInt("TutorialCompleted") == 1 ? true : false) : false; }
        set { int b = value == true ? 1 : 0; PlayerPrefs.SetInt("TutorialCompleted", b); } }
    public static float PlayerMaxHP {
        get { return PlayerPrefs.GetFloat("MaxHP"); }
        set { PlayerPrefs.SetFloat("MaxHP", value); } }
    public static float PlayerAtk {
        get { return PlayerPrefs.GetFloat("Atk"); }
        set { PlayerPrefs.SetFloat("Atk", value); } }
    public static float PlayerDef {
        get { return PlayerPrefs.GetFloat("Def"); }
        set { PlayerPrefs.SetFloat("Def", value); } }
    public static float PlayerSpeed {
        get { return PlayerPrefs.GetFloat("Speed"); }
        set { PlayerPrefs.SetFloat("Speed", value); } }
    public static WeaponType CurrentWeapon {
        get { return (WeaponType)PlayerPrefs.GetInt("WeaponType"); }
        set { PlayerPrefs.SetInt("WeaponType", (int)value); } }
    public static Area? AreaToTeleportTo {
        get { return PlayerPrefs.HasKey("AreaToTeleportTo") ?
                (Area?)PlayerPrefs.GetInt("AreaToTeleportTo") : null; }
        set { PlayerPrefs.SetInt("AreaToTeleportTo", (int)value); } }
    public static Vector2 LastPos {
        get { return new Vector2(PlayerPrefs.GetFloat("LastPosX"), 
                                 PlayerPrefs.GetFloat("LastPosY")); }
        set { PlayerPrefs.SetFloat("LastPosX", value.x);
              PlayerPrefs.SetFloat("LastPosY", value.y);
              Debug.Log(value.x + "   " + value.y); } }
}
