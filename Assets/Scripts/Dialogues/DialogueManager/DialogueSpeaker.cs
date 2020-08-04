using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityExtensions;

//CONTROLADOR DE LOS DIALOGOS QUE TIENE CADA PERSONAJE
public class DialogueSpeaker : MonoBehaviour
{
    [ReorderableList]
    public List<Conversation> dialogueAvailable = new List<Conversation>();
    [SerializeField] private int indexDialogue = 0; //RECORRE CADA CONVERSACION DENTRO DE LA LISTA speechAvailable
    [SerializeField] private int dialogueLocalIn = 0; // RECORRE CADA DIALOGO DENTRO DE LA CONVESACION ACTUAL
    public int DialogueLocalIn { get => dialogueLocalIn; set => dialogueLocalIn = value; }

    void Start()
    {
        //INICIALIZAMOS AMBOS VALORES A 0
        indexDialogue = 0;
        dialogueLocalIn = 0;

        ///SOLO PARA PROBAR
        // foreach (var conv in dialogueAvailable)
        // {
        //     conv.finished = false;
        //     var quest = conv.question;
        //     if (quest != null)
        //     {
        //         foreach (var option in quest.options)
        //         {
        //             option.convResult.finished = false;
        //         }
        //     }
        // }
        /// PROBAR
    }

    void Update()
    {

    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player") //&& PRESIONAMOS UN BOTON) //&&presional el boton)
        {
            Speak();
        }
    }

    public void Speak()
    {
        //VERIFICACION SI EL INDEX DE DIALOGOS ES MENOR O IGUAL A LA CANTIDAD DE CONVERSACIONES MENOS 1 
        if (indexDialogue <= dialogueAvailable.Count - 1)
        {
            //SI LA CONVERSACION ESTA DESBLOQUEADA
            if (dialogueAvailable[indexDialogue].unlock)
            {
                if (dialogueAvailable[indexDialogue].finished)
                {
                    //SI LA CONVERSACION RETURN ES TRUE
                    if (UpdateMySpeech())
                    {
                        DialogueManager.instance.ShowUI(true);//MOSTRAMOS LA UI
                        DialogueManager.instance.SetConversation(dialogueAvailable[indexDialogue], this);
                    }
                    DialogueManager.instance.SetConversation(dialogueAvailable[indexDialogue], this);
                    return;
                }
                DialogueManager.instance.ShowUI(true);
                DialogueManager.instance.SetConversation(dialogueAvailable[indexDialogue], this);//SETEAMOS LA CONVERSACION
            }
            else
            {
                //DECIMOS QUE LA CONVERSACION ESTA BLOQUEADA
                Debug.LogWarning("La conversacion esta bloqueada");
                DialogueManager.instance.ShowUI(false);//CERRAMOS LA UI
            }

        }
        //SINO FINALIZA EL DIALOGO Y DESACTIVAMOS EL SHOWUI
        else
        {
            print("Fin del dialogo");
            DialogueManager.instance.ShowUI(false);
        }
    }
    //FUNCION QUE ACTUALIZA EL SPECH
    bool UpdateMySpeech()
    {
        //SI NO SE PUEDE REUTILIZAR LA CONVERSACION
        if (!dialogueAvailable[indexDialogue].reUse)
        {
            if (indexDialogue < dialogueAvailable.Count - 1)
            {
                indexDialogue++;//AUMENTAMOS EL INDEX UNO Y 
                return true; //DEVOLVERNOS TRUE PARA PASAR A LA SIGUIENTE CONVERSACION
            }
            else
            {
                return false;
            }
        }
        else
        {
            //SI SI SE PUEDE REUSAR ES TRUE
            return true;
        }
    }
    //AL SALIR SE DESACTIVA
     private void OnTriggerExit2D(Collider2D other) {
         if(other.gameObject.tag =="Player")
         {
             DialogueManager.instance.ShowUI (false);
         }
    }
}