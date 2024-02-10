using UnityEngine;
using System.Collections;
using UnityEditor.EditorTools;

public class FallPlatform : MonoBehaviour
{
    [Tooltip("Speed at which the platform falls and rises")]
    public float Speed = 2f;

    [Tooltip("Delay before the platform falls after the game starts")]
    public float fallDelay = 2f;
    
    [Tooltip("Delay before the platform rises back after falling")]
    public float riseDelay = 2f;

    private Vector3 initialPosition;
    private bool isFalling = false;

    void Start() {
        initialPosition = transform.position;
        Invoke("Fall", fallDelay);
    }

    void Update() {
        if (isFalling) {
            // Move the platform downward
            transform.position += Vector3.down * Speed * Time.deltaTime;
        }
    }

    void Fall() {
        isFalling = true;
        Invoke("Rise", riseDelay);
    }

    void Rise() {
        // Smoothly move the platform back to its initial position
        StartCoroutine(MoveToInitialPosition());
    }

    private IEnumerator MoveToInitialPosition() {
        float t = 0f;
        Vector3 startPos = transform.position;
        
        while (t < 1) {
            t += Time.deltaTime * (1 / Speed);
            transform.position = Vector3.Lerp(startPos, initialPosition, t);
            yield return null;
        }

        transform.position = initialPosition;

        isFalling = false;

        Invoke("Fall", fallDelay);
    }
}
