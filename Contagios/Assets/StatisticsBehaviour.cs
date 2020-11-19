using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatisticsBehaviour : MonoBehaviour
{
    public Text contagiados;
    public Text sanos;
    public Text enTratamiento;
    public Text fallecidos;
    public Text recuperados;
    public Text dias;

    public GameManager gameManager;

    private static int CantidadContagiados;
    private static int CantidadSanos;
    private static int CantidadEnTratamiento;
    private static int CantidadFallecidos;
    private static int CantidadRecuperados;

    private int total;
    private float time = 0f;
    public static float day = 5f;
    
    private static bool canReceivePeople;
    public static int CapacidadHospital = 50;

    // Start is called before the first frame update
    void Start()
    {
        total = gameManager.CantidadPersonas;
        CantidadSanos = total;
        CantidadContagiados = 0;
        CantidadEnTratamiento = 0;
        CantidadFallecidos = 0;
        CantidadRecuperados = 0;

        canReceivePeople = false;

        StartCoroutine(EmpezarARecibirEnHospital());
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        contagiados.text = ((float) CantidadContagiados * 100 / (float) total).ToString("F1") + "%";
        sanos.text = ((float) CantidadSanos * 100 / (float) total).ToString("F1") + "%";
        enTratamiento.text = ((float) CantidadEnTratamiento * 100 / (float) CapacidadHospital).ToString("F1") + "%";
        fallecidos.text = ((float) CantidadFallecidos * 100 / (float) total).ToString("F1") + "%";

        recuperados.text = "Recuperados: " + ((float) CantidadRecuperados * 100 / (float) total).ToString("F1") + "%";
        dias.text = "Días: " + (time / day).ToString("F1") + " días";
    }

    IEnumerator EmpezarARecibirEnHospital()
    {
        yield return new WaitForSecondsRealtime(15f);
        canReceivePeople = true;
    }

    public static void MeContagie()
    {
        CantidadContagiados = CantidadContagiados + 1;
        CantidadSanos = CantidadSanos - 1;
    }
    

    public static bool PuedoRecibirTratamiento()
    {
        if (!canReceivePeople) return false;

        if (CantidadEnTratamiento < CapacidadHospital)
        {
            return true;
        }
        return false;
    }

    public static void RecibirTratamiento()
    {
        CantidadEnTratamiento = CantidadEnTratamiento + 1;
    }

    public static void Fallecer(PersonStatus estado)
    {
        CantidadFallecidos = CantidadFallecidos + 1;
        if (estado == PersonStatus.SICK)
        {
            CantidadContagiados = CantidadContagiados - 1;
        }
        else if (estado == PersonStatus.TREATMENT)
        {
            CantidadEnTratamiento = CantidadEnTratamiento - 1;
        }
    }

    public static void Recuperar(PersonStatus estado)
    {
        CantidadRecuperados = CantidadRecuperados + 1;
        if (estado == PersonStatus.SICK)
        {
            CantidadContagiados = CantidadContagiados - 1;
        }
        else if (estado == PersonStatus.TREATMENT)
        {
            CantidadEnTratamiento = CantidadEnTratamiento - 1;
        }
    }

}
