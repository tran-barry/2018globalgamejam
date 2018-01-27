using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    private Rigidbody2D mRigidBody2D;
    private GameObject scanner;
    private float m_scanMoveMultiplier = 0.5f;
    private float m_moveSpeed = 0.2f;
    private float rotateSpeed = 3f;
    private bool isScanning = false;

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
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            gameObject.transform.Rotate(0, 0, (rotateSpeed * (isScanning ? m_scanMoveMultiplier : 1f)));
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            gameObject.transform.Rotate(0, 0, (-rotateSpeed * (isScanning ? m_scanMoveMultiplier : 1f)));
        }

        //Move


        Vector3 deltaPos = Vector3.forward;
        deltaPos.z = 0f;
		if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            deltaPos.y += m_moveSpeed * (isScanning ? m_scanMoveMultiplier : 1f);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            deltaPos.y -= m_moveSpeed * (isScanning ? m_scanMoveMultiplier : 1f);
        }
        
        gameObject.transform.Translate(deltaPos);
    }
}