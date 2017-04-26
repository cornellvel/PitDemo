using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using Valve.VR;

[RequireComponent(typeof(SteamVR_TrackedObject))]

public class PickupParent : MonoBehaviour
{

    private SteamVR_TrackedObject rightTrackedObject;
    private SteamVR_Controller.Device rightDevice;
    private EVRButtonId trigger = EVRButtonId.k_EButton_SteamVR_Trigger;

    public bool InCart = false;
    public double Price;

    // value to track if the trigger was pressed; initialized to zero
    int pressed = 0;

    // Use this for initialization
    void Start()
    {

        //rightTrackedObject = GameObject.Find("[CameraRig]/Controller (right)").GetComponent<SteamVR_TrackedObject>();

    }

    SteamVR_TrackedObject trackedObj;
    public Transform sphere;

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    /**If the touchpad is pressed, the sphere will be reset -- its position, velocity, and angular velocity will be all 
	   be transformed back to the 0 vector. **/
    void FixedUpdate()
    {
        SteamVR_Controller.Device device = SteamVR_Controller.Input((int)trackedObj.index);

        if (device.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
        {
            sphere.transform.position = new Vector3(0.0528f, 0.166f, -1.073f);
            sphere.GetComponent<Rigidbody>().velocity = Vector3.zero;
            sphere.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }

    }

    void OnTriggerStay(Collider col)
    {
        SteamVR_Controller.Device device = SteamVR_Controller.Input((int)trackedObj.index);

        //If the trigger on the controller is pressed, set the sphere to be a child of the controller.
        if (device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
        {
            //rigidBody will not be affected by the Physics system because it is being moved by our hands 
            col.attachedRigidbody.isKinematic = true;
            col.gameObject.transform.SetParent(this.gameObject.transform);
        }

        //If the trigger on the controller is released, 
        if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            col.gameObject.transform.SetParent(null);
            col.attachedRigidbody.isKinematic = false;
            col.attachedRigidbody.useGravity = true;
       


            tossObject(col.attachedRigidbody);
        }
    }

    void OnCollisionStay(Collision collisionInfo)
    {

        rightTrackedObject = GameObject.FindWithTag("Right Controller").GetComponent<SteamVR_TrackedObject>();
        //Debug.Log("coliision happening");
        rightDevice = SteamVR_Controller.Input((int)rightTrackedObject.index);

        if (collisionInfo.gameObject.tag == "Right Controller")
        {
            //Debug.Log("right controller");
            if (rightDevice.GetPressDown(trigger))
            {
                //Debug.Log("trigger press down");
                this.gameObject.transform.parent = rightTrackedObject.transform;

                // indicated that the trigger was pressed down
                pressed = 1;

                Debug.Log(this.gameObject.name + " picked up ON COLLISION STAY");
            }
            if (rightDevice.GetPressUp(trigger))
            {

                // indicates that at some point during the collision, the trigger was released
                pressed = 2;

                //Debug.Log("trigger press up");

                this.gameObject.transform.parent = null;

                Debug.Log("using gravity and kinematics with " + this.gameObject.name + " ON COLLISION STAY");
                this.GetComponent<Rigidbody>().useGravity = true;
                this.GetComponent<Rigidbody>().isKinematic = false;
                Debug.Log(this.gameObject.name + " dropped off ON COLLISION STAY");
            }
        }
        else
        {
            if (this.GetComponent<Rigidbody>().useGravity)
            {
                Debug.Log("take away gravity from " + this.gameObject.name);
                this.GetComponent<Rigidbody>().useGravity = false;
            }
            if (!this.GetComponent<Rigidbody>().isKinematic)
            {
                Debug.Log("take away kinematics from " + this.gameObject.name);
                this.GetComponent<Rigidbody>().isKinematic = true;
            }
        }
    }

    void OnCollisionExit(Collision collisionInfo)
    {
        rightTrackedObject = GameObject.FindWithTag("Right Controller").GetComponent<SteamVR_TrackedObject>();
        rightDevice = SteamVR_Controller.Input((int)rightTrackedObject.index);

        // if (pressed == 2 || this.gameObject.transform.parent != null)
        if (pressed == 2 || rightDevice.GetPressUp(trigger))
        {
            //Debug.Log("No longer in contact with " + this.gameObject.name);
            this.gameObject.transform.parent = null;
            Debug.Log("using gravity and kinematics with " + this.gameObject.name + " ON COLLISION EXIT");
            this.GetComponent<Rigidbody>().useGravity = true;
            this.GetComponent<Rigidbody>().isKinematic = false;
            Debug.Log(this.gameObject.name + " dropped off ON COLLISION EXIT");
        }
    }

    /**tossObject takes a rigidBody and, if the origin point does exist, sets the velocity of the rigidbody to the world
		space's transform of that velocity. If the origin does not exist, it naively sets the rigidbody's velocity
		to that of the device. **/
    void tossObject(Rigidbody rigidBody)
    {
        SteamVR_Controller.Device device = SteamVR_Controller.Input((int)trackedObj.index);

        Transform origin = trackedObj.origin ? trackedObj.origin : trackedObj.transform.parent;

        //Converts the transforms from local space to world space for better accuracy 
        if (origin != null)
        {
            rigidBody.velocity = origin.TransformVector(device.velocity);
            rigidBody.angularVelocity = origin.TransformVector(device.angularVelocity);
        }

        //naive approximation
        else
        {
            rigidBody.velocity = device.velocity;
            rigidBody.angularVelocity = device.angularVelocity;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}