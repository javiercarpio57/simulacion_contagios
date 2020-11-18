using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PersonStatus
{
    HEALTHY,
    SICK,
    DIED,
    TREATMENT
}

public class Person : MonoBehaviour
{
    public PersonStatus status = PersonStatus.HEALTHY;

    public Material sickMaterial;
    public Material healthyMaterial;
    public Material inTreatmentMaterial;
    public Material diedMaterial;

    public void ChangeStatus(PersonStatus newStatus)
    {
        if (newStatus == PersonStatus.HEALTHY)
        {
            GetComponent<Renderer>().material = healthyMaterial;
        }
        else if (newStatus == PersonStatus.SICK)
        {
            GetComponent<Renderer>().material = sickMaterial;
            StatisticsBehaviour.MeContagie();
        }
        else if (newStatus == PersonStatus.TREATMENT)
        {
            GetComponent<Renderer>().material = inTreatmentMaterial;
        }
        else if (newStatus == PersonStatus.DIED)
        {
            GetComponent<Renderer>().material = diedMaterial;
        }
        status = newStatus;
    }
}
