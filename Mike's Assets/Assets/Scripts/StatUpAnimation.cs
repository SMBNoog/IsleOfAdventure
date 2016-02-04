using UnityEngine;
using System.Collections;

public class StatUpAnimation : MonoBehaviour {

    private bool timer = true;

    void OnEnable()
    {
        transform.localPosition = new Vector2(0, 0);
        StartCoroutine(Heart());
        StartCoroutine(Timer());

        Destroy(gameObject, 2f);        
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(1f);
        timer = false;
    }

    IEnumerator Heart()
    {
        while (timer)
        {
            float r = Random.Range(1, 4);
            switch ((int)r)
            {
                case 1: transform.position += Vector3.up * 1.8f * Time.deltaTime; break;
                case 2: transform.position += ((Vector3.up * 1.8f) + Vector3.left * 1.5f) * Time.deltaTime; break;
                case 3: transform.position += ((Vector3.up * 1.8f) + Vector3.right * 1.5f) * Time.deltaTime; break;
                default: break;
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
