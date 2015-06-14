using UnityEngine;
using System.Collections;

public class Player : Photon.MonoBehaviour
{
	[SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _force = 10.0f;
    [SerializeField] private AudioClip _hurtSound;
    [SerializeField] private AudioClip _shootSound;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private GameObject _projectile;
    [SerializeField] private float _projectileSpeed = 10.0f;
    [SerializeField] private float _slowAmount = 10.0f;
    [SerializeField] private float _slowDuration = 2.0f;
    [SerializeField] private tk2dTextMesh _nameText;

    private void Awake()
    {
        if (photonView.isMine)
        {
            CameraController.Instance.Setup(transform);
        }
        _nameText.text = photonView.owner.name;
        RacingGame.Instance.TryShowStartButton();
    }

	private void FixedUpdate()
	{
        if (photonView.isMine && RacingGame.Instance.GameStarted)
        {
            var direction = Vector3.zero;
            direction.x = Input.GetAxis("Horizontal");
            direction.y = Input.GetAxis("Vertical");

            _rigidbody.AddForce(direction * _force);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                photonView.RPC("ShootHarpoon", PhotonTargets.All);
            }
        }
        RotateToMovingDirection();
        UpdateFacingDirection();
	}

    private void RotateToMovingDirection()
    {
        Vector2 moveDirection = _rigidbody.velocity;
        if (moveDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            _rigidbody.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    private void UpdateFacingDirection()
    {
        if( _rigidbody.velocity.x > 0.2f )
        {
            _rigidbody.transform.localScale = Vector3.Lerp(transform.localScale, new Vector3( 1, 1, 1 ), Time.deltaTime * 5);
            _nameText.scale = Vector3.Lerp(_nameText.scale, new Vector3( 1, 1, 1 ), Time.deltaTime * 5);

        }
        else if( _rigidbody.velocity.x < -0.2f )
        {
            _rigidbody.transform.localScale = Vector3.Lerp(transform.localScale, new Vector3( 1, -1, 1 ), Time.deltaTime * 5);
            _nameText.scale = Vector3.Lerp(_nameText.scale, new Vector3( -1, 1, 1 ), Time.deltaTime * 5);
        }
    }

    [RPC]
    private void ShootHarpoon()
    {
        Debug.Log("Shoot RPC");
        AudioManager.Instance.PlayClip(_shootSound);
        var projectile = Instantiate(_projectile, _spawnPoint.position, Quaternion.identity) as GameObject;
        projectile.GetComponent<HarpoonProjectile>().OwnerId = photonView.ownerId;
        projectile.GetComponent<Rigidbody2D>().AddForce(_spawnPoint.right * _projectileSpeed);
    }

    [RPC]
    private void GotHitByHarpoon()
    {
        Debug.Log("Got Hit");
        AudioManager.Instance.PlayClip(_hurtSound);
        StartCoroutine(SlowRoutine());
    }

    private IEnumerator SlowRoutine()
    {
        _rigidbody.drag = _slowAmount;
        yield return new WaitForSeconds(_slowDuration);
        _rigidbody.drag = 1.0f;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (photonView.isMine)
        {
            if (other.name == "FinishLine")
            {
                other.enabled = false;
                RacingGame.Instance.EndGame(PhotonNetwork.playerName);
            }
        }
    }
}
