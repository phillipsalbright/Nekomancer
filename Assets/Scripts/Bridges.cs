using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridges : MonoBehaviour
{
    bool bridgeBuilt = false;
    [SerializeField]
    float bridgeDestructionTime = .7f;

    HamsterWheel wheel;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void BuildBridge(HamsterWheel hamsterWheel)
    {
        wheel = hamsterWheel;
        gameObject.SetActive(true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            StartCoroutine("TearDownBridge");
            bridgeBuilt = true;
        }
    }

    IEnumerator TearDownBridge()
    {
        yield return new WaitForSeconds(bridgeDestructionTime);
        DestroyBridge();
    }

    void DestroyBridge()
    {
        wheel.SetBridgesBuilt(false);
        bridgeBuilt = false;
        gameObject.SetActive(false);

        StartCoroutine(RespawnTimer(5f));
    }

    private IEnumerator RespawnTimer(float time)
    {
        float elapsedTime = 0;

        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        gameObject.SetActive(true);
    }
}
