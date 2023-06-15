using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class JuegoMemoria : MonoBehaviour
{
    //[Header("Valores")]

    //variables
    /// <summary>
    /// m_NumCards: guarda el numero de cartas en pantalla (las creadas para el nivel)
    /// m_NumeroPares: guarda el numero de pares ya encontrados
    /// m_Level: guarda el numero del nivel actual
    /// 
    /// m_playArea: guarda el numero de filas y columnas del nivel
    /// m_SpaceBetweenCards: variable utilizada para calcular y guardar el espacio entre las cartas
    /// m_cardSize: variable para guardar las medidas de la carta (64,88)
    /// m_cardScale: variable que se usa para calcular y guardar la escala de todas cartas del nivel
    /// m_MaxSize: se usa para guardar el maximo tamaño en escala en los ejes x e y
    /// 
    /// maxSize: variable usada para guardar el tamaño maximo de la carta ya que la escala puede ser diferente en x e y . Por lo tanto si hay diferencia se toma la menor de las 2
    /// m_Time: variable utilizada por el temporizador para saber el tiempo que le queda al jugador de juego
    /// 
    /// m_IsPlaying: variable utilizada para saber si el juego esta en marcha o no
    /// 
    /// Cards: Lista para guardas todas las cartas del nivel
    /// FacedUpCards: Lista que guarda todas las cartas que estan cara arriba
    /// CardsIds: Lista creada para la mezcla y asignacion de Ids a las cartas
    /// </summary>

    private int m_NumCards = 0;
    private int m_NumeroPares = 0;
    private int m_Level = 0;

    private Vector2Int m_playArea = new Vector2Int(16, 16);
    public Vector2 m_SpaceBetweenCards = new Vector2(64, 88);
    private Vector2 m_cardSize = new Vector2(64, 88);
    public Vector2 m_cardScale = new Vector2(0, 0);
    private Vector2 m_MaxSize /*= Vector2.zero*/;

    private float maxSize = 0;
    private float m_Time = 60;

    private bool m_IsPlaying = true;

    private List<GameObject> Cards;
    private List<CardScript> FacedUpCards;
    public List<int> CardsIds;

    [Header("References")]
    [Tooltip("Prefab of the used card"), SerializeField] GameObject m_Card;
    [Tooltip("RectTransform of the play Area where the cards are gonna be placed"), SerializeField] RectTransform m_PlayAreaTranform;
    [Tooltip("TextMeshPro used for beeing a timer"), SerializeField] TextMeshProUGUI Timer;
    [Tooltip("TextMeshPro used for showing the level the user is currently playing"), SerializeField] TextMeshProUGUI Level;
    [Tooltip("Button for Reset game the Play Area -> start on lvl 0"), SerializeField] Button ResetButton;



    public int m_lastId = 0;
    private int m_lastLevelCardCount = 0;
    private float m_initialTime = 3;
    [SerializeField] TextMeshProUGUI m_initialTimer;












    void Start()
    {
        Cards = new List<GameObject>();
        FacedUpCards = new List<CardScript>();
        CardsIds = new List<int>();
        createCards();
        ResetButton.onClick.AddListener(delegate { restart(); });
    }


    // Update is called once per frame
    void Update()
    {
        //miramos si estamos jugando
        if (m_IsPlaying)
        {
            //si estamos jugando empieza el temporizador
            CountDown();
            //y miramos si hemos ganado
            if (m_NumeroPares == (m_NumCards / 2))
            {
                //reseteamos la variable de numero de pares a 0 ahora para que no se vuelva a repetir el invoke hasta que se termine el segundo
                m_NumeroPares = 0;
                Debug.Log("You Win");
                foreach (GameObject card in Cards)
                {
                    card.GetComponent<CardScript>().MostrarReverso();
                }
                Invoke("NextLevel", 1f);
            }
        }
        else
        {

        }
    }



    //crear Cartas
    /// <summary>
    /// createCards : funcion que crea las cartas (necesita un retoque para que cree las cartas faltantes solo)
    /// MezclaIds : funcion que crea todos los Ids necesarios y los mezcla
    /// RemoveCards : funcion que elimina todas las cartas al actualizar o cambiar de nivel (necesita un retoque para que elimine las cartas sobrantes solo)
    /// </summary>
    /// 

    //cambios necesarios para que cree cartas ya sea al principio o las necesarias segun las que falten por crear
    private void createCards()
    {
        m_NumCards = m_playArea.x * m_playArea.y;
        MezclaIds();
        m_MaxSize = new Vector2(m_PlayAreaTranform.rect.width, m_PlayAreaTranform.rect.height);
        //Debug.Log(m_Card.transform.localScale);
        //Debug.Log(m_MaxSize);

        //El Area de juego esta en la mitad de la pantalla, calculamos el tamaño del tablero para que se encuentre en el medio

        m_cardScale = CardSize();
        //*2 para que el espacio entre cartas sea de otra carta
        m_SpaceBetweenCards = new Vector2((m_cardSize.x * m_cardScale.x) * 2f, (m_cardSize.y * m_cardScale.y) * 2f);
        Vector2 CardInitialPos = CalcCardPos();
        for (int x = m_lastLevelCardCount; x < m_NumCards; x++)
        {
            GameObject CardGO = Instantiate(m_Card, new Vector3(0, 0, 0), Quaternion.identity);
            Cards.Add(CardGO);
            CardGO.GetComponent<CardScript>().setGameManagerJMemoria(this);
            CardGO.transform.SetParent(m_PlayAreaTranform);
        }
        //creamos la cartas
        for (int x = 0; x < m_playArea.x; x++)
        {
            for (int y = 0; y < m_playArea.y; y++)
            {
                //Debug.Log(y + (m_playArea.y * x));
                Cards[y + (m_playArea.y * x)].GetComponent<CardScript>().changeName(CardsIds[(m_playArea.y * x) + y].ToString());
                //*1,5 ya que las cartas quedan muy pequeñas
                Cards[y + (m_playArea.y * x)].transform.localScale = new Vector3(maxSize*1.5f, maxSize*1.5f, 0.1f);
                Cards[y + (m_playArea.y * x)].transform.localPosition = new Vector3((x * (m_SpaceBetweenCards.x)) - CardInitialPos.x, (y * m_SpaceBetweenCards.y) - CardInitialPos.y, 0);
            }
        }
        m_lastLevelCardCount = m_NumCards;


        //mostrar las cartas al inicio y quitarlo
        ActivateCards(false);
        InitialTimer();


    }

    private void MezclaIds()
    {
        //List<int> idsCards = new List<int>();
        {
            int i;
            for (i = m_lastId; i < m_NumCards; i++)
            {
                //Debug.Log("i: "+i);
                CardsIds.Add(i / 2);
            }
            m_lastId = i;
            Shuffle(CardsIds);
            //CardsIds
        }
    }

    private void ResetIds() {
        m_lastId = 0;
        for (int i = CardsIds.Count - 1;i >= 0; i--)
        {
            CardsIds.RemoveAt(i);
        }
        MezclaIds();
    }

    private void RefreshCards()
    {
        //RemoveCards();
        createCards();
    }


    //a cambiar, en vez de eliminar las cartas, mirar cuantas se necesitan crear y reutilizar las ya creadas
    private void RemoveCards()
    {
        m_NumCards = m_playArea.x * m_playArea.y;
        /*foreach (GameObject go in Cards)
        {
            Destroy(go);
        }*/
        Debug.Log(Cards.Count);
        for (var i = (m_lastLevelCardCount-1); i >= (m_NumCards); i--)
        {
            Destroy(Cards[i]);
            Cards.RemoveAt(i);
        }
        m_lastLevelCardCount = m_NumCards;
    }





    //POSICION
    /// <summary>
    /// funcion CalcCardPos calcula la posicion media de todo el area de juego para posicionar las Cartas
    /// funcion CardSize calcula el tamaño de cada carta
    /// </summary>

    private Vector2 CalcCardPos()
    {
        float posicionX = (m_playArea.x-1) * m_SpaceBetweenCards.x;
        float posicionY = (m_playArea.y-1) * m_SpaceBetweenCards.y;

        return new Vector2(posicionX / 2, posicionY / 2);
    }

    private Vector2 CardSize()
    {
        float maxSizeX = m_MaxSize.x / (m_playArea.x * (m_cardSize.x * 2f));
        float maxSizeY = m_MaxSize.y / (m_playArea.y * (m_cardSize.y * 2f));

        maxSize = 0;
        if (maxSizeX > maxSizeY)
            maxSize = maxSizeY;
        else
            maxSize = maxSizeX;
        //Debug.Log("maxSizeX: " + maxSizeX + ", maxSizeY:" + maxSizeY);

        return new Vector2(maxSizeX, maxSizeY);
    }




    //TEMPORIZADOR
    /// <summary>
    /// funcion COUNTDOWN calcula el tiempo
    /// funcion DISPLAYTIME enseña el tiempo en pantalla
    /// </summary>
    private void CountDown()
    {

        if (m_Time > 0)
        {
            m_Time -= Time.deltaTime;
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

        Timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }



    //PAREJAS
    /// <summary>
    /// facedUp: funcion que se llama al clickear sobre una carta y esta se pone cara arriba. Se encarga de cuardar las cartas
    ///     boca arriba y de ver si son pares
    /// resetCards : Corutina para dar la vuelta a las cartas y mostrar el reverso. Se hace despues de un segundo
    /// </summary>

    public void facedUp(CardScript GO)
    {
        FacedUpCards.Add(GO);
        //Debug.Log(GO);
        if (FacedUpCards.Count % 2 == 0)
        {
            if (String.Compare(FacedUpCards[0].getName(), FacedUpCards[1].getName()) == 0)
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

    private IEnumerator resetCards(CardScript c1, CardScript c2)
    {
        yield return new WaitForSeconds(1);
        c1.MostrarReverso();
        c2.MostrarReverso();
        /*foreach (CardScript face in FacedUpCards)
        {
            face.MostrarReverso();
            FacedUpCards.Remove(face);
        }*/

    }

    //LEVELS
    /// <summary>
    /// NextLevel : funcion utilizada para cambiar de nivel . Altera la suma de 2 filas en la x y 1 en la y . Y resetea las variables necesarias
    /// </summary>


    private void NextLevel()
    {
        if (m_Level % 2 == 0)
            m_playArea.x += 2;
        else
            m_playArea.y++;
        RefreshCards();
        m_Time = 60;
        m_Level++;
        Level.text = "Level " + m_Level;

    }

    public void startGame()
    {
        m_IsPlaying = true;
    }
    
    public void stopGame()
    {
        m_IsPlaying = false;
    }

    public void restart()
    {
        m_playArea.x = 2;
        m_playArea.y = 2;
        RemoveCards();
        ResetIds();
        createCards();
        m_Time = 60;
        m_Level=0;
        Level.text = "Level " + m_Level;
    }



    public void ActivateCards(bool activate)
    {
        foreach(GameObject Card in Cards)
        {
            Card.GetComponent<CardScript>().ActivateReverse(activate);
        }
    }

    public void InitialTimer()
    {



        ActivateCards(true);
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