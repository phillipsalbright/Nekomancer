using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollyCat : PlayerMovement
{
    [SerializeField]
    private GameObject catVisuals;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}