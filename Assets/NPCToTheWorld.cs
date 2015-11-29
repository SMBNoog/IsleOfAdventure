using UnityEngine;
using System.Collections;

public enum NPCTo { Forest, Castle, World, SceneLoader }

public class NPCToTheWorld : MonoBehaviour {

    public NPCTo NPCTo;

	void OnTriggerEnter2D(Collider2D other)
    {
        IAttributesManager attributes = Interface.Find<IAttributesManager>(other.gameObject);
        if (attributes != null)
        { 
            attributes.SaveAttributes();

            switch(NPCTo)
            {
                case NPCTo.World: GameInfo.setArea(GameInfo.Area.World); GameInfo.TutorialCompleted = true; break;
                case NPCTo.Forest: GameInfo.setArea(GameInfo.Area.Forest); break;
                case NPCTo.Castle: GameInfo.setArea(GameInfo.Area.Castle); break;
                case NPCTo.SceneLoader: break;
            }
            Application.LoadLevel("SceneLoader");            
        }
    }
}
