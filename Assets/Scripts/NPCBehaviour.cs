using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility;

public class NPCBehaviour : MonoBehaviour
{
   // public GameObject self;
    public Transform m_Target;
    private Rigidbody m_Rigidbody;
    public float m_MaxAngularVelocity = 10;

    private Renderer meshRenderer;

    private WaypointProgressTracker waypointScript;

    // Angular speed in radians per sec.
    public float speed = 1.0f;

    Vector3 originalPos;

   
    public bool spawned;
    private bool isKilled;
    private bool isIdle;

    // Use this for initialization
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        meshRenderer = GetComponent<MeshRenderer>();
        waypointScript = GetComponent<WaypointProgressTracker>();
        originalPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
 
        spawned = false;
        isKilled = false;
        isIdle = true;
    }

    void Update()
    {
        Idle();

        if (spawned)
        {
            MoveAndBeVisible();
        }

        if (isKilled)
        {
            ResetAndBeHidden();
        }

    }

    public void SetTarget(Transform WaypointTargetObject)
    {
        m_Target = WaypointTargetObject;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "KillNpc")
        {
            isKilled = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        
        if (other.gameObject.tag == "KillNpc")
        {
            spawned = false;
            isIdle = true;
        }
    }
    void Idle()
    {
        if (isIdle)
        {
        meshRenderer.enabled = false;
        waypointScript.enabled = false;
        m_Rigidbody.isKinematic = true;
        transform.GetChild(1).gameObject.SetActive(false);
        }
    }

    void MoveAndBeVisible()
    {
        isIdle = false;
      
        m_Rigidbody.isKinematic = false;
     
        meshRenderer.enabled = true;
        waypointScript.enabled = true;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(m_Target.position - transform.position), 5 * Time.deltaTime);
        transform.position += transform.forward * Time.deltaTime * m_MaxAngularVelocity;
        transform.GetChild(1).gameObject.SetActive(true);
    }

    void ResetAndBeHidden()
    {
        isIdle = true;

        meshRenderer.enabled = false;
        waypointScript.enabled = false;

        transform.position = originalPos;

        isKilled = false;
        spawned = false;
        transform.GetChild(1).gameObject.SetActive(false);
    }
}