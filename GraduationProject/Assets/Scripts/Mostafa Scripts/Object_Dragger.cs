using UnityEngine;
using System.Collections;
 
public class Object_Dragger : MonoBehaviour 
{
 
	private Vector3 screenPoint;
	private Vector3 offset;
	Rigidbody rb;

    private void Start() {
		rb = GetComponent<Rigidbody>();
	}
    void OnMouseDown()
	{
		screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
		offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
	}
	
	void OnMouseDrag()
	{
		
		Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		//transform.position = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
		rb.velocity = ((Camera.main.ScreenToWorldPoint(curScreenPoint) + offset) - transform.position) * 10;
		//rb.isKinematic = true;
	}

	private void OnMouseUp() {
		//rb.isKinematic = false;
	}


}