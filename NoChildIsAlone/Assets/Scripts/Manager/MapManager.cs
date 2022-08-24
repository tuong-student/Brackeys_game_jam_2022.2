using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    #region Components
    [SerializeField] Transform Entry, Table;
    public List<Vector3> destinationList = new List<Vector3>();
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
            Person female = Person.CreateFemale(this.transform);
            male.isRequest = true;
            male.SetRequestName(name);
            female.SetName(name);
            male.destination = Table.position;
        }
        else
        {
            Person male = Person.CreateMale(this.transform);
            Person female = Person.CreateFemale(Entry);
            female.isRequest = true;
            female.SetRequestName(name);
            male.SetName(name);
            female.destination = Table.position;
        }
    }
}
