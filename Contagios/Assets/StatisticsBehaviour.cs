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
    public Text dias;

    public GameManager gameManager;

    private static int CantidadContagiados;
    private static int CantidadSanos;

    private int total;
    private float time = 0f;
    public float day = 5f;

    // Start is called before the first frame update
    void Start()
    {
        total = gameManager.CantidadPersonas;
        CantidadSanos = total;
        CantidadContagiados = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        contagiados.text = ((float) CantidadContagiados * 100 / (float) total).ToString("F1") + "%";
        sanos.text = ((float) CantidadSanos * 100 / (float) total).ToString("F1") + "%";

        dias.text = "Días: " + (time / day).ToString("F1") + " días";
    }

    public static void MeContagie()
    {
        CantidadContagiados = CantidadContagiados + 1;
        CantidadSanos = CantidadSanos - 1;
    }
}
