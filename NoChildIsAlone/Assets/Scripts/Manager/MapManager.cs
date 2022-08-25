using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    #region Components
    [SerializeField] Transform Entry, TableTransform;
    public List<Transform> destinationList = new List<Transform>();
    List<string> nameList = new List<string>();
    #endregion

    #region Stats
    #endregion

    public static MapManager InsMapManager;

    private void Awake()
    {
        if (InsMapManager == null) InsMapManager = this;
    }

    private IEnumerator Start()
    {
        nameList = PersonReader.GetNames();
        CreatePeople();
        yield return new WaitForSeconds(2f);
        CreatePeople();
    }

    private void Update()
    {
        
    }

    public void CreatePeople()
    {
        string name = PersonReader.GetRandomName();
        if(Random.Range(0, 2) < 1)
        {
            Person male = Person.CreateMale(Entry);
            Person female = Person.CreateFemale();
            male.isRequest = true;
            male.SetRequestName(name);
            female.SetName(name);
            male.destination = TableTransform.position;
        }
        else
        {
            Person male = Person.CreateMale();
            Person female = Person.CreateFemale(Entry);
            female.isRequest = true;
            female.SetRequestName(name);
            male.SetName(name);
            female.destination = TableTransform.position;
        }
    }
}
