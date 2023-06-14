using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class CardScript : MonoBehaviour
{
    [Tooltip("Animator responsable de las animaciones"),SerializeField] private Animator m_Animator;
    private string m_Name;
    [Tooltip("TextMeshPro para poner los numeros de los pares"),SerializeField] private TextMeshProUGUI m_text;
    [Tooltip("Parte de la cara"),SerializeField] private Button m_caraButton;
    private bool faceUp = false;
    private JuegoMemoria GameManagerJMemoria;
    //[SerializeField] private Image cara;
    private int veces = 0;


    // Start is called before the first frame update
    void Start()
    {

    }



    // Update is called once per frame
    void Update()
    {
        
    }

    /*private void OnMouseDown()
    {
        Debug.Log("clicked");
        m_Animator.Play("FichaFrenteAlReverso");
        this.transform.localScale = new Vector3(0.5f, 0.1f, 0.5f);
        
    }*/

    //funcion utilizada para poner los numeros a cada carta
    public void changeName(string name) { 
        m_Name = name;
        m_text.text = name; 
    }

    public string getName()
    {
        return m_Name;
    }

    //funcion para mostrar la cara del frente
    public void MostrarFrente()
    {
        Debug.Log("clicked");
        m_Animator.Play("FichaReversoAlFrente");
        m_caraButton.enabled = false;
        faceUp = true;
        GameManagerJMemoria.facedUp(this);
        //Invoke("changeColor", 0.5f);
    }

    public void setGameManagerJMemoria(JuegoMemoria JM)
    {
        GameManagerJMemoria = JM;
    }

/*
    private void changeColor()
    {
        if(veces%2==0)
        cara.color = new Color32(255, 255, 225, 100);
        else
        cara.color = new Color32(255, 0, 0, 100);

        veces++;
    }*/

    //funcion para mostrar el reverso de la carta
    public void MostrarReverso()
    {
        Debug.Log("clicked cara");
        m_Animator.Play("FichaFrenteAlReverso");
        m_caraButton.enabled = true;
        faceUp = false;
        //Invoke("changeColor", 0.5f);
    }

}
