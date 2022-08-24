using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Person : MonoBehaviour
{
    #region Components
    [SerializeField] Animator anim;
    [SerializeField] List<Vector3> destinationList = new List<Vector3>();
    #endregion

    #region Stats
    public Vector3 destination;
    public string name;
    public string requestName;
    public bool isRequest;

    [SerializeField]
    bool isFinish;
    bool isBlock;
    bool isPlayer;
    int i;
    #endregion

    public static Person CreateMale(Transform parent = null)
    {
        return Instantiate<Person>(Resources.Load<Person>("Prefabs/Characters/Male"), parent);
    }

    public static Person CreateFemale(Transform parent = null)
    {
        return Instantiate<Person>(Resources.Load<Person>("Prefabs/Characters/Female"), parent);
    }

    private void Start()
    {
        destinationList = MapManager.InsMapManager.destinationList;
        i = 0;
    }

    private void Update()
    {
        if (isRequest)
            MoveWhenRequest();
        else
            MoveRandom();


    }

    private void OnTriggerEnter(Collider other)
    {
        isBlock = true;
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayer = true;
            Stop();
            other.gameObject.GetComponent<PlayerScript>().FaceToPerson(this.transform.position);
            CustomCamera.InsCustomCamera.SetCamToHead();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isBlock = false;
        isPlayer = false;
    }

    public void SetName(string name)
    {
        this.name = name;
    }

    public void SetRequestName(string name)
    {
        requestName = name;
    }

    void MoveWhenRequest()
    {
        Vector3 dir = NOOD.NoodyCustomCode.LookDirection(this.transform.position, destination);
        if(Vector3.Distance(this.transform.position, destination) > 1f && isBlock == false)
        {
            anim.SetBool("Run", true);
            this.transform.position += dir * 5f * Time.deltaTime;
            Rotate(dir);
        }
        else
        {
            Stop();
            if(isFinish)
                MoveNext();
        }
    }

    void MoveRandom()
    {
        if (isPlayer) return;
        Vector3 dir = NOOD.NoodyCustomCode.LookDirection(this.transform.position, destination);
        if (Vector3.Distance(this.transform.position, destination) > 1f && isBlock == false)
        {
            anim.SetBool("Run", true);
            this.transform.position += dir * 2f * Time.deltaTime;
            Rotate(dir);
        }
        else
        {
            Stop();
            DelayMove();
        }

        void DelayMove()
        {
            Vector3 newPos = NOOD.NoodyCustomCode.GetPointAroundAPosition3D(this.transform.position, 3);
            destinationList.Add(newPos);
            MoveNext();
        }
    }

    void MoveNext()
    {
        isBlock = false;
        if (i++ < destinationList.Count)
        {
            destination = destinationList[i];
        }
    }

    void Rotate(Vector3 direction)
    {
        
        this.transform.forward = direction;
    }

    void Stop()
    {
        anim.SetBool("Run", false);
    }
}

public class PersonReader
{
    static string path = "Assets/Scripts/Name/Name.txt";

    public static List<string> GetNames()
    {
        List<string> nameList = new List<string>();
        StreamReader inp_stm = new StreamReader(path);

        while (!inp_stm.EndOfStream)
        {
            string inp_ln = inp_stm.ReadLine();
            // Do Something with the input. 
            nameList.Add(inp_ln);
        }

        inp_stm.Close();
        return nameList;
    }

    public static string GetRandomName()
    {
        List<string> names = GetNames();
        int r = Random.Range(0, names.Count);
        return names[r];
    }

    public static string GetRandomNameExcept(string exception)
    {
        List<string> names = GetNames();
        int r = Random.Range(0, names.Count);
        while (names[r].Equals(exception))
        {
            r = Random.Range(0, names.Count);
        }
        return names[r];

    }
}