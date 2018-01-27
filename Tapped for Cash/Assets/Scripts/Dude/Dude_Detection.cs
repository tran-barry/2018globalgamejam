using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dude_Detection : MonoBehaviour {

    MonoBehaviour dude;

	// Use this for initialization
	void Start () {
        dude = this.GetComponentInParent<Dude>();	
	}
	
	// Update is called once per frame
	void Update () {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // GLTODO: Call whatever UI starts the scanning process (OnTriggerStay2D does the actual scoring
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.Log("dude trigger: " + collision.name);
        if (collision.name == "BasicPlayer")
        {
            //Debug.Log("playa");
            Player playa = collision.GetComponent<Player>();
            if (playa.IsScanning())
            {
                float dist = Vector3.Distance(this.transform.position, collision.transform.position);
                //float maxdistance = this.transform.
                //Debug.Log("Player overlap start: " + dist.ToString());
                CalculateDistanceAsRatio2D(this.transform, playa.transform);
                CalculateDistanceAsRatio3D(this.transform, playa.transform);
            }

        }
    }

    private float CalculateDistanceAsRatio2D(Transform obj1, Transform obj2)
    {
        float centerDiff = 0.0f; // difference between the centers
        float colliderDiff = 0.0f; // difference between the collider edges

        float rayDist1 = 0.0f;
        float rayDist2 = 0.0f;
        RaycastHit2D rayHit;

        Vector3 drawlineOffset = new Vector3(0, 0.02f, 0); // for the drawlines to not render over each other

        centerDiff = (obj2.position - obj1.position).magnitude; // distance between 2 objects

        rayHit = Physics2D.Raycast(obj1.position, Vector3.zero - (obj1.position - obj2.position).normalized);
        if (rayHit.collider != null)
        {
            Debug.DrawLine(obj1.position, rayHit.point, Color.red);
            rayDist1 = rayHit.distance; //Vector2.Distance(rayHit.point, obj1.position);
        }

        rayHit = Physics2D.Raycast(obj2.position, Vector3.zero - (obj2.position - obj1.position).normalized);
        if (rayHit.collider != null)
        {
            Debug.DrawLine(obj2.position + drawlineOffset, rayHit.point + (Vector2)drawlineOffset, Color.green); // offset so line can be seen
            rayDist2 = Vector2.Distance(rayHit.point, obj2.position);
        }

        float centerDiffRayDist1 = (centerDiff - rayDist1);
        float centerDiffRayDist2 = (centerDiff - rayDist2);

        colliderDiff = centerDiff - (centerDiff - rayDist1) - (centerDiff - rayDist2); // colliderDiff - (collider2 radius) - (collider1 radius)

        float ratio = (colliderDiff / centerDiff);
        Debug.Log("colliderDiff2D: " + colliderDiff.ToString() + ", centerDiff: " + centerDiff.ToString() + ", ratio: " + ratio.ToString() + ", centerDiffRayDist1: " + centerDiffRayDist1.ToString() + ", " + centerDiffRayDist2.ToString());

        return ratio;
    }

    private float CalculateDistanceAsRatio3D(Transform obj1, Transform obj2)
    {
        float centerDiff = 0.0f; // difference between the centers
        float colliderDiff = 0.0f; // difference between the collider edges

        float rayDist1 = 0.0f;
        float rayDist2 = 0.0f;
        RaycastHit rayHit;

        Vector3 drawlineOffset = new Vector3(0, 0.02f, 0); // for the drawlines to not render over each other

        centerDiff = (obj2.position - obj1.position).magnitude; // distance between 2 objects

        Physics.Raycast(obj1.position, Vector3.zero - (obj1.position - obj2.position).normalized, out rayHit);
        if (rayHit.collider != null)
        {
            Debug.DrawLine(obj1.position, rayHit.point, Color.red);
            rayDist1 = Vector2.Distance(rayHit.point, obj1.position);
        }

        Physics.Raycast(obj2.position, Vector3.zero - (obj2.position - obj1.position).normalized, out rayHit);
        if (rayHit.collider != null)
        {
            Debug.DrawLine(obj2.position + drawlineOffset, rayHit.point + drawlineOffset, Color.green); // offset so line can be seen
            rayDist2 = Vector2.Distance(rayHit.point, obj2.position);
        }

        float centerDiffRayDist1 = (centerDiff - rayDist1);
        float centerDiffRayDist2 = (centerDiff - rayDist2);

        colliderDiff = centerDiff - (centerDiff - rayDist1) - (centerDiff - rayDist2); // colliderDiff - (collider2 radius) - (collider1 radius)

        float ratio = (colliderDiff / centerDiff);
        Debug.Log("3D: " + obj1.position + " ," + obj2.position);
        Debug.Log("colliderDiff3D: " + colliderDiff.ToString() + ", centerDiff: " + centerDiff.ToString() + ", ratio: " + ratio.ToString() + ", centerDiffRayDist1: " + centerDiffRayDist1.ToString() + ", " + centerDiffRayDist2.ToString());

        return ratio;
    }
}
