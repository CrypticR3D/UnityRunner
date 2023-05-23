using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform[] targets; // Reference to the characters to follow
    [SerializeField] private float smoothTime = 0.5f; // Smoothing time for camera movement
    [SerializeField] private Vector3 offset; // Offset from the characters

    [SerializeField] private float minZoom = 10f; // Minimum zoom level
    [SerializeField] private float maxZoom = 20f; // Maximum zoom level
    [SerializeField] private float zoomLerpSpeed = 5f; // Zooming speed

    private Vector3 velocity; // Velocity for smoothing
    private Camera camera;

    private void Start()
    {
        camera = GetComponent<Camera>();
        camera.orthographic = true;
    }

    private void LateUpdate()
    {
        if (targets.Length == 0)
            return;

        // Calculate the center point between the characters
        Vector3 centerPoint = GetCenterPoint();

        // Calculate the desired camera position
        Vector3 desiredPosition = centerPoint + offset;

        // Smoothly move the camera towards the desired position
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);

        // Calculate the desired orthographic size (zoom level) based on the distance between the characters
        float desiredSize = GetDesiredOrthographicSize();

        // Adjust the camera's orthographic size to achieve the desired zoom level
        camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, desiredSize, Time.deltaTime * zoomLerpSpeed);
    }

    private Vector3 GetCenterPoint()
    {
        if (targets.Length == 1)
        {
            return targets[0].position;
        }
        else
        {
            // Find the bounds that encapsulate all the characters
            Bounds bounds = new Bounds(targets[0].position, Vector3.zero);
            for (int i = 1; i < targets.Length; i++)
            {
                bounds.Encapsulate(targets[i].position);
            }

            return bounds.center;
        }
    }

    private float GetDesiredOrthographicSize()
    {
        // Calculate the distance between the characters
        float distance = 0f;
        for (int i = 0; i < targets.Length - 1; i++)
        {
            for (int j = i + 1; j < targets.Length; j++)
            {
                float currDistance = Vector3.Distance(targets[i].position, targets[j].position);
                if (currDistance > distance)
                {
                    distance = currDistance;
                }
            }
        }

        // Map the distance between the characters to an orthographic size between the minimum and maximum zoom
        float desiredSize = Mathf.Lerp(minZoom, maxZoom, distance / maxZoom);
        return desiredSize;
    }

}
