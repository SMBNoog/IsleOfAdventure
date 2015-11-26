using UnityEngine;
using System.Collections;

static class GameInfo {

    public enum Area { TutorialArea, World, Forest, Town, Castle }

    private static Area currentArea = Area.World;

    public static string sceneToLoad { get; set; }

    static bool TutorialCompleted = false;

    public static void setArea(Area area)
    {
        currentArea = area;
    }



    //public static float PlayerHP { get { return PlayerPrefs.asdfklsdfakljadfs; } set { playladfsjfdasjdfs; }}
}
