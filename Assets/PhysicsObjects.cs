using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObjects : MonoBehaviour
{
    [SerializeField] List<GameObject> physicsObjects;
    [SerializeField] BoxCollider boxCollider;
    [SerializeField] ZoomieCat zoomieCatRef;
    [SerializeField] bool isInteractable = false;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        zoomieCatRef = FindObjectOfType<ZoomieCat>();
    }

    private void Update()
    {
        // 
        if (zoomieCatRef.IsInTrigger() && zoomieCatRef.enabled && zoomieCatRef.IsZooming())
        {
            isInteractable = true;
            foreach (var item in physicsObjects)
            {
                item.GetComponent<Rigidbody>().isKinematic = false;

            }
        }
        else
        {

            isInteractable = false;
            //foreach (var item in physicsObjects)
            //{
            //    item.GetComponent<Rigidbody>().isKinematic = true;
            //}
        }
    }

    
}
