using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using Valve.VR;

[RequireComponent(typeof(CharacterController))]

public class GravityCharacter : MonoBehaviour
{
    public float speed = 3.0F;
    public float rotateSpeed = 3.0F;
    void Update() { 
        if (Input.GetButtonDown("Fall"))
            transform.Translate(Vector3.down * Time.deltaTime);
            Debug.Log("move down");
    }
}