using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartRoom : Room
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    override public void ChooseEnemySpawns()
    {
        Debug.Log("No enemy spawns because this is the starting room");
    }
}
