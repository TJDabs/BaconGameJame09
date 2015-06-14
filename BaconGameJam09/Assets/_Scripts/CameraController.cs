using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public Transform Player;
    public Camera Camera;

    public Vector2 Margin;
    public Vector2 Smoothing;

    public BoxCollider2D Bounds;

    private Vector3 _min;
    private Vector3 _max;

    public bool IsFollowing = false;

    private static CameraController _instance;

    public static CameraController Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

    public void Setup(Transform player)
    {
        Debug.Log("Setting up");
        Camera.enabled = false;
        Camera.enabled = true;
        Player = player;
        _min = Bounds.bounds.min;
        _max = Bounds.bounds.max;
        IsFollowing = true;
    }

    public void Update()
    {
        var x = transform.position.x;
        var y = transform.position.y;

        if (IsFollowing)
        {
            if (Mathf.Abs(x - Player.position.x) > Margin.x)
            {
                x = Mathf.Lerp(x, Player.position.x, Smoothing.x * Time.deltaTime);
            }

            if (Mathf.Abs(y - Player.position.y) > Margin.y)
            {
                y = Mathf.Lerp(y, Player.position.y, Smoothing.y * Time.deltaTime);
            }

            var cameraHalfWidth = Camera.orthographicSize * ((float)Screen.width / Screen.height);

            x = Mathf.Clamp(x, _min.x + cameraHalfWidth, _max.x - cameraHalfWidth);
            y = Mathf.Clamp(y, _min.y + Camera.orthographicSize, _max.y - Camera.orthographicSize);

            transform.position = new Vector3(x, y, transform.position.z);
        }
    }
}
