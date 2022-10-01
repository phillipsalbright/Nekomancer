using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Start()
    {
        foreach(Ability ability in GetComponents<Ability>())
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

    void SetState(States newState)
    {
        SetScriptEnabled(false);
        currentState = newState;
        SetScriptEnabled(true);
    }

    void SetScriptEnabled(bool setEnabled)
    {
        if (abilityDictionary.ContainsKey(currentState))
            abilityDictionary[currentState].enabled = setEnabled;
    }
}
