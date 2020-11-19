using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PersonStatus
{
    HEALTHY,
    SICK,
    DIED,
    TREATMENT,
    RECOVERED
}

public class Person : MonoBehaviour
{
    public float tiempoEnHospital = 25f;
    public PersonStatus status = PersonStatus.HEALTHY;

    public Material sickMaterial;
    public Material healthyMaterial;
    public Material inTreatmentMaterial;
    public Material diedMaterial;
    public Material recoveredMaterial;

    private float sickTime;
    private float inTreatmentTime;
    private float initialSickTime;

    private void Start()
    {
        sickTime = 0f;
        inTreatmentTime = 0f;
    }

    private void Update()
    {
        if (status == PersonStatus.SICK)
        {
            sickTime += Time.deltaTime;
            
            if (StatisticsBehaviour.PuedoRecibirTratamiento())
            {
                ChangeStatus(PersonStatus.TREATMENT);
                StatisticsBehaviour.RecibirTratamiento();
            }

            if (sickTime >= (6 * StatisticsBehaviour.day))
            {
                if (HasDied())
                {
                    StatisticsBehaviour.Fallecer(PersonStatus.SICK);
                    ChangeStatus(PersonStatus.DIED);
                }
                else
                {
                    StatisticsBehaviour.Recuperar(PersonStatus.SICK);
                    ChangeStatus(PersonStatus.RECOVERED);
                }
            }
        }
        
        if (status == PersonStatus.TREATMENT)
        {
            inTreatmentTime += Time.deltaTime;

            if (inTreatmentTime >= tiempoEnHospital)
            {
                if (HasDied())
                {
                    StatisticsBehaviour.Fallecer(PersonStatus.TREATMENT);
                    ChangeStatus(PersonStatus.DIED);
                }
                else
                {
                    StatisticsBehaviour.Recuperar(PersonStatus.TREATMENT);
                    ChangeStatus(PersonStatus.RECOVERED);
                }
            }
        }
    }

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
        else if (newStatus == PersonStatus.RECOVERED)
        {
            GetComponent<Renderer>().material = recoveredMaterial;
        }
        status = newStatus;
    }

    private bool HasDied()
    {
        if ((sickTime / (float) StatisticsBehaviour.day) > 6)
        {
            if (Random.value < 0.90f)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            if (Random.value < (Mathf.Pow(2, (sickTime / (float) StatisticsBehaviour.day)) + 2) / (float) 100) {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
