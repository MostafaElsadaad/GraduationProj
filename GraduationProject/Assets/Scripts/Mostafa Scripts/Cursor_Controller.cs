using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems; // Required when using Event data.
using UnityEngine.UI;


public class Cursor_Controller : MonoBehaviour, IPointerDownHandler, IDragHandler {
    public RectTransform image_movable;
    public Button button;
    public Toggle m_Toggle;
    public bool colliding = false;
    public bool clicked = false;
    public Sprite[] cursors;
    Vector3 receivedPos = Vector3.zero;
    public Vector3 Hand_coordinates;
    Rigidbody rb;
    public float damping_factor = 0.01f;
    RecievedData DataReciever;
    private string hover_type = "none";
    private void Start() {
        DataReciever = FindObjectOfType<RecievedData>();
        gameObject.GetComponent<Image>().sprite = cursors[0];
        rb = GetComponent<Rigidbody>();
    }
    private void Update() {
        receivedPos = DataReciever.receivedPos;
        //Hand_coordinates = new Vector3(Remap_Trans(receivedPos.x, 30, 70, -900f, 900f), Remap_Trans(receivedPos.y, 30, 70, -490f, 490f), rb.position.z);
        Hand_coordinates = new Vector3(Remap_Trans(receivedPos.x, 0.3f, 0.8f, -900f, 900f), Remap_Trans(receivedPos.y, 0.8f, 0.3f, -490f, 490f), rb.position.z);
        image_movable.anchoredPosition = Vector2.Lerp(image_movable.anchoredPosition, Hand_coordinates, damping_factor);
        
    }

    private void OnTriggerEnter(Collider other) {
        if (other.GetComponent<Button>() != null) {
            button = other.GetComponent<Button>();
            hover_type = "Button";
        }
        else if (other.GetComponent<Toggle>() != null) {
            m_Toggle = other.GetComponent<Toggle>();
            hover_type = "Toggle";
        }
        colliding = true;
        if (clicked == false) {
            StartCoroutine("clickgesture");
        }
    }

    private void OnTriggerExit(Collider other) {
        colliding = false;
    }



    IEnumerator clickgesture() {
        yield return new WaitWhile(() => DataReciever.gesture != "index_opos");
          if (colliding == true) {
                var pointer = new PointerEventData(EventSystem.current); // pointer event for Execute
            if (hover_type == "Button") {
                ExecuteEvents.Execute(button.gameObject, pointer, ExecuteEvents.submitHandler);
            }
            else if (hover_type == "Toggle") {
                m_Toggle.isOn = true;
                Debug.Log("TASDAD");
            }
                clicked = true;
          }
        yield return new WaitForSeconds(0.2f);
        clicked = false;
    }
/*    IEnumerator CountToClick() {
        gameObject.GetComponent<Image>().sprite = cursors[1];
        yield return new WaitForSeconds(1);
        if (colliding == false) {
            gameObject.GetComponent<Image>().sprite = cursors[0];
            yield break;
        }
        gameObject.GetComponent<Image>().sprite = cursors[2];
        yield return new WaitForSeconds(1);

        if (colliding == false) {
            gameObject.GetComponent<Image>().sprite = cursors[0];
            yield break;
        }
        gameObject.GetComponent<Image>().sprite = cursors[3];

        yield return new WaitForSeconds(1);
        if (colliding == false) {
            gameObject.GetComponent<Image>().sprite = cursors[0];
            yield break;
        }
        if (colliding == true) {
            var pointer = new PointerEventData(EventSystem.current); // pointer event for Execute
            ExecuteEvents.Execute(button.gameObject, pointer, ExecuteEvents.submitHandler);
            gameObject.GetComponent<Image>().sprite = cursors[0];
        }

    }*/


    public float Remap_Trans(float value, float in_start, float in_end, float Out_start, float out_end) {
        return (value - in_start) / (in_end - in_start) * (out_end - Out_start) + Out_start;
    }


    public void OnPointerDown(PointerEventData eventData) {
        //Debug.Log(this.gameObject.name + " Was Clicked.");
    }

    void IDragHandler.OnDrag(PointerEventData eventData) {
        image_movable.anchoredPosition += eventData.delta;
    }
}
