using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using Valve.VR;

public class PitFall : MonoBehaviour
{
    public GameObject Floor;
    public float gravityScale = 1.0f;

    // Global Gravity doesn't appear in the inspector. Modify it here in the code
    // (or via scripting) to define a different default gravity for all objects.

    public static float globalGravity = -400f;

    void Update()
    {
        if (Input.GetButtonDown("Fall"))
        {
            this.GetComponent<Rigidbody>().useGravity = true;
            // PlaySound();
            Debug.Log("things are falling");

            Vector3 gravity = globalGravity * gravityScale * Vector3.up;
            this.GetComponent<Rigidbody>().AddForce(gravity, ForceMode.Acceleration);
            

        }
        else
        {
            this.GetComponent<Rigidbody>().useGravity = false;
        }
    }
}