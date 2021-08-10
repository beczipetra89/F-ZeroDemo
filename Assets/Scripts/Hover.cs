using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour
{
    public Animator hoverAnim;

    void Update()
    {
        if (!Input.GetKey(KeyCode.UpArrow)) 
        {
            hoverAnim.SetFloat("Direction", 1);
            hoverAnim.SetTrigger("sinkDown");

        }
        
        if (Input.GetKey(KeyCode.UpArrow))
        {
            hoverAnim.SetFloat("Direction", -1);
            hoverAnim.SetTrigger("raiseUp");
        }
    }
}
