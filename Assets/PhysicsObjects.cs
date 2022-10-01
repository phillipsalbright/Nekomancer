using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObjects : MonoBehaviour
{
    [SerializeField]
    List<GameObject> physicsObjects;
    [SerializeField]
    BoxCollider boxCollider;
    [SerializeField]
    ZoomieCat zoomieCatRef;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        zoomieCatRef = FindObjectOfType<ZoomieCat>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Check if zoomie cat is active
        // If zoomie cat is in trigger enable rigid body
        // else disable rigidbody
    }
}
