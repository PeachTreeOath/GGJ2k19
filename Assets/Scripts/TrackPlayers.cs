using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPlayers : MonoBehaviour
{
    [SerializeField] 
    float boundingBoxPadding = 2f;

    [SerializeField]
    float minimumOrthographicSize = 3f;

    [SerializeField]
    float zoomSpeed = 20f;

    private Camera camera;

    private void Awake () 
    {
        camera = GetComponent<Camera>();
        camera.orthographic = true;
    }

    private void LateUpdate()
    {
        Rect boundingBox = CalculatePlayersBoundingBox();
        Vector3 cameraPosition = CalculateCameraPosition(boundingBox);;

        if (!float.IsNaN(cameraPosition.x) && !float.IsNaN(cameraPosition.y))
        {
            transform.position = cameraPosition;
            camera.orthographicSize = CalculateOrthographicSize(boundingBox);
        }
    }

    /// <summary>
    /// Calculates a bounding box that contains all the players.
    /// </summary>
    /// <returns>A Rect containing all the players.</returns>
    private Rect CalculatePlayersBoundingBox()
    {
        float minX = Mathf.Infinity;
        float maxX = Mathf.NegativeInfinity;
        float minY = Mathf.Infinity;
        float maxY = Mathf.NegativeInfinity;

        foreach (var player in GameObject.FindGameObjectsWithTag("Player"))
        {
            Vector3 position = player.GetComponent<Transform>().position;

            minX = Mathf.Min(minX, position.x);
            minY = Mathf.Min(minY, position.y);
            maxX = Mathf.Max(maxX, position.x);
            maxY = Mathf.Max(maxY, position.y);
        }
        return Rect.MinMaxRect(minX - boundingBoxPadding, maxY + boundingBoxPadding, maxX + boundingBoxPadding, minY - boundingBoxPadding);
    }

    /// <summary>
    /// Calculates a camera position given the a bounding box containing all the players.
    /// </summary>
    /// <param name="boundingBox">A Rect bounding box containg all players.</param>
    /// <returns>A Vector3 in the center of the bounding box.</returns>
    private Vector3 CalculateCameraPosition(Rect boundingBox)
    {
        Vector2 boundingBoxCenter = boundingBox.center;

        return new Vector3(boundingBoxCenter.x, boundingBoxCenter.y, camera.transform.position.z);
    }

    /// <summary>
    /// Calculates a new orthographic size for the camera based on the target bounding box.
    /// </summary>
    /// <param name="boundingBox">A Rect bounding box containg all players.</param>
    /// <returns>A float for the orthographic size.</returns>
    private float CalculateOrthographicSize(Rect boundingBox)
    {
        float orthographicSize = camera.orthographicSize;
        Vector3 topRight = new Vector3(boundingBox.x + boundingBox.width, boundingBox.y, 0f);
        Vector3 topRightAsViewport = camera.WorldToViewportPoint(topRight);
       
        if (topRightAsViewport.x >= topRightAsViewport.y)
            orthographicSize = Mathf.Abs(boundingBox.width) / camera.aspect / 2f;
        else
            orthographicSize = Mathf.Abs(boundingBox.height) / 2f;

        return Mathf.Clamp(Mathf.Lerp(camera.orthographicSize, orthographicSize, Time.deltaTime * zoomSpeed), minimumOrthographicSize, Mathf.Infinity);
    }
}