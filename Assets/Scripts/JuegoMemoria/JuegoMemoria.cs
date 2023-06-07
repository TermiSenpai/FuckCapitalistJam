using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JuegoMemoria : MonoBehaviour
{
    [Header("Valores")]
    //public GameObject plano;
    [SerializeField] float m_PlayAreaX = 2;
    [SerializeField] float m_PlayAreaY = 2;
    [SerializeField] Vector2 m_SpaceBetweenChips = Vector2.zero;
    [SerializeField] Vector2 m_MaxSize = Vector2.zero;

    [Header("Referencias")]
    [SerializeField] GameObject m_Chip;
    [SerializeField] Transform m_PlayArea;

    void Start()
    {
        //plano.transform.localScale = new Vector3(m_MaxSize.x,0.1f, m_MaxSize.y);
        Debug.Log(m_Chip.transform.localScale);
        //El Area de juego esta en la mitad de la pantalla, calculamos el tamaño del tablero para que se encuentre en el medio
        
        float chipsize = ChipSize();
        m_SpaceBetweenChips = new Vector2(m_SpaceBetweenChips.x * chipsize, m_SpaceBetweenChips.y * chipsize);
        Vector2 posicionInicialFicha = CalcularPosicionFicha();
        //creamos la fichas
        for (int x=0; x < m_PlayAreaX; x++)
        {
            for(int y=0; y< m_PlayAreaY; y++)
            {
                GameObject fichaGO = Instantiate(m_Chip, new Vector3(0,0,0),Quaternion.identity);
                fichaGO.transform.localScale = new Vector3(chipsize, 0.1f, chipsize);

                fichaGO.transform.SetParent(m_PlayArea);
                fichaGO.transform.localPosition = new Vector3((x * m_SpaceBetweenChips.x)-posicionInicialFicha.x, 0, (y * m_SpaceBetweenChips.y)-posicionInicialFicha.y);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Vector2 CalcularPosicionFicha()
    {
        float posicionX = (m_PlayAreaX - 1) * m_SpaceBetweenChips.x;
        float posicionY = (m_PlayAreaY - 1) * m_SpaceBetweenChips.y;

        return new Vector2(posicionX/2,posicionY/2);
    }

    private float ChipSize()
    {
        float maxSizeX = m_MaxSize.x/ (m_PlayAreaX*m_SpaceBetweenChips.x);
        float maxSizeY = m_MaxSize.y / (m_PlayAreaY * m_SpaceBetweenChips.y);

        float maxSize = 0;
        if (maxSizeX > maxSizeY)
            maxSize = maxSizeY;
        else
            maxSize = maxSizeX;
        Debug.Log(maxSize);

        return maxSize;
    }

}