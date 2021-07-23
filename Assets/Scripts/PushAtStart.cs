using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushAtStart : MonoBehaviour
{
    /* void Start()
     {
         // Give a boost kick at start
         gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX; ;

         gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * 40000000); 
     }

     void Update()
     {
         StartCoroutine(UnfreezeRotation());
     }

     private IEnumerator UnfreezeRotation()
     {
         yield return new WaitForSeconds(2);
         gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
     }*/

    public float boostSpeed;
    void Start()
    {
        gameObject.GetComponent<Rigidbody>().velocity = -Vector3.right * boostSpeed;
    }

    void Update()
    {
        StartCoroutine(Disable());
    }

    private IEnumerator Disable()
    {
        yield return new WaitForSeconds(1);
        gameObject.GetComponent<PushAtStart>().enabled = false;
    }
}