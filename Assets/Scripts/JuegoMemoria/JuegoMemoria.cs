using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class JuegoMemoria : MonoBehaviour
{
    [Header("Valores")]
    //public GameObject plano;
    [Tooltip("Texto al realizar hover de la variable en el editor") ,SerializeField] Vector2Int playArea;
    [SerializeField] int m_PlayAreaX = 2;
    [SerializeField] int m_PlayAreaY = 2;
    [SerializeField] int m_NumeroPares = 0;
    /*[SerializeField]*/ Vector2 m_SpaceBetweenChips = new Vector2(64,88);
    /*[SerializeField]*/ Vector2 m_MaxSize /*= Vector2.zero*/;

    [Header("Referencias")]
    [SerializeField] GameObject m_Chip;
    [SerializeField] RectTransform m_PlayArea;
    [SerializeField] TextMeshProUGUI Timer;
    [SerializeField] Button ResfreshButton;


    private float m_Time = 3;

    private List<GameObject> Cards;
    public List<int> CardsIds;

    public bool m_IsPlaying = true;

    void Start()
    {
        Cards = new List<GameObject>();
        createCards();
        ResfreshButton.onClick.AddListener(delegate { RefreshCards(); });
    }


    //crear Cartas
    /// <summary>
    /// createCards
    /// MezclaIds
    /// RemoveCards
    /// </summary>
    private void createCards()
    {
        m_NumeroPares = m_PlayAreaX * m_PlayAreaY;
        MezclaIds();
        //CardsIds = MezclaIds();
        m_MaxSize = new Vector2(m_PlayArea.rect.width, m_PlayArea.rect.height);
        //plano.transform.localScale = new Vector3(m_MaxSize.x,0.1f, m_MaxSize.y);
        Debug.Log(m_Chip.transform.localScale);
        Debug.Log(m_MaxSize);

        //El Area de juego esta en la mitad de la pantalla, calculamos el tamaño del tablero para que se encuentre en el medio

        Vector2 chipsize = ChipSize();
        m_SpaceBetweenChips = new Vector2(m_SpaceBetweenChips.x * chipsize.x, m_SpaceBetweenChips.y * chipsize.y);
        Vector2 posicionInicialFicha = CalcularPosicionFicha();
        //creamos la fichas
        for (int x = 0; x < m_PlayAreaX; x++)
        {
            for (int y = 0; y < m_PlayAreaY; y++)
            {
                GameObject fichaGO = Instantiate(m_Chip, new Vector3(0, 0, 0), Quaternion.identity);
                fichaGO.GetComponent<Ficha>().changeName(CardsIds[(m_PlayAreaY * x) + y].ToString());
                Cards.Add(fichaGO);
                fichaGO.transform.localScale = new Vector3(chipsize.x, chipsize.x, 0.1f);

                fichaGO.transform.SetParent(m_PlayArea);
                fichaGO.transform.localPosition = new Vector3((x * m_SpaceBetweenChips.x) - posicionInicialFicha.x, (y * m_SpaceBetweenChips.y) - posicionInicialFicha.y, 0);
            }
        }
    }

    private void MezclaIds()
    {
        List<int> idsCards = new List<int>();
        {
            for(int i = 0;i < m_NumeroPares; i++)
            {
                idsCards.Add(i/2);
            }

            CardsIds = idsCards;
            Shuffle(CardsIds);
            //CardsIds
        }
    }

    private void RefreshCards()
    {
        RemoveCards();
        createCards();
    }

    private void RemoveCards()
    {
        foreach (GameObject go in Cards)
        {
            Destroy(go);
        }
        Debug.Log(Cards.Count);
        for (var i = (Cards.Count-1); i >= 0; i--)
        {
            Cards.RemoveAt(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(m_IsPlaying)
        CountDown();    
    }



    //POSICION
    /// <summary>
    /// funcion CALCULARPOSICIONFICHA calcula la posicion media de todo el area de juego para posicionar las Cartas
    /// funcion CHIPSIZE calcula el tamaño de cada carta
    /// </summary>

    private Vector2 CalcularPosicionFicha()
    {
        float posicionX = (m_PlayAreaX - 1) * m_SpaceBetweenChips.x;
        float posicionY = (m_PlayAreaY - 1) * m_SpaceBetweenChips.y;

        return new Vector2(posicionX/2,posicionY/2);
    }

    private Vector2 ChipSize()
    {
        float maxSizeX = m_MaxSize.x/ (m_PlayAreaX*(m_SpaceBetweenChips.x));
        float maxSizeY = m_MaxSize.y / (m_PlayAreaY * (m_SpaceBetweenChips.y));

        /*float maxSize = 0;
        if (maxSizeX > maxSizeY)
            maxSize = maxSizeY;
        else
            maxSize = maxSizeX;
        Debug.Log(maxSize);*/

        return new Vector2(maxSizeX,maxSizeY);
    }




    //TEMPORIZADOR
    /// <summary>
    /// funcion COUNTDOWN calcula el tiempo
    /// funcion DISPLAYTIME enseña el tiempo en pantalla
    /// </summary>
    private void CountDown()
    {
        
        if(m_Time > 0)
        {
            m_Time -=Time.deltaTime;
        }
        else
        {
            m_Time = 0;
            m_IsPlaying = false;
            Debug.Log("GameOver");
        }

        DisplayTime(m_Time);
    }

    private void DisplayTime(float TimeDisplay)
    {
        if (TimeDisplay < 0)
        {
            TimeDisplay = 0;
        }
        float minutes = Mathf.FloorToInt(TimeDisplay / 60);
        float seconds = Mathf.FloorToInt(TimeDisplay % 60);

        Timer.text = string.Format("{0:00}:{1:00}",minutes,seconds);
    }






    //shuffle
    System.Random rnd = new System.Random();

    public void Shuffle<T>(IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rnd.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}