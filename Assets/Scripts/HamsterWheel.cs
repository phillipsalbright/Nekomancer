using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HamsterWheel : MonoBehaviour
{
    [SerializeField]
    private float runTime = 1f;

    [SerializeField]
    private List<Bridges> bridges = new List<Bridges>();

    private bool running = false;

    ZoomieCat zoomieCat;

    bool bridgesBuilt = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!bridgesBuilt && other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            zoomieCat = other.GetComponentInParent<ZoomieCat>();
            if (zoomieCat.enabled && zoomieCat.IsZooming())
            {
                if (!running)
                {
                    running = true;
                    StartCoroutine("RunOnWheel");
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!bridgesBuilt && other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (zoomieCat.enabled && zoomieCat.IsZooming())
            {
                if (!running)
                {
                    running = true;
                    StartCoroutine("RunOnWheel");
                }
            }
            else
            {
                running = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            running = false;
        }
    }

    public void SetBridgesBuilt(bool built)
    {
        bridgesBuilt = built;
    }

    IEnumerator RunOnWheel()
    {
        yield return new WaitForSeconds(runTime);
        if (running)
        {
            SetBridgesBuilt(true);
            foreach (Bridges bridge in bridges)
            {
                bridge.BuildBridge(this);
            }            
        }
    }
}
