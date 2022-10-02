using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] ParticleSystem p;
    [SerializeField] Animator a;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.y < -20)
        {
            p.Play();
            if (a)
            {
                a.SetBool("growing", true);
                this.enabled = false;
            }
        }
    }
}
