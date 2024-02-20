//Last Editor: Caleb Richardson
//Last Edited: Feb 14

using System;
using UnityEngine;

public class CameraController : MonoBehaviour{
	public float WanderLimit => wanderLimitFromMidpoint;
	public Vector3 MidPoint => midpoint;

	[SerializeField] private float wanderLimitFromMidpoint = 25f;

	[SerializeField] private float smoothSpeed = 5f;
	private Transform[] playerTransforms = new Transform[2];

	private Vector3 cameraOffSet;

	private bool trackPlayers = false;
	private Vector3 midpoint;

	private void Start() {
		PlayerManager.Instance.OnCorrectPlayerCount += SetupCamera;
		CalculateOffset();
	}

    private void OnDestroy() {
		PlayerManager.Instance.OnCorrectPlayerCount -= SetupCamera;
	}

    private void CalculateOffset(){
		cameraOffSet = transform.position;
		cameraOffSet.y = 0;
    }

    private void SetupCamera(object sender, EventArgs e){
		var currentPlayerList = PlayerManager.Instance.CurrentPlayerList;

		for (int i = 0; i < currentPlayerList.Count; i++){
			playerTransforms[i] = currentPlayerList[i].GetPlayerTransform();
		}

		trackPlayers = true;
    }

    private void Update() {
		if(!trackPlayers) return;

		midpoint = CalculateMidPoint();

		transform.position = Vector3.Lerp(transform.position, new Vector3(midpoint.x, transform.position.y, midpoint.z) + cameraOffSet, smoothSpeed * Time.deltaTime);
	}

	private Vector3 CalculateMidPoint(){
		var _midpoint = (playerTransforms[0].position + playerTransforms[1].position) / 2f;

		_midpoint.y = 1f;

		return _midpoint;
	}

	private void OnDrawGizmos() {
		Gizmos.color = Color.green;
		Gizmos.DrawWireCube(midpoint, Vector3.one);
	}
}
