using UnityEngine;
using System.Collections;

public class Player : Photon.MonoBehaviour
{
	[SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _force = 1.0f;
    private Vector3 correctPlayerPos;
    private Quaternion correctPlayerRot;

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

    // Update is called once per frame
    void Update()
    {
        if (!photonView.isMine)
        {
            transform.position = Vector3.Lerp(transform.position, this.correctPlayerPos, Time.deltaTime * 5);
            transform.rotation = Quaternion.Lerp(transform.rotation, this.correctPlayerRot, Time.deltaTime * 5);
        }
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            // We own this player: send the others our data
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);

        }
        else
        {
            // Network player, receive data
            this.correctPlayerPos = (Vector3)stream.ReceiveNext();
            this.correctPlayerRot = (Quaternion)stream.ReceiveNext();
        }
    }
}
