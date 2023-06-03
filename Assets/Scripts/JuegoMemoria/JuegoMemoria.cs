using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JuegoMemoria : MonoBehaviour
{
    [Header("Valores")]
    [SerializeField] int m_AreaJuegoX = 2;
    [SerializeField] int m_AreaJuegoY = 2;
    [SerializeField] Vector2 SeparacionEntreFichas = Vector2.zero;

    [Header("Referencias")]
    [SerializeField] GameObject m_Ficha;
    [SerializeField] Transform m_AreaJuego;

    void Start()
    {
        //El Area de juego esta en la mitad de la pantalla, calculamos el tamaño del tablero para que se encuentre en el medio
        Vector2 posicionInicialFicha = CalcularPosicionFicha();

        //creamos la fichas
        for(int x=0; x < m_AreaJuegoX; x++)
        {
            for(int y=0; y< m_AreaJuegoY; y++)
            {
                GameObject fichaGO = Instantiate(m_Ficha);
                fichaGO.transform.SetParent(m_AreaJuego);
                fichaGO.transform.localPosition = new Vector3((x * SeparacionEntreFichas.x)-posicionInicialFicha.x, 0, (y * SeparacionEntreFichas.y)-posicionInicialFicha.y);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Vector2 CalcularPosicionFicha()
    {
        float posicionX = (m_AreaJuegoX - 1) * SeparacionEntreFichas.x;
        float posicionY = (m_AreaJuegoY - 1) * SeparacionEntreFichas.y;

        return new Vector2(posicionX/2,posicionY/2);
    }
}