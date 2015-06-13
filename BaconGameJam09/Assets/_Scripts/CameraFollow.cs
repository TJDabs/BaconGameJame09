using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour 
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _zDepth = -1;

	private void Update ()
    {
        transform.position = new Vector3(_target.position.x, _target.position.y, _target.position.z - _zDepth);
	}
}
