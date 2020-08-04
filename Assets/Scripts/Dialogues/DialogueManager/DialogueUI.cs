using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//ENCARGADO DE LA PARTE VISUAL DEL DIALOGO
public class DialogueUI : MonoBehaviour
{
    public Conversation conversationScript;
    [SerializeField] private float textSpeed = 6.0f; //VELOCIDAD CON LA QUE SE ESCRIBIRA EL TEXTO
    [SerializeField] private GameObject speechContainerGO;//GO CONVERSATION
    [SerializeField] private GameObject questionContainerGO;//GO QUESTION

    [SerializeField] private Image speakImg;//CONTIENE LA IMAGEN DEL PERSONAJE QUE ESTE HABLANDO
    [SerializeField] private TextMeshProUGUI nameTxt; //TxtCharacterName
    [SerializeField] private TextMeshProUGUI conversationTxt; //TxtDialogue
    [SerializeField] private Button btnNext; //
    [SerializeField] private Button btnBefore;
    private AudioSource audioSource;//AUDIO (OPCIONAL) QUE CONTENDRA AL DECIR EL DIALOGO

    [SerializeField] private int localIn = 0; //RECORRE CADA DIALOGO DENTRO DE LA CONVERSACION (conversationCcript), 
                                                //ADOPTA EL VALOR EN BASE AL QUE TENGA PUESTO EL DIALOGUESPEAKER AL MOMENTO DE HABLAR

    public int LocalIn { get => localIn; set => localIn = value; }

    

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
        speechContainerGO.SetActive(true);
        questionContainerGO.SetActive(false);

