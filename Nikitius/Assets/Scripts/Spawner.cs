using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    bool spawn = true;

    [SerializeField] GameObject[] meteorPrefabArray;
    [SerializeField] Vector3 target;

    [SerializeField] float minSpawnDelay = 5f;
    [SerializeField] float maxSpawnDelay = 1f;
    
    [SerializeField] float minSpeedOfRotation = 1f;
    [SerializeField] float maxSpeedOfRotation = 10;

    IEnumerator Start()
    {
       
        while(spawn)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
            SpawnMeteor();
        }
    }
    private void Update()
    {
        RotatateArountEarth();
    }
    private void SpawnMeteor()
    {
        var meteorIndex = Random.Range(0, meteorPrefabArray.Length);
        Spawn(meteorPrefabArray[meteorIndex]);
    }

    private void Spawn(GameObject myMeteor)
    {
        GameObject newMeteor = Instantiate(myMeteor, transform.position, transform.rotation);
    }

    private void RotatateArountEarth()
    {
        transform.RotateAround(target, Vector3.forward, Random.Range(minSpeedOfRotation, maxSpeedOfRotation) * Time.deltaTime);
    }
  
    

}
