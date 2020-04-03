using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollisionController : MonoBehaviour
{
    private bool hasHit = false;
    public Text text;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player" && !hasHit)
        {
            hasHit = true;
            PlayerManager.instance.Score(200);
            text.gameObject.SetActive(true);
            StartCoroutine(Wait());
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3.0f);
        text.gameObject.SetActive(false);
    }
}
