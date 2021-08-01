using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushAtStart : MonoBehaviour
{
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