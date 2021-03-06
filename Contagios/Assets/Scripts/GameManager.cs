﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int CantidadPersonas = 1000;

    public Person person;
    public int LeftWall = -10;
    public int RightWall = 10;
    public int DownWall = -6;
    public int UpWall = 6;

    private Person last;

    // Start is called before the first frame update
    void Start()
    {
        Person newPerson;
        for (var i = 0; i <= CantidadPersonas; i++)
        {
            newPerson = Instantiate(person, transform);
            float x = Random.Range(LeftWall, RightWall);
            float z = Random.Range(DownWall, UpWall);
            newPerson.gameObject.transform.position = new Vector3(x, 0.4f, z);
            newPerson.ChangeStatus(PersonStatus.HEALTHY);

            if (i == CantidadPersonas)
            {
                last = newPerson;
            }
        }

        StartCoroutine(StartSimulation(last));
    }

    IEnumerator StartSimulation(Person lastPerson)
    {
        yield return new WaitForSecondsRealtime(5f);
        print("PRIMER CONTAGIO");
        lastPerson.ChangeStatus(PersonStatus.SICK);
    }
}
