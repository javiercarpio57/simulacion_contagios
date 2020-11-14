using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int CantidadPersonas = 100;
    public Person person;
    public int LeftWall;
    public int RightWall;
    public int DownWall;
    public int UpWall;

    // Start is called before the first frame update
    void Start()
    {
        for (var i = 0; i <= CantidadPersonas; i++)
        {
            GameObject newPerson = Instantiate(person, null).gameObject;
            float x = Random.Range(LeftWall, RightWall);
            float z = Random.Range(DownWall, UpWall);
            newPerson.transform.position = new Vector3(x, 0.4f, z);
        }
    }
}
