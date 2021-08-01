using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility;

public class NPCBehaviour : MonoBehaviour
{
    public Transform m_Target;
    private Rigidbody m_Rigidbody;
    public float m_MaxAngularVelocity = 10;

    private WaypointProgressTracker waypointScript;
    Vector3 originalPos;

    CapsuleCollider capsuleCol;

    private Renderer meshRenderer;
    public Material[] bodyMat;

    [SerializeField]
    [Range(0f, 10f)]
    float lerpTime;

    [SerializeField]
    [ColorUsage(false, true)]
    Color[] meshColors;
    int colorIndex = 0;
    float t = 0f;

    public bool spawned;
    private bool isKilled;
    private bool isIdle;


    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        meshRenderer = GetComponent<MeshRenderer>();
        bodyMat = meshRenderer.materials;

        waypointScript = GetComponent<WaypointProgressTracker>();
        originalPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
 
        spawned = false;
        isKilled = false;
        isIdle = true;

        capsuleCol = GetComponent<CapsuleCollider>();
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
            capsuleCol.enabled = false;
        }
    }

    void MoveAndBeVisible()
    {
        isIdle = false;
      
        m_Rigidbody.isKinematic = false;
        capsuleCol.enabled = true;

        waypointScript.enabled = true;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(m_Target.position - transform.position), 5 * Time.deltaTime);
        transform.position += transform.forward * Time.deltaTime * m_MaxAngularVelocity;
       
        transform.GetChild(1).gameObject.SetActive(true); 

        bodyMat[1].color = Color.Lerp(bodyMat[1].color, meshColors[colorIndex], lerpTime * Time.deltaTime);
        t = Mathf.Lerp(t, 1f, lerpTime * Time.deltaTime);
        if (t > .9f)
        {
            t = 0f;
            colorIndex++;
            colorIndex = (colorIndex >= meshColors.Length) ? 0 : colorIndex;
        }

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