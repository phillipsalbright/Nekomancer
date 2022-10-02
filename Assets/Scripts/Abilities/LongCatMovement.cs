using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongCatMovement : PlayerMovement
{
    [SerializeField] CapsuleCollider cc;
    [SerializeField] GameObject paws;
    [SerializeField] GameObject kittyCat;

    bool extending = false;
    protected override void FixedUpdate()
    {
        bool jumpPressedLocal = jumpPressed;
        jumpPressed = false;
        base.FixedUpdate();
        jumpPressed = jumpPressedLocal;
        if (jumpPressed)
        {
            if (!extending)
            {
                audioManager.clip = activeClip;
                audioManager.loop = true;
                audioManager.Play();
                extending = true;
            }
            if (cc.height < 8)
            {
                cc.height += .1f;
                //capsule.transform.localScale = new Vector3(1, cc.height / 2, 1);

                anim.SetFloat("Blend", (cc.height - 2) / 6);
                kittyCat.transform.localPosition = new Vector3(0, .11f - cc.height / 2 + 1, 0);
                //paws.transform.localPosition = new Vector3(paws.transform.localPosition.x, cc.transform.localPosition.y + cc.height / 2 - .5f, paws.transform.localPosition.z);
            }
        } else
        {
            if (extending)
            {
                audioManager.Stop();
                extending = false;
            }
            if (cc.height > 2)
            {
                Vector3 newPos = new Vector3(cc.transform.localPosition.x, cc.transform.localPosition.y + cc.height / 2, cc.transform.localPosition.z);
                cc.height = 2;
                //capsule.transform.localScale = new Vector3(1, cc.height / 2, 1);
                anim.SetFloat("Blend", (cc.height - 2) / 6);
                kittyCat.transform.localPosition = new Vector3(0, .11f - cc.height / 2 + 1, 0);
                //cc.gameObject.transform.localPosition = newPos;
                //capsule.gameObject.transform.localPosition = newPos;
                this.transform.position += newPos;
                RaycastHit hit;
                Ray ray = new Ray(paws.transform.position, Vector3.down);
                paws.transform.localPosition = new Vector3(paws.transform.localPosition.x, cc.transform.localPosition.y + cc.height / 2 - .5f, paws.transform.localPosition.z);
                if (Physics.Raycast(ray, out hit) && hit.distance < 7) {
                    //Debug.Log(hit.distance);
                    playerRB.AddForce(this.transform.forward * 5, ForceMode.Impulse);
                }
            }
        }
    }
}
