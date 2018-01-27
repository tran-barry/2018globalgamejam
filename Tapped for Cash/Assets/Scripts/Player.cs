using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    private Rigidbody2D mRigidBody2D;
    private GameObject scanner;
    private float m_scanMoveMultiplier = 0.5f;
    private float m_moveSpeed = 2f;
    private float m_maxSpeed = 1.5f;
    private float rotateSpeed = 3f;
    private bool isScanning = false;

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

        //Rotate
        bool isRotating = false;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            gameObject.transform.Rotate(0, 0, (rotateSpeed * (isScanning ? m_scanMoveMultiplier : 1f)));
            isRotating = true;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            isRotating = true;
            gameObject.transform.Rotate(0, 0, (-rotateSpeed * (isScanning ? m_scanMoveMultiplier : 1f)));
        }

        //adjust momentum to forward
        if (isRotating)
        {
            //Debug.Log(transform.up.normalized);
            mRigidBody2D.velocity = (transform.up.normalized) * mRigidBody2D.velocity.magnitude;
        }
        
        
        
        //Move
	
        Vector2 deltaPos = Vector2.zero;
        bool isStandingStill = true;
   
		if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            deltaPos.y += m_moveSpeed * (isScanning ? m_scanMoveMultiplier : 1f);
            isStandingStill = false;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            deltaPos.y -= m_moveSpeed * (isScanning ? m_scanMoveMultiplier : 1f);
            isStandingStill = false;
        }

        //cap velocity
        Vector2 newVelocity = mRigidBody2D.velocity;
        
        //gameObject.transform.Translate(deltaPos);
        mRigidBody2D.AddRelativeForce(deltaPos);
        if (newVelocity.magnitude > m_maxSpeed)
        {
            //Debug.Log("capped speed");
            newVelocity = newVelocity.normalized * m_maxSpeed;
            
        }
        if (isStandingStill)
        {
            newVelocity *= 0.9f;
        }
        mRigidBody2D.velocity = newVelocity;

        if (!isRotating)
        {
            mRigidBody2D.angularVelocity = 0f;
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