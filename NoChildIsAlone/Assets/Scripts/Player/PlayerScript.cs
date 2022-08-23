using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    #region Components
    [SerializeField] Rigidbody myBody;
    [SerializeField] Animator playerAnim;
    Vector3 movement;
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
    #endregion

    public static PlayerScript InsPlayerScript;

    void Awake()
    {
        if (InsPlayerScript == null) InsPlayerScript = this;
    }

    void Start()
    {
        currentSpeed = speed;
        currentMaxSpeed = maxSpeed;
    }

    void Update()
    {
        Move();
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
}
