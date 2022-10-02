using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HintBillboard : MonoBehaviour
{
    public GameObject player;
    public TMP_Text textComponent;
    public Vector3 offset;

    public void SetText(string newText)
    {
        textComponent.text = newText;
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position + offset;
    }

    private void FixedUpdate()
    {
        transform.LookAt(player.transform, Vector3.up);
    }

}
