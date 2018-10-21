using UnityEngine;
using System.Collections;

public class BasicTargetMover : MonoBehaviour {

	public bool doSpin = true;
	public bool doMotion = true;

	public float spinSpeed = 180.0f;
	public float motionMagnitude = 0.1f;

	// Update is called once per frame
	void Update () {
		
		if (doSpin) {
			// rotate gameObject around its up axis
			this.gameObject.transform.Rotate (Vector3.up * spinSpeed * Time.deltaTime);	
		}

		if (doMotion) {
			// move gameObject up and down
			this.gameObject.transform.Translate(Vector3.up * Mathf.Cos(Time.timeSinceLevelLoad) * motionMagnitude);	
		}
	}
}
