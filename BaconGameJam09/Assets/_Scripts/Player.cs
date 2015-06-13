using UnityEngine;
using System.Collections;

public class Player : Photon.MonoBehaviour
{
	[SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _force = 1.0f;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _collideSound;

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

    private void OnCollisionEnter2D(Collision2D coll)
    {
        _audioSource.PlayOneShot(_collideSound);
    }
}
