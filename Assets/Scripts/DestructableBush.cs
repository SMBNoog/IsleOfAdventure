using UnityEngine;
using System.Collections;

public class DestructableBush : MonoBehaviour
{

    public GameObject leaves;

    void OnCollisionEnter2D(Collision2D other)
    {
        IWeapon weapon = Interface.Find<IWeapon>(other.collider.gameObject);
        if(weapon != null)
        {  
            leaves.gameObject.SetActive(true);
            leaves.transform.SetParent(null);            
            ////IBushDie bush = Interface.Find<IBushDie>(gameObject);
            ////if (bush != null)
            ////{
            ////    bush.Die();
            ////}
            Destroy(gameObject);
        }
    }    
}
