using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    public Vector3 target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void FocusOnTarget()
    {
        if (target != Vector3.zero)
        {
            transform.LookAt(target+Vector3.up);
            Vector3 desiredPosition = target + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            Debug.Log(smoothedPosition);
            transform.position = smoothedPosition;
        }
    }
    void LateUpdate()
    {
        FocusOnTarget();
    }
}
