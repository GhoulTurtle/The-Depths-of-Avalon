using UnityEngine;

//Testing Script for a camera system
public class CameraController : MonoBehaviour{
	//For test purposes using assigned transforms
	[SerializeField] private Transform player1;
	[SerializeField] private Transform player2;

	[SerializeField] private float minZoom = 5f;
	[SerializeField] private float maxZoom = 10f;

	[SerializeField] private float smoothSpeed = 5f;

	private Camera playerCam;

	private void Awake() {
		TryGetComponent(out playerCam);
	}

	private void Update() {
		var midpoint = CalculateMidPoint();

		transform.position = new Vector3(midpoint.x, transform.position.y, midpoint.y);

		SmoothZoom();
	}

	private Vector3 CalculateMidPoint(){
		return (player1.position + player2.position) / 2f;
	}

	private void SmoothZoom(){
		float distance = Vector3.Distance(player1.position, player2.position);
    	float targetSize = Mathf.Clamp(distance, minZoom, maxZoom);
   		playerCam.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, targetSize, smoothSpeed * Time.deltaTime);
	}
}
