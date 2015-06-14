using UnityEngine;
using System.Collections;

public class HarpoonProjectile : MonoBehaviour
{
    public int OwnerId;

    private Rigidbody2D _rigidbody;

	private void Start () 
    {
        _rigidbody = GetComponent<Rigidbody2D>();
	}
	
	void Update () 
    {
        RotateToMovingDirection();
	}

    private void RotateToMovingDirection()
    {
        Vector2 moveDirection = _rigidbody.velocity;
        if (moveDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.transform.tag == "Player")
        {
            Debug.Log("Collided with Player");
            if (coll.gameObject.GetComponent<PhotonView>().ownerId != OwnerId)
            {
                Debug.Log("Collided with player that is not me");
                coll.transform.GetComponent<PhotonView>().RPC("GotHitByHarpoon", PhotonTargets.All);
            }
        }
    }
}
