using UnityEngine;


public class CameraController : MonoBehaviour
{
    public Transform Player;

    [Header("Approximate time for the camera to refocus")]
    public float m_DampTime = 0.2f;
 
    [Header("Zoom Speed")]  // Used with the SmoothDamp function.
    [Range(0.5f, 4f)]
    public float maxSpeed = 2f;

    [Header("The smallest orthographic size the camera can be.")]
    public float m_MinSize = 6.5f;
    [Header("The biggest orthographic size the camera can be.")]
    public float m_MaxSize = 20f;

    private Camera m_Camera;                        
    private float m_ZoomSpeed;                      // Reference speed for the smooth damping of the orthographic size.
    private Vector3 m_MoveVelocity;                 // Reference velocity for the smooth damping of the position.
    private Vector3 m_DesiredPosition;              // The position the camera is moving towards.

    private void Awake ()
    {
        m_Camera = GetComponentInChildren<Camera> ();
        maxSpeed *= 100f; // Only big values work well with the SmoothDamp..
    }

    private void Update ()
    {
        Move();
        Zoom();
    }

    private void Move ()
    {
        Vector3 averagePos = Player.position;
        averagePos.y = transform.position.y;

        transform.position = Vector3.SmoothDamp(transform.position, averagePos, ref m_MoveVelocity, m_DampTime);
    }

    void Zoom()
    {
        float requiredSize = m_Camera.orthographicSize;

        if (Input.mouseScrollDelta.y != 0f)
            requiredSize += (Time.deltaTime * 1000f * -Mathf.Sign(Input.mouseScrollDelta.y));

        requiredSize = Mathf.Clamp(requiredSize, m_MinSize, m_MaxSize);
        m_Camera.orthographicSize = Mathf.SmoothDamp(m_Camera.orthographicSize, requiredSize, ref m_ZoomSpeed, m_DampTime, maxSpeed);
    }
}