using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    private Rigidbody2D mRigidBody2D;
    private float m_scanMoveMultiplier = 0.5f;
    private float m_moveSpeed = 0.2f;
    private float rotateSpeed = 3f;
    private bool isScanning = false;

    private void Awake()
    {
        mRigidBody2D = GetComponent<Rigidbody2D>();
    }

    void Start () {
		
	}
	
	void Update () {
        //Rotate
        if (Input.GetKey(KeyCode.A))
        {
            gameObject.transform.Rotate(0, 0, (rotateSpeed * (isScanning ? m_scanMoveMultiplier : 1f)));
        }
        if (Input.GetKey(KeyCode.D))
        {
            gameObject.transform.Rotate(0, 0, (-rotateSpeed * (isScanning ? m_scanMoveMultiplier : 1f)));
        }

        //Move


        Vector3 deltaPos = Vector3.forward;
        deltaPos.z = 0f;
		if (Input.GetKey(KeyCode.W))
        {
            deltaPos.y += m_moveSpeed * (isScanning ? m_scanMoveMultiplier : 1f);
        }
        if (Input.GetKey(KeyCode.S))
        {
            deltaPos.y -= m_moveSpeed * (isScanning ? m_scanMoveMultiplier : 1f);
        }
        
        gameObject.transform.Translate(deltaPos);
    }
}
