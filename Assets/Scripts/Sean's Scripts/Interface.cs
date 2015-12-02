using UnityEngine;
using System.Collections;
using System;

public enum Team { Player, Enemy }
public enum WellBeingState { Alive, Dead }
public enum ActionState { Idle, Patrolling, EngagedInBattle, AggroByEnemy, ChargeAtPlayer, Running }
public enum TypeOfStatIncrease { HP, ATK, DEF }
public enum WeaponType { Wooden, Bronze, Silver, Gold, Epic, Any }


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

public interface IAttributesManager
{
    void LoadAttributes();
    void SaveAttributes();
}

public interface IMessageDelegate
{
    void OnClickOK();
    void OnClickCancel();
    void ShowMessage(string dialogMessage, string okButton, string cancelButton, Dialogue.DialogueDelegate onClickOK);
}

public interface INPCMessageAndAction
{
    string DialogMessage { get; }

    void OnClickOK();
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
