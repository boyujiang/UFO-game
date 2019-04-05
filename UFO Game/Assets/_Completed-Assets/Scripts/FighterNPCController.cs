﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterNPCController : NPCController
{
    // Start is called before the first frame update
    private readonly float projectileLatencyS = 1.0f;
    private float timeSinceLastProjectile = 0f;
    public GameObject projectile;
    Vector2 projectilePos;

    private void Start()
    {
        //projectile = Resources.Load("Projectile") as GameObject;
        base.characterVelocity = base.characterVelocity * 3;
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
