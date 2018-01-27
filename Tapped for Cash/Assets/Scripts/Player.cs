using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    private Rigidbody2D mRigidBody2D;
    private GameObject scanner;
    private float m_scanMoveMultiplier = 0.5f;
    private float m_moveSpeed = 200f;
    private float m_maxSpeed = 1.5f;
    private float rotateSpeed = 3f;
    private bool isScanning = false;

    private Vector2 deltaPos = Vector2.zero;
    private float mRotation = 0f;

    public bool IsScanning()
    {
        return isScanning;
    }

    private void Awake()
    {
        mRigidBody2D = GetComponent<Rigidbody2D>();
        scanner = transform.Find("Dude_scanning").gameObject;
        scanner.SetActive(false);
    }

    void Start () {
		
	}
	
	void Update () {
	    if (Time.timeScale == 0)
	    {
	        return;
	    }

        // Toggle scan
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(isScanning)
            {
                isScanning = false;
            }
            else
            {
                isScanning = true;
            }
            scanner.SetActive(isScanning);
        }

        //Rotation Inputs
        mRotation = 0f;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            mRotation = rotateSpeed * (isScanning ? m_scanMoveMultiplier : 1f);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            mRotation = -rotateSpeed * (isScanning ? m_scanMoveMultiplier : 1f);
        }

        
        
        
        
        //Move Inputs
        deltaPos = Vector2.zero;
   
	    if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            deltaPos.y += m_moveSpeed * (isScanning ? m_scanMoveMultiplier : 1f);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            deltaPos.y -= m_moveSpeed * (isScanning ? m_scanMoveMultiplier : 1f);
        }

        
    }


    private void FixedUpdate()
    {
        if (Time.timeScale == 0)
        {
            return;
        }

        gameObject.transform.Rotate(0, 0, mRotation);
        mRigidBody2D.AddRelativeForce(deltaPos);

        //cap velocity
        Vector2 newVelocity = mRigidBody2D.velocity;

        //gameObject.transform.Translate(deltaPos);

        if (newVelocity.magnitude > m_maxSpeed)
        {
            //Debug.Log("capped speed");
            newVelocity = newVelocity.normalized * m_maxSpeed;

        }
        if (deltaPos ==Vector2.zero)
        {
            newVelocity *= 0.9f;
        }
        mRigidBody2D.velocity = newVelocity;

        if (mRotation==0)
        { //stop rotating
            mRigidBody2D.angularVelocity = 0f;
        }
        else
        {//adjust momentum to forward
            Debug.Log(transform.up.normalized);
            mRigidBody2D.velocity = (transform.up.normalized) * mRigidBody2D.velocity.magnitude;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("player trigger");
        if (other.name == "BasicPlayer")
        {
            Player playa = other.GetComponent<Player>();
            Debug.Log("Player overlap start");

        }
    }

}
