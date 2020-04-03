using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GateController : MonoBehaviour
{

    void OnTriggerEnter(Collider collide)
    {
       

        if(collide.gameObject.name=="Player")
        {
            Debug.Log("Hit gate destroying player object");
            Destroy(collide.gameObject);

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        }



    }

}
