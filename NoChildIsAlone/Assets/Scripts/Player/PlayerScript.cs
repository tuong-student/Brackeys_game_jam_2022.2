using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UDL.Core;
using DG.Tweening;

public class PlayerScript : AbstractView
{
    #region Components
    [SerializeField] Rigidbody myBody;
    [SerializeField] Animator playerAnim;
    [SerializeField] GameObject pressE;
    [SerializeField] Transform upHeadTransform;
    Vector3 movement;

    public Person currentPerson;
    #endregion

    #region Stats
    [SerializeField] float speed;
    [SerializeField] float dashSpeed;
    float currentSpeed;
    float currentMaxSpeed;
    [SerializeField] float dashDuration;
    [SerializeField] float maxSpeed;
    [SerializeField] float maxDashSpeed;
    Vector3 direction;
    bool isFollowed;
    #endregion

    public static PlayerScript InsPlayerScript;

    public static PlayerScript Create(Transform parent = null)
    {
        return Instantiate<PlayerScript>(Resources.Load<PlayerScript>("Prefabs/Characters/Player"), parent);
    }

    void Awake()
    {
        if (InsPlayerScript == null) InsPlayerScript = this;
        pressE = GameObject.FindGameObjectWithTag("E");
        pressE.gameObject.SetActive(false);
    }

    void Start()
    {
        currentSpeed = speed;
        currentMaxSpeed = maxSpeed;

    }

    void Update()
    {
        Move();
        pressE.transform.position = NOOD.NoodyCustomCode.WorldPointToScreenPoint(upHeadTransform.position);
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isFollowed)
            {
                currentPerson.isFollowPlayer = false;
                isFollowed = false;
            }

            if (pressE.gameObject.activeInHierarchy)
            {
                if (currentPerson && isFollowed == false)
                {
                    currentPerson.isFollowPlayer = true;
                    isFollowed = true;
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Table"))
        {
            currentPerson.isFollowPlayer = false;
            isFollowed = false;
            collision.gameObject.GetComponent<Table>().SetApplyPerson(currentPerson);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Person person = other.GetComponent<Person>();
        if (person)
        {
            currentPerson = person;
            person.isPlayer = true;
            person.Player = this.gameObject;
            ShowPressEText(true);
            NameManager.InsNameManager.ShowNameText(person.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Person person = other.GetComponent<Person>();
        if (person)
        {
            person.isPlayer = false;
            ShowPressEText(false);
            NameManager.InsNameManager.HideNameText();
        }
    }

    void Move()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.z = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            currentSpeed = dashSpeed;
            currentMaxSpeed = maxDashSpeed;
            StartCoroutine(setNormalSpeed());
        }

        if (myBody.velocity.magnitude < currentMaxSpeed)
        {
            myBody.AddForce(movement * currentSpeed, ForceMode.Impulse);
            playerAnim.SetBool("Run", true);
        }

        if (movement == Vector3.zero)
        {
            playerAnim.SetBool("Run", false);
        }

        direction = (movement).normalized;
        if (direction != Vector3.zero)
            this.transform.forward = direction;
    }

    IEnumerator setNormalSpeed()
    {
        yield return new WaitForSeconds(dashDuration);
        currentSpeed = speed;
        currentMaxSpeed = maxSpeed;
    }

    public void FaceToPerson(Vector3 personPostion)
    {
        Vector3 dir = NOOD.NoodyCustomCode.LookDirection(this.transform.position, personPostion);
        this.transform.forward = dir;
    }

    public void ShowPressEText(bool value)
    {
        pressE.gameObject.SetActive(value);
    }
}
