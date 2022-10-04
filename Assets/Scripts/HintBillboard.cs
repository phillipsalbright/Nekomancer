using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HintBillboard : MonoBehaviour
{
    public GameObject player;
    public TMP_Text textComponent;
    public Vector3 offset;

    public string defaultTxt = "'E' or 'Gamepad_Left'\n";

    public void SetText(string newText)
    {
        textComponent.text = defaultTxt + newText;
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position + offset;
    }

    private void FixedUpdate()
    {
        transform.LookAt(player.transform, Vector3.up);
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

}
