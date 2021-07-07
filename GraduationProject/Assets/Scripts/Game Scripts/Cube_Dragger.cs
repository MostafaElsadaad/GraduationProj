using UnityEngine;
using System.Collections;
 
public class Cube_Dragger : MonoBehaviour {

	private Vector3 screenPoint;
	private Vector3 offset;
	Rigidbody rb;
	public float damping_factor = 10;

	////public ParticleSystem dustEffect;
	private void Start() {
		rb = GetComponent<Rigidbody>();
	}


    void OnMouseDown() {
		//createDust();
		screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
		offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
	}

	void OnMouseDrag() {
		//createDust();
		Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		//transform.position = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
		rb.velocity = ((new Vector3(Camera.main.ScreenToWorldPoint(curScreenPoint).x, Camera.main.ScreenToWorldPoint(curScreenPoint).y, gameObject.transform.position.z + Input.mouseScrollDelta.y) + new Vector3(offset.x,offset.y,0)) - transform.position) * damping_factor;
	}



	//void createDust()
	//{
	//dustEffect.Play();
	//}

}