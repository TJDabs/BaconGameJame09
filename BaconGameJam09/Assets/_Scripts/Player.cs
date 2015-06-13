using UnityEngine;
using System.Collections;

public class Player : Photon.MonoBehaviour
{
	[SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _force = 1.0f;

	private void FixedUpdate()
	{
        if (photonView.isMine)
        {
            var direction = Vector3.zero;
            direction.x = Input.GetAxis("Horizontal");
            direction.y = Input.GetAxis("Vertical");

            _rigidbody.AddForce(direction * _force);
        }
	}
}
