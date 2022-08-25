using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    public Person requestPerson, applyPerson;

    public static Table InsTable;

    private void Awake()
    {
        if (InsTable == null) InsTable = this;
    }

    public void SetApplyPerson(Person person)
    {
        applyPerson = person;
        if (CheckPerson())
        {
            Debug.Log("True");
            requestPerson.isFinish = true;
            applyPerson.isFinish = true;
            applyPerson.followPerson = requestPerson;
        }
    }

    public bool CheckPerson()
    {
        if(applyPerson.IsMale != requestPerson.IsMale)
        {
            if (requestPerson.requestName.Equals(applyPerson.name))
            {
                return true;
            }
        }
        return false;   
    }
}
