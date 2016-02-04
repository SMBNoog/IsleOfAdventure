using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public float timeLimit;
    public Slider slider;
    private GameObject player;
    private Player playerscript;

    void Start()
    {
        playerscript = FindObjectOfType<Player>();
        player = playerscript.gameObject;
    }

    void Update()
    {
        if (timeLimit > 0)
        {
            timeLimit -= Time.deltaTime;
            slider.value = timeLimit;
        }
        else
        {
            IAttributesManager attribute = Interface.Find<IAttributesManager>(player);
            if (attribute != null)
            {
                attribute.SaveAttributes(false);
                GameInfo.AreaToTeleportTo = GameInfo.Area.World;
                SceneManager.LoadScene("SceneLoader");
            }
        }
    }
}