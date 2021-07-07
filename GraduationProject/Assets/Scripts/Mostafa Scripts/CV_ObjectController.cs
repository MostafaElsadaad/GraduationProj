using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CV_ObjectController : MonoBehaviour
{
    string[] backgroundMusic = { "Music1", "Music2" };
    public Vector3 Hand_coordinates;
    Rigidbody rb;
    public float damping_factor = 0.08f;
    Vector3 receivedPos = Vector3.zero;
    public string gesture = "open";
    RecievedData DataReciever;
    public Vector3 offset;
    public GameObject collided;
    public bool grasped = false;
    Vector3 to = new Vector3(0, 0, 160);
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManager>().Stop("Theme");
        FindObjectOfType<AudioManager>().Play(backgroundMusic[Random.Range(0, 1)]);
        DataReciever = FindObjectOfType<RecievedData>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // For Testing Purposes
        if (collided) {
            Debug.Log(getRelativePosition(gameObject.transform, collided.transform.position));
        }
        if (!DataReciever) {
            DataReciever = FindObjectOfType<RecievedData>();
        }
        if (!rb) {
            rb = GetComponent<Rigidbody>();
        }
        // For Testing Purposes

        receivedPos = DataReciever.receivedPos;
        gesture = DataReciever.gesture;
        Hand_coordinates = new Vector3(Remap_Trans(receivedPos.x, 0.25f, 0.75f, -1.85f, 2.0063f), Remap_Trans(receivedPos.y, 0.6f  , 0.4f, 0.22f, 2f), Remap_Trans(receivedPos.z, 0.15f, 0.25f, -1.245f, 1.7f));
        rb.velocity = (Hand_coordinates - transform.position) * damping_factor; // Remap_Trans(receivedPos.y, 0.75f - (receivedPos.z/3) , 0.25f + (receivedPos.z / 3), 0.25f, 2.35f)
        if ((grasped == true) && (gesture == "grasp")) {
            collided.transform.position = gameObject.transform.position + new Vector3(-0.1f,0.3f,0.4f);
            transform.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, to, Time.deltaTime);
            //transform.rotation = Quaternion.Lerp(from.rotation, to.rotation, Time.time * 0.1f);
        }
    }

    private void FixedUpdate() {
        
    }

    void OnCollisionEnter(Collision collision) {
        Debug.Log(collision.gameObject.tag);
        if ((collision.gameObject.tag == "Blue_Cube") || (collision.gameObject.tag == "Red_Cube")){ 
        collided = collision.gameObject;
    }
        if (((collision.gameObject.tag == "Blue_Cube") || (collision.gameObject.tag == "Red_Cube")) && (gesture == "grasp")) {
            grasped = true;
            offset = gameObject.transform.position - collision.gameObject.GetComponent<Transform>().position;
            
        }
    }

    void OnCollisionExit(Collision collision) {
       // grasped = false;
    }

    public float Remap_Trans(float value, float in_start, float in_end, float Out_start, float out_end) {
        return (value - in_start) / (in_end - in_start) * (out_end - Out_start) + Out_start;
    }

    public  Vector3 getRelativePosition(Transform origin, Vector3 position) {
        Vector3 distance = position - origin.position;
        Vector3 relativePosition = Vector3.zero;
        relativePosition.x = Vector3.Dot(distance, origin.right.normalized);
        relativePosition.y = Vector3.Dot(distance, origin.up.normalized);
        relativePosition.z = Vector3.Dot(distance, origin.forward.normalized);

        return relativePosition;
    }

}
