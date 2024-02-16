//Last Editor: Caleb Richardson
//Last Edited: Feb 16
using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour{
    [Header("Moving Platform Variables")]
    [Tooltip("Speed that the platform will move from point A to point B.")]
    [SerializeField, Range(0.1f, 3f)] private float movementSpeed = 2f;

    [Tooltip("Delay before the platform moves from point A to B.")]
    [SerializeField, Range(0.5f, 10f)] private float pointADelay = 2f;
    
    [Tooltip("Delay before the platform moves from point B to A.")]
    [SerializeField, Range(0.5f, 10f)] private float pointBDelay = 2f;

    [Header("Required References")]
    [SerializeField] private Transform pointATransform;
    [SerializeField] private Transform pointBTransform;

    [Header("Debugging Variables")]
    [SerializeField] private Color gizmosColor = Color.green;
    [SerializeField] private bool drawGizmos = true;

    private Transform currentPoint;
    private WaitForSecondsRealtime pointATimer;
    private WaitForSecondsRealtime pointBTimer;

    private void Awake() {
        pointATimer = new WaitForSecondsRealtime(pointADelay);
        pointBTimer = new WaitForSecondsRealtime(pointBDelay);

        currentPoint = pointATransform;
        transform.position = pointATransform.position;
    }

    private void Start() {
       StartCoroutine(MovementDelay());
    }

    private void OnDestroy() {
        StopAllCoroutines();
    }

    private IEnumerator MovementDelay(){
        var timer = currentPoint == pointATransform ? pointATimer : pointBTimer;
        var nextPoint = currentPoint == pointATransform ? pointBTransform : pointATransform;
        
        yield return timer;
        StartCoroutine(MoveToPosition(nextPoint));
    }

    private IEnumerator MoveToPosition(Transform positionTransform) {
        float t = 0f;
        
        while (t < 1) {
            t += Time.deltaTime * movementSpeed;
            transform.position = Vector3.Lerp(currentPoint.position, positionTransform.position, t);
            yield return null;
        }

        currentPoint = positionTransform; 
        transform.position = positionTransform.position;

        StartCoroutine(MovementDelay());
    }

    private void OnDrawGizmos() {
        if(!drawGizmos) return;

        Gizmos.color = gizmosColor;

        if(pointATransform != null){
            Gizmos.DrawWireCube(pointATransform.position, transform.localScale);
        }

        if(pointBTransform != null){
            Gizmos.DrawWireCube(pointBTransform.position, transform.localScale);
        }
    }
}
