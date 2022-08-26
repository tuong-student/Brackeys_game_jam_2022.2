using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UDL.Core;
using UniRx;

public class MapManager : AbstractView
{
    #region Components
    [SerializeField] Transform Entry, TableTransform;
    public List<Transform> destinationList = new List<Transform>();
    List<string> nameList = new List<string>();
    #endregion

    #region Stats
    #endregion

    public static MapManager InsMapManager;

    public static MapManager Create(Transform parent = null)
    {
        return Instantiate<MapManager>(Resources.Load<MapManager>("Prefabs/Manager/----MapManager----"), parent);
    }

    private void Awake()
    {
        if (InsMapManager == null) InsMapManager = this;
        Table.OnPersonTrue += CreatePeople;
    }

    private IEnumerator Start()
    {
        nameList = PersonReader.GetNames();
        for(int i = 0; i < 7; i++)
        {
            yield return new WaitForSeconds(0.5f);
            CreatePeople();
        }
    }

    private void Update()
    {
        
    }

    public void CreatePeople()
    {
        string name = PersonReader.GetRandomName();
        if(Random.Range(0, 2) < 1)
        {
            Person male = Person.CreateMale(Entry).AddTo(this);
            Person female = Person.CreateFemale().AddTo(this);
            male.isRequest = true;
            male.SetRequestName(name);
            female.SetName(name);
            male.destination = TableTransform.position;
        }
        else
        {
            Person male = Person.CreateMale().AddTo(this);
            Person female = Person.CreateFemale(Entry).AddTo(this);
            female.isRequest = true;
            female.SetRequestName(name);
            male.SetName(name);
            female.destination = TableTransform.position;
        }
    }
}
