using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using Valve.VR;

public class Controller2 : MonoBehaviour
{

    private SteamVR_TrackedObject rightTrackedObject;
    private SteamVR_Controller.Device rightDevice;
    private EVRButtonId trigger = EVRButtonId.k_EButton_SteamVR_Trigger;

    // public FixedJoint joint;

    // Use this for initialization
    void Start()
    {
        //rightTrackedObject = GameObject.Find("[CameraRig]/Controller (right)").GetComponent<SteamVR_TrackedObject>();
    }

    public void CreateJoint(GameObject food)
    {
        Debug.Log("starting to create joint");
        if (!food.GetComponent<FixedJoint>())
        {
            Debug.Log("if joint doesn't already exist");
            var fixedJoint= food.AddComponent<FixedJoint>();
            fixedJoint.connectedBody = GameObject.FindWithTag("Right Controller").GetComponent<Rigidbody>();
            Debug.Log("add joint to food and connect it with right controller");
        }
}

    // Update is called once per frame
    void Update () {

        //rightDevice = SteamVR_Controller.Input((int)rightTrackedObject.index);
        //if (rightDevice.GetPressDown(trigger)) //
        //{
        //    //    if OnCollisionEnter(Collision other)
        //    //        Debug.Log("colliding happening");
        //    //    rightDevice = SteamVR_Controller.Input((int)rightTrackedObject.index);
        //    //    if (other.gameObject.tag == "food")
        //    //    {
        //    //        other.transform.parent = rightTrackedObject.transform;
        //    //        Debug.Log("food happening");
        //    //    }
        //    //}
        //    Debug.Log("trigger is pressed");
        //}
    }
}






           