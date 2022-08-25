using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Person : MonoBehaviour
{
    #region Components
    [SerializeField] Animator anim;
    [SerializeField] List<Vector3> destinationList = new List<Vector3>();
    [SerializeField] GameObject Player;
    [HideInInspector] public Person followPerson;
    #endregion

    #region Stats
    public Vector3 destination;
    public string name;
    public string requestName;
    public bool isRequest;
    
    public bool isFinish;
    bool isBlock;
    bool isPlayer;
    int i;
    bool isMale;
    public bool isFollowPlayer;
    #endregion

    public bool IsMale
    {
        get { return isMale; }
        set { isMale = value; }
    }

    public static Person CreateMale(Transform parent = null)
    {
        Person person = Instantiate<Person>(Resources.Load<Person>("Prefabs/Characters/Male"), parent);
        person.IsMale = true;
        return person;
    }

    public static Person CreateFemale(Transform parent = null)
    {
        Person person = Instantiate<Person>(Resources.Load<Person>("Prefabs/Characters/Female"), parent);
        person.IsMale = false;
        return person;
    }

    private void Start()
    {
        destinationList.Clear();
        foreach(Transform trans in MapManager.InsMapManager.destinationList)
        {
            destinationList.Add(trans.position);
        }
        i = 0;
    }

    private void Update()
    {
        if (isFollowPlayer)
        {
            FollowPlayer();
            return;
        }

        if(isFinish && !isRequest)
        {
            FollowPerson();
            return;
        }

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
            PlayerScript playerScript = other.gameObject.GetComponent<PlayerScript>();
            playerScript.FaceToPerson(this.transform.position);
            playerScript.currentPerson = this;
            PlayerScript.InsPlayerScript.ShowPressEText(true);
            NameManager.InsNameManager.ShowNameText(this.name);
            Player = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isBlock = false;
        NameManager.InsNameManager.HideNameText();
        PlayerScript.InsPlayerScript.ShowPressEText(false);
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayer = false;
        }
    }

    public void FollowPlayer()
    {
        if (Player)
        {
            destination = Player.transform.position;
            MoveUntilDistance(3f);
        }
    }

    public void FollowPerson()
    {
        isFollowPlayer = false;
        isFinish = true;
        destination = followPerson.transform.position;
        MoveUntilDistance(2f);
    }

    public void SetName(string name)
    {
        this.name = name;
    }

    public void SetRequestName(string name)
    {
        requestName = name;
    }

    void MoveUntilDistance(float distance)
    {
        Vector3 dir = NOOD.NoodyCustomCode.LookDirection(this.transform.position, destination);
        if (Vector3.Distance(this.transform.position, destination) > distance)
        {
            anim.SetBool("Run", true);
            this.transform.position += dir * 5f * Time.deltaTime;
            Rotate(dir);
        }
        else
        {
            Stop();
        }
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
            if (i == 0 && isBlock == false)
            {
                NameManager.InsNameManager.SetRequest(!this.IsMale, this.requestName);
                Table.InsTable.requestPerson = this;
            }
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
            StartCoroutine(DelayMove());
        }

        IEnumerator DelayMove()
        {
            yield return new WaitForSeconds(0.5f);
            Vector3 newPos = NOOD.NoodyCustomCode.GetPointAroundAPosition3D(this.transform.position, 10f);
            newPos.y = 0;
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