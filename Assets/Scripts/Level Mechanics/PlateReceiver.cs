using System;
using System.Collections;
using UnityEngine;

public class PlateReceiver : MonoBehaviour {
    public PlateObject PlateObject;

    public Transform objectToMove;
    public Vector3 activatedLocalPosition;
    private Vector3 originalLocalPosition;
    public float moveSpeed = 1.0f;

    private bool isMoving = false;
    private bool isActivated = false;

    private void Start() {
        originalLocalPosition = objectToMove.localPosition; // Store the original local position
    }

    private void OnEnable() {
        PlateObject.OnActivate += OnPlateActivate;
        PlateObject.OnDeactivate += OnPlateDeactivate;
    }

    private void OnDisable() {
        PlateObject.OnActivate -= OnPlateActivate;
        PlateObject.OnDeactivate -= OnPlateDeactivate;
    }

    private void OnPlateActivate(object sender, EventArgs e) {
        if (!isActivated) {
            isActivated = true;
            MoveObject(activatedLocalPosition);
        }
    }

    private void OnPlateDeactivate(object sender, EventArgs e) {
        if (isActivated) {
            isActivated = false;
            MoveObject(originalLocalPosition); // Move back to original position
        }
    }

    private void MoveObject(Vector3 targetLocalPosition) {
        Debug.Log("Moving to: " + targetLocalPosition); // Debugging
        if (isMoving)
            StopAllCoroutines(); // Stop any ongoing movement

        StartCoroutine(MoveCoroutine(targetLocalPosition));
    }


    private IEnumerator MoveCoroutine(Vector3 targetLocalPosition) {
        isMoving = true;
        Vector3 startPos = objectToMove.localPosition;
        Vector3 endPos = originalLocalPosition + targetLocalPosition;

        float journeyLength = Vector3.Distance(startPos, endPos);
        float startTime = Time.time;

        Debug.Log("End Position: " + endPos); // Debugging

        while (true) {
            float distCovered = (Time.time - startTime) * moveSpeed;
            float fracJourney = distCovered / journeyLength;
            objectToMove.localPosition = Vector3.Lerp(startPos, endPos, fracJourney);
            if (fracJourney >= 1f)
                break;
            yield return null;
        }

        objectToMove.localPosition = endPos; // Ensure final position is precise
        isMoving = false;
    }
}
