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

    [SerializeField] private ParticleSystem transformation;

    private void Start()
    {
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

        transformation.Play();
    }

    void SetState(States newState)
    {
        SetObjEnabled(false);
        currentState = newState;
        SetObjEnabled(true);
    }

    void SetObjEnabled(bool setEnabled)
    {
        abilityDictionary[currentState].enabled = setEnabled;
    }
}
