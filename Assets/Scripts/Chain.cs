using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chain : MonoBehaviour
{
    public GameObject chainParent;
    public float distancePulled;
    public float pullDuration;

    private bool pulled;

    [SerializeField]
    List<GameObject> tapestries = new List<GameObject>();

    private void Start()
    {
        foreach (GameObject tapestry in tapestries)
        {
            tapestry.SetActive(false);
        }
    }

    public void PullChain()
    {
        if (!pulled)
        {
            pulled = true;
            StartCoroutine(SmoothLerp(pullDuration));
            // ADD OTHER CHAIN FUNCTIONALITY HERE
            foreach (GameObject tapestry in tapestries)
            {
                tapestry.SetActive(true);
            }
        }
    }

    private IEnumerator SmoothLerp(float time)
    {
        Vector3 startingPos = chainParent.transform.position;
        Vector3 finalPos = chainParent.transform.position - (chainParent.transform.up * distancePulled);
        float elapsedTime = 0;

        while (elapsedTime < time)
        {
            chainParent.transform.position = Vector3.Lerp(startingPos, finalPos, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
