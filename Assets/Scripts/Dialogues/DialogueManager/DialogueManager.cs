using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ENCARGA DE LAS FUNCIONES QUE HAY QUE MOSTRAR 
public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance { get; private set; }
    public static DialogueSpeaker speakerActual;
    [SerializeField] private DialogueUI dialogueUIScript; //dialUI
    [SerializeField] private GameObject player;

    public QuestionController questionController;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        dialogueUIScript = FindObjectOfType<DialogueUI>();

        questionController = FindObjectOfType<QuestionController>();
    }
    void Start()
    {
        ShowUI(false);
        //    player.GetComponent<DialogueSpeaker>().Speak();
    }

    public void ShowUI(bool show)
    {
        dialogueUIScript.gameObject.SetActive(false);
        if (!show)
            dialogueUIScript.LocalIn = 0;
        //VALORES DE MOVIMIENTO POR DEFECTO DEL PLAYER YA QUE NO SE ESTA MOSTRANDO
        //else //SINO Y SE ESTA MOSTRANDO
        //
        //AQUI VA LA LOGICA PARA INHABILITAR EL MOVIMIENTO DEL JUGADOR PARA QUE PERMANEZCA QUIETO
        //

    }
    public void SetConversation(Conversation conv, DialogueSpeaker speaker)
    {
        //SI EL SPEAKER NO ES NULO
        if (speaker != null)
        {
            speakerActual = speaker; //SPEAKER ACTUAL SE IGUAL AL SPEAKER QUE SE PASA COMO PARAMETRO
        }
        else
        {
            //SI EL SPEAKER ES NULL QUIERE DECIR QUE VIENE UNA PREGUNTA
            //RESEREAMOT EL LOCAL IN PARA RECORRER LA CONVERSACION EN PRODUCTO DE LA RESPUESTA ELEGIDA
            dialogueUIScript.conversationScript = conv; //IGUALAMOS A LA CONVERSACION QUE PASAMOS EN EL PARAMETRO
            dialogueUIScript.LocalIn = 0; // RESEEAMOS LOS DIALOGOS DE LA CONVERSACION
            dialogueUIScript.UpdateSpeech(0); //LLAMAMOS A LA FUNCION QUE ACTUALIZARA Y LE PASAMOS EL SWITCH CASE 0 
        }
        //CONDICIONAL PARA VER SI LA CONVERSACION YA FINALIZO  Y LA CONVERSACION NO SE REUTILIZARA
        if (conv.finished && !conv.reUse)
        {
            dialogueUIScript.conversationScript = conv; //PASAMOS LA NUEVA CONVERSACION
            dialogueUIScript.LocalIn = conv.dialogues.Length; //LOCAL IN = TOTAL DE DIALOGOS PARA SABER QUE ESTAMOS AL TOPE 
            dialogueUIScript.UpdateSpeech(1);//LAMAMOS AL SWITCH CASE 1 EN EL DIALOGUE UI SCRIPT
        }
        else //SINO
        {
            dialogueUIScript.conversationScript = conv; //IGUALAMOS AL PARAMETRO
            dialogueUIScript.LocalIn = speakerActual.DialogueLocalIn; //DIALOGUE SCRIPT LO IGUALAMOS AL DIALOGO ACTUAL
            dialogueUIScript.UpdateSpeech(0); //LLAMAMOS A LA FUNCION QUE ACTUALIZARA Y LE PASAMOS EL SWITCH CASE 0 
        }
    }

    //FUNCION PARA CAMBIAR EL STADO DE REUSAR SOLO SE USA PARA PROBAR
    public void ChangeStateReusable (Conversation conv, bool desiredState) //ESTADO DESEADO
    {
        conv.reUse = desiredState;
    }

    //FUNCION PARA LLAMAR Y DESBLOQUEAR CUALQUIER CONVERSACION
    public void BlockAndUnlockOfConversation(Conversation conv, bool unlocked)
    {   
        conv.unlock = unlocked;
    }
}
