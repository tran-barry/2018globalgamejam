﻿using System.Collections;
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
    int playerLayer = (~(1 << 8));

    private Vector2 deltaPos = Vector2.zero;
    private float mRotation = 0f;


    public CardSlot[] tempCardSlots = new CardSlot[3];

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


        CheckAllSlots();
        
    }


    private void FixedUpdate()
    {
        gameObject.transform.Rotate(0, 0, mRotation*Time.timeScale);
        mRigidBody2D.AddRelativeForce(deltaPos*Time.timeScale);

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

        if (other.tag == "Card")
        {
            //Debug.Log("Card!!!");
            //add card to array, if possible
            int nextAvailable = FindAvailableCardSlot();
            if (nextAvailable != -1)
            {
                
                tempCardSlots[nextAvailable].Add(other.GetComponent<TempCard>());
            }

        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Card")
        {
            //remove card from slot, if found
            int otherId = other.GetComponent<TempCard>().ID;
            for (int i = 0; i < 3; ++i)
            {
                if (tempCardSlots[i].slotFilled && tempCardSlots[i].card.ID == otherId)
                {
                    tempCardSlots[i].Remove();
                    break;
                }
            }

        }
    }

    private int FindAvailableCardSlot()
    {
        for (int i = 0; i < 3; ++i)
        {
            if (!tempCardSlots[i].slotFilled)
            {
                return i;
            }
        }
        return -1;
    }

    private bool CheckLOS(Vector2 startPos, Vector2 endPos)
    {
        RaycastHit2D hit2D = Physics2D.Raycast(startPos, endPos - startPos, (endPos - startPos).magnitude, playerLayer);
        Debug.DrawRay(startPos, endPos - startPos, Color.red);
        // Debug.Log(startPos + "," + endPos);
        Debug.Log(hit2D.collider.gameObject);
        if (hit2D.collider.gameObject.tag == "Card")
        {
            return true;
        }
        return false;
    }

    private void CheckAllSlots()
    {
        for (int i = 0; i < 3; ++i)
        {
            if (tempCardSlots[i].slotFilled)
            {
                Debug.Log(CheckLOS(transform.position, tempCardSlots[i].card.transform.position));
            }
        }
    }

    

}

[System.Serializable]
public class CardSlot
{
    public TempCard card = null;
    public bool slotFilled = false;
    public float percentComplete = 0f;
    public bool isDrained = false;

    public void Remove()
    {
        card = null;
        slotFilled = false;
        percentComplete = 0f;
        isDrained = false;
    }

    public void Add(TempCard tempCard)
    {
        if (!slotFilled)
        {
            card = tempCard;
            slotFilled = true;
        }
        else
        {
            Debug.Log("Why are you adding to a filled slot?");
        }
        
    }
}


