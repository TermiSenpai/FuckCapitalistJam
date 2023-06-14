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
    [SerializeField] int m_NumeroFichas = 0;
    [SerializeField] int m_NumeroPares = 0;
    /*[SerializeField]*/ Vector2 m_SpaceBetweenChips = new Vector2(64,88);
    Vector2 m_cardSize = new Vector2(64, 88);
    Vector2 m_cardScale = new Vector2(0, 0);
    /*[SerializeField]*/ Vector2 m_MaxSize /*= Vector2.zero*/;
    float maxSize = 0;

    [Header("Referencias")]
    [SerializeField] GameObject m_Chip;
    [SerializeField] RectTransform m_PlayArea;
    [SerializeField] TextMeshProUGUI Timer;
    [SerializeField] TextMeshProUGUI Level;
    [SerializeField] Button ResfreshButton;


    private float m_Time = 60;

    private List<GameObject> Cards;
    public List<Ficha> FacedUpCards;
    public List<int> CardsIds;

    public int m_Level = 0;

    public bool m_IsPlaying = true;

    void Start()
    {
        Cards = new List<GameObject>();
        FacedUpCards = new List<Ficha>();
        createCards();
        ResfreshButton.onClick.AddListener(delegate { RefreshCards(); });
    }


    // Update is called once per frame
    void Update()
    {
        if (m_IsPlaying)
        {
            CountDown();
            if (m_NumeroPares == (m_NumeroFichas / 2))
            {
                m_NumeroPares = 0;
                Debug.Log("You Win");
                Invoke("NextLevel", 1f);
            }
        }
    }



    //crear Cartas
    /// <summary>
    /// createCards
    /// MezclaIds
    /// RemoveCards
    /// </summary>
    /// 

    //cambios necesarios para que cree cartas ya sea al principio o las necesarias segun las que falten por crear
    private void createCards()
    {
        m_NumeroFichas = m_PlayAreaX * m_PlayAreaY;
        MezclaIds();
        m_MaxSize = new Vector2(m_PlayArea.rect.width, m_PlayArea.rect.height);
        Debug.Log(m_Chip.transform.localScale);
        Debug.Log(m_MaxSize);

        //El Area de juego esta en la mitad de la pantalla, calculamos el tamaño del tablero para que se encuentre en el medio

        m_cardScale = ChipSize();
        m_SpaceBetweenChips = new Vector2((m_cardSize.x * m_cardScale.x)*2f, (m_cardSize.y * m_cardScale.y)*2f);
        Vector2 posicionInicialFicha = CalcularPosicionFicha();
        //creamos la fichas
        for (int x = 0; x < m_PlayAreaX; x++)
        {
            for (int y = 0; y < m_PlayAreaY; y++)
            {
                GameObject fichaGO = Instantiate(m_Chip, new Vector3(0, 0, 0), Quaternion.identity);
                fichaGO.GetComponent<Ficha>().changeName(CardsIds[(m_PlayAreaY * x) + y].ToString());
                fichaGO.GetComponent<Ficha>().setGameManagerJMemoria(this);
                Cards.Add(fichaGO);
                fichaGO.transform.localScale = new Vector3(maxSize, maxSize, 0.1f);

                fichaGO.transform.SetParent(m_PlayArea);
                fichaGO.transform.localPosition = new Vector3((x * (m_SpaceBetweenChips.x)) - posicionInicialFicha.x, (y * m_SpaceBetweenChips.y) - posicionInicialFicha.y, 0);
            }
        }
    }

    private void MezclaIds()
    {
        List<int> idsCards = new List<int>();
        {
            for(int i = 0;i < m_NumeroFichas; i++)
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


    //a cambiar, en vez de eliminar las fichar, mirar cuantas se necesitan crear y reutilizar las ya creadas
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
        float maxSizeX = m_MaxSize.x/ (m_PlayAreaX*(m_cardSize.x*2f));
        float maxSizeY = m_MaxSize.y / (m_PlayAreaY*(m_cardSize.y*2f));

        maxSize = 0;
        if (maxSizeX > maxSizeY)
            maxSize = maxSizeY;
        else
            maxSize = maxSizeX;
        Debug.Log("maxSizeX: "+maxSizeX+", maxSizeY:"+maxSizeY);

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



    public void facedUp(Ficha GO)
    {
        FacedUpCards.Add(GO);
        Debug.Log(GO);
        if(FacedUpCards.Count%2== 0)
        {
            if (String.Compare(FacedUpCards[0].getName(), FacedUpCards[1].getName())==0)
            {
                Debug.Log("par");
                m_NumeroPares++;
                
            }
            else
            {
                //Invoke("resetCards", 1f);
                StartCoroutine(resetCards(FacedUpCards[0], FacedUpCards[1]));
            }
            FacedUpCards.RemoveAt(1);
            FacedUpCards.RemoveAt(0);

        }
    }

    private IEnumerator resetCards(Ficha c1,Ficha c2)
    {
        yield return new WaitForSeconds(1);
        c1.MostrarReverso();
        c2.MostrarReverso();
        /*foreach (Ficha face in FacedUpCards)
        {
            face.MostrarReverso();
            FacedUpCards.Remove(face);
        }*/
        
    }


    private void NextLevel()
    {
        if(m_Level%2==0)
            m_PlayAreaX+=2;
        else
            m_PlayAreaY++;
        RefreshCards();
        m_Time = 60;
        m_Level++;
        Level.text = "Level "+m_Level;
        
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