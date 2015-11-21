using UnityEngine;
using System.Collections;

public class LandingPlatform : MonoBehaviour {
    
	void OnTriggerEnter2D(Collider2D other)
    {
        IAttacker attacker = Interface.Find<IAttacker>(other.gameObject);
        if(attacker != null)
        {
            other.gameObject.transform.SetParent(null);
        }
    }
}
