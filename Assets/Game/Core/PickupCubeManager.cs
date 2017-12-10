﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using Random = UnityEngine.Random;

public class PickupCubeManager : MonoBehaviour
{
    public PickupCube pickupCubePrefab;

    public SkillDatabase skillDatabase;

    [NonSerialized]
    public Transform pickupHolder;

    [NonSerialized]
    public Transform newSkillsHolder;

    void Start()
    {
        pickupHolder = (new GameObject("Pickup Holder")).transform;
        newSkillsHolder = (new GameObject("Skills Holder")).transform;
    }

    public void SpawnPickup(Monster monster)
    {
        var randomNum = Random.Range(0, 10);

        if (randomNum <= 1)
        {
            var pos = monster.transform.position + new Vector3(0, .5f, 0);
            var pickup = Instantiate(pickupCubePrefab, pos, Quaternion.identity);
            pickup.transform.SetParent(pickupHolder);

            pickup.speed = monster.speed;
        }
    }

    public void ActivatePickupCube()
    {
        var player = GameManager.instance.player;
        var skill = GenerateRandomSkill(player);

        player.SetNewSkill(skill);

        GameManager.instance.scoreManager.AddScore(20);
    }

    public Skill GenerateRandomSkill(Unit owner)
    {
        int numSkills = skillDatabase.allSkills.Length;

        Skill skill = null;

        while(skill == null)
        {
            skill = skillDatabase.allSkills[Random.Range(0, numSkills)];
        }        

        Skill newSkill = Instantiate(skill, newSkillsHolder); //create a new skill from prefab
        newSkill.Initialize(owner);

        return newSkill;
    }
}
