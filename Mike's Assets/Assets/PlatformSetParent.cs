using UnityEngine;
using System.Collections;

public class PlatformSetParent : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        IAttacker attacker = Interface.Find<IAttacker>(other.gameObject);
        if (attacker != null)
        {
            //if (other.gameObject.transform.parent != null)
            //    other.gameObject.transform.parent = null;

            other.gameObject.transform.parent = this.transform;
        }
    }
}