        btnNext.gameObject.SetActive(true);
        btnBefore.gameObject.SetActive(false);
    }

    void Update()
    {
        // //if (presionamos un boton)
        // {
        //     UpdateSpeech(1);
        // }
    }
    public void UpdateSpeech(int behaviour)//ACTUALIZA EL DIALOGO EN BASE AL COMPORTAMIENTO
    {
        speechContainerGO.SetActive(true);//ACTIVAMOS EL GO DEL SPEECH 
        questionContainerGO.SetActive(false); //DESACTIVAMOS EL GO DE LAS PREGUNTAS

        //INICIAMOS UN SWITCH PARA LOS DIFERENTES COMPORTAMIENTOS (BEHAVIOUR)
        switch (behaviour)
        {
            //-1 PARA RETROCEDER AL DIALOGO ANTERIOR
            case -1:
                if (localIn > 0) ///Si local in es mayor a 0 va al array en 0 Y VA A LA PRIMER LINEA DEL DIALOGO SI SE CUMPLE PDEMOS RETROCEDER SINO ROMPO EL SWITCH
                {
                    print("Dialogo anterior");
                    localIn--;//RESTAMOS 1 AL LOCALIN PARA RETROCEDER AL DIALOGO ANTERIOR
                    nameTxt.text = conversationScript.dialogues[localIn].character.nameCharacter; //EL NAME.TEXT = NOMBRE DEL CHARACTER QUE ESTE HABLANDO
                    StopAllCoroutines(); //DETENEMOS TODAS LAS CORRUTINAS
                    StartCoroutine(WriteText()); //COMENZAMOS LA CORRUTINA PARA ESCRIBIR EL TEXTO QUE DA EL EFECTO QUE SE ESCRIBA LETRA POR LETRA (PODEMOS BORRAR SI NO QUEREMOS USAR)
                    speakImg.sprite = conversationScript.dialogues[localIn].character.imgSprite; //ACEDEMOS A LA IMAGEN QUE TIENE NUESTRO PERSONAJE

                    //SI PUSIMOS UN AUDIO Y ESTE NO ES NULO
                    if (conversationScript.dialogues[localIn].sound != null)
                    {
                        audioSource.Stop(); //PARAMOS UN AUDIO QUE SE ESTE EJECUTANDO EN EL MOMENTO
                        audioSource.PlayOneShot(conversationScript.dialogues[localIn].sound);//REPRODUCE EL AUDIO QUE CONTIENE EL SCRIPTABLE OBJECT EN BASE A LA CONVERSACION
                    }
                    btnBefore.gameObject.SetActive(localIn > 0); //SI EL LOCALIN > 0 ES TRUE Y SE ACTIVA EL BOTON
                }
                //ACCEDEMOS 
                DialogueManager.speakerActual.DialogueLocalIn = localIn;; //IGUALAMOS EL DIALLOCAL INT PARA QUE AMBOS SEAN 0 Y SEPAN QUE ESTAMOS EN EL PRIMER DIALOGO
                break;

            // 0 => PARA EL DIALOGO ACTUAL
            case 0:
                print("Dialogo actualizado");
                nameTxt.text = conversationScript.dialogues[localIn].character.nameCharacter; //PASAMOS EL NOMBRE DEL PERSONAJE QUE AHORA HABLARA
                StopAllCoroutines(); //PARAMOS TODAS LAS CORRUTINAS
                StartCoroutine(WriteText()); //INICIAMOS LA CORRUTINA QUE ESCRIBIRA EL TEXTO  LETRA POR LETRA
                speakImg.sprite = conversationScript.dialogues[localIn].character.imgSprite; //CAMBIAMOS A LA IMAGEN DEL PERSONAJE QUE HABLARA
                
                //LO MISMO QUE LO ANTERIOR SI EL AUDIO NO ES NULL REPRODUCE
                if (conversationScript.dialogues[localIn].sound != null)
                {
                    audioSource.Stop();
                    var audio = conversationScript.dialogues[localIn].sound;
                    audioSource.PlayOneShot(audio);
                }
                //SI LOCAL IN ES MAYOR A LA LONGITUD DE DIALOGOS MENOS UNO (PARA NO PASARNOS DEL ARRAY) YA QUE COMENZAMOS DESDE 0
                if (localIn >= conversationScript.dialogues.Length - 1)
                    btnNext.GetComponentInChildren<TextMeshProUGUI>().text = "Finalizar";//ACCEDEMOS AL COMPONENTE TEXTO DLE BOTON Y LOS CAMBIAMOS
                else
                    btnNext.GetComponentInChildren<TextMeshProUGUI>().text = "Continuar"; //<color = red> Continuar </color> PODEMOS PONERLO ASI PARA RESALTAR EN UN COLOR
                break;

            // 1 PARA EL DIALOGO SIGUIENTE
            case 1:
                //EL -1 ES PARA EVITAR QUE SALGA EL INDEX DEL ARRAY DE LOS DIALOGOS
                if (localIn < conversationScript.dialogues.Length - 1)
                {
                    print("Dialogo siguiente"); 
                    localIn++;//SUMAMOS UNO
                    nameTxt.text = conversationScript.dialogues[localIn].character.nameCharacter; //NOMBRE DEL PERSONAJE
                    StopAllCoroutines();//PARAMOS CORRUTINAS
                    StartCoroutine(WriteText()); //INICIAMOS CORRUTINA WRITETEXT
                    conversationTxt.text = conversationScript.dialogues[localIn].dialogue; //LA CONVERSACION QUE TENDRA
                    speakImg.sprite = conversationScript.dialogues[localIn].character.imgSprite;//IMAGEN DEL PERSONAJE
                    //SI EL SONIDO NO ES NULO SE REPRODUCE UN SONIDO
                    if (conversationScript.dialogues[localIn].sound != null)
                    {
                        audioSource.Stop();
                        var audio = conversationScript.dialogues[localIn].sound;
                        audioSource.PlayOneShot(audio);
                    }
                    //VERIFICACION PARA ACTIVAR EL BOTON
                    btnBefore.gameObject.SetActive(localIn > 0);
                    //CONDICIONAL PARA FINALIZAR O CONTINUAR
                    if (localIn >= conversationScript.dialogues.Length - 1)
                        btnNext.GetComponentInChildren<TextMeshProUGUI>().text = "Finalizar";
                    else
                        btnBefore.GetComponentInChildren<TextMeshProUGUI>().text = "Continuar";
                }
                else
                {
                    print("dialogo terminado");
                    localIn = 0;//RESETEAMOS EL VALOR DEL LOCAL IN
                    DialogueManager.speakerActual.DialogueLocalIn = 0;//DE IGUAL FORMA RESETEAMOS EL VALOR
                    conversationScript.finished = true;//EL DIALOGO HA SIDO FINALIZADO
                    ///
                    ///SI EN LA CONVERSACION HAY UNA PREGUNTA
                    if(conversationScript.question != null)
                    {
                        speechContainerGO.SetActive(false); //DESACTIVA EL GO DEL SPEECH
                        questionContainerGO.SetActive (true); //ACTIVA EL GO DE LA PREGUNTA
                        var newQuestion = conversationScript.question; // VARIABLE QUE GUARDA LA PREGUNTA
                        DialogueManager.instance.questionController.ActiveButtons( newQuestion.options.Length, newQuestion.question, newQuestion.options);  //LLAMAMOS AL CONTROLADOR PREGUNTAS Y LA FUNCION DE ACTIVAR BOTONES
                        return;
                    }
                    ///    
                }
                DialogueManager.instance.ShowUI(false);
                return;
                default : 
                Debug.LogWarning("Estas pasando mas valores, solo se admiten valores de -1 a 1");
                break;
        }
    }

    IEnumerator WriteText()
    {
        conversationTxt.maxVisibleCharacters = 0;  //ACCEDEMOS AL TEXTO DE LA CONVERASACION Y VACIAMOS  OTRA FORMA => conversationTxt.text = string.Empty;//
        conversationTxt.text = conversationScript.dialogues[localIn].dialogue; //LA CONVERSACION ES IGUAL A LA CONVERSACION EN EL SCRIPT DIALOGUES
        conversationTxt.richText = true; 

        //ESTE FOR AGARRA CADA CARACTER DE LA PALABRA Y LA RECORRE
        for(int i = 0; i < conversationScript.dialogues[localIn].dialogue.ToCharArray().Length; i++ )
        {
            conversationTxt.maxVisibleCharacters++; //POR CADA CARACTER QUE HAYA EN LA PALABRA SE EMPEZARA A ESCRIBIR LETRA POR LETRA
            yield return new WaitForSeconds(1.0f / textSpeed); //DIVIDIMOS 1 SEGUNDO ENTRE LA VELOCIDAD DEL TEXTO
        }
    }
}
