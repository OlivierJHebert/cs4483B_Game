using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlindfireTurret : MonoBehaviour
{
    public GameObject Player;
    public GameObject projectilePrefab;//prefab being "fired"
    public float spawnTime = 1f;//seconds between pellets

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(fireProjectiles());
    }

    private void spawnProjectile()//create new projectile at turret tip
    {
        GameObject p = Instantiate(projectilePrefab) as GameObject;
        p.transform.parent = gameObject.transform;
        p.transform.position = new Vector2(gameObject.transform.position.x / 2, gameObject.transform.position.y);
    }

    IEnumerator fireProjectiles()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime);
            spawnProjectile();
        }
    }


}
