using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CatState : MonoBehaviour
{
    public enum States
    {
        Zombie,
        Zoomie,
        LongCat,
        RollyCat,
    }
    public States currentState = States.Zombie;

    Dictionary<string, States> stateDictionary = new Dictionary<string, States>();

    Dictionary<States, Ability> abilityDictionary = new Dictionary<States, Ability>();

    public GameObject finalForm;

    AudioSource audioManager;

    [SerializeField]
    AudioClip switchSound;

    private void Start()
    {
        audioManager = GetComponent<AudioSource>();
        foreach(Ability ability in GetComponentsInChildren<Ability>())
        {
            stateDictionary.Add(ability.GetStringRep(), ability.GetState());
            abilityDictionary.Add(ability.GetState(), ability);
        }
    }

    public void ApplyPotion(string potion)
    {
        if (stateDictionary.ContainsKey(potion))
            SetState(stateDictionary[potion]);
        else
            SetState(States.Zombie);
    }

    public void FinalState()
    {
        SetObjEnabled(false);
        finalForm.SetActive(true);
        StartCoroutine("EndGame");
    }

    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(2);
    }

    void SetState(States newState)
    {
        if (newState == currentState)
            return;
        audioManager.clip = switchSound;
        audioManager.loop = false;
        audioManager.Play();
        SetObjEnabled(false);
        currentState = newState;
        SetObjEnabled(true);
    }

    void SetObjEnabled(bool setEnabled)
    {
        abilityDictionary[currentState].enabled = setEnabled;
    }
}
