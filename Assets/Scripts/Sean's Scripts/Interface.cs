using UnityEngine;
using System.Collections;

public enum Team { Player, Enemy }
public enum WellBeingState { Alive, Dead }
public enum ActionState { Idle, Patrolling, EngagedInBattle, AggroByEnemy, ChargeAtPlayer, RunningAway }
public enum TypeOfStatIncrease { HP, ATK, DEF }
public enum WeaponType { Wooden, Bronze, Silver, Gold, Epic }


public interface IAttacker
{
    WellBeingState wellBeing { get; set; }
    ActionState actionState { get; set; }
    TypeOfStatIncrease typeOfStatIncrease { get; set; }
    Team Team { get; }
    Vector2 Pos { get; }
    float Atk { get; set; }
}

public interface IWeapon
{
    float Atk { get; set; }
    float Def { get; set; }
    WeaponType WeaponType { get; set; }
}

public interface ISpawner
{
    void Died(Enemy enemy);
}

public interface IPlayerCurrentWeapon
{
    WeaponType weaponType { get; }
}

//public interface IPlayerAccumulatedHP
//{
//    float a_HP { get; set; }
//}

public interface IAttributesManager
{
    void LoadAttributes();
    void SaveAttributes();
}

public interface INPCMessage
{
    string message { get; }

    void OnClickOK();

    void OnTriggerEnter2D(Collider2D other);
}

public static class Interface
{
    public static T Find<T>(GameObject gameObject, bool debug = false) where T : class
    {
        foreach (var component in gameObject.GetComponents<MonoBehaviour>())
        {
            if (debug)
                Debug.Log("Component is " + component.GetType().Name + ", enabled: " + component.isActiveAndEnabled);
            if (component.isActiveAndEnabled && component is T)
            {
                return component as T;
            }
        }
        return null;
    }
    
}
