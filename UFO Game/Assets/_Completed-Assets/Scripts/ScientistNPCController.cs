using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScientistNPCController : NPCController
{
    private readonly float projectileLatencyS = 1.2f;
    private float timeSinceLastProjectile = 0f;
    public int score = 10;
    public GameObject projectile;
    Vector2 projectilePos;

    private void Start()
    {
        //projectile = Resources.Load("Projectile") as GameObject;
        base.Start();
    }

    void CreateProjectile()
    {
        projectilePos = this.transform.position;
        Instantiate(projectile, projectilePos, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - projectileLatencyS > timeSinceLastProjectile)
        {
            CreateProjectile();
            timeSinceLastProjectile = Time.time;
        }
        base.Update();
    }
}
