using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour 
{
    [SerializeField] private List<Transform> _spawnPoints;

    private static SpawnManager _instance;

    public static SpawnManager Instance
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

    public void SpawnPlayer()
    {
        StartCoroutine(SpawnPlayerRoutine());
    }

    private IEnumerator SpawnPlayerRoutine()
    {
        yield return new WaitForSeconds(2.0f);
        foreach (var spawn in _spawnPoints)
        {
            var collided = Physics2D.OverlapCircleAll(spawn.position, 1, 1 << LayerMask.NameToLayer("Player"));
            Debug.Log("Collidered with " + collided.Length);
            if (collided == null || collided.Length == 0)
            {
                Debug.Log("Found Spawn " + spawn.gameObject.name);
                PhotonNetwork.Instantiate("Player", spawn.position, Quaternion.identity, 0);
                yield break;
            }
        }
    }
}
