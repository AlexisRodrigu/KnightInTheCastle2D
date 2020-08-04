using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityExtensions;//LIBRERIA PARA USAR LAS LISTA ORDENADA

[CreateAssetMenu(fileName = "Conversation", menuName = "Dialogue System / New Conversation", order = 1)]
public class Conversation : ScriptableObject
{
    [System.Serializable] //VISUALIZAMOS EN EL INSPECTOR
    public struct Speech //ESTRUCTURA LLAMADA SPEECH
    {
        public Character character; //REFERENCIAMOS AL CHARACTER QUE DICE LA LINEA
        public AudioClip sound; //SI QUEREMOS QUE TENGA O SE ESCUCHE UN AUDIO (OPCIONAL)

        [TextArea(3, 5)]//
        public string dialogue; //TEXTO DEL DIALGO
    }
    public bool unlock;
    public bool finished;//CUANDO EL DIALOGO TERMINE
    public bool reUse;
    [ReorderableList] //EXTENSION DESCARGADA DE GIT
    //CON ESTA LISTA PODEMOS SEPARAR LAS LISTAS Y MOVERLAS MAS FACIL
    public Speech[] dialogues; //LISTA O ARRAY DE DIALOGOS
    /////////
    public Question question; //REFERENCIAMOS A LAS PREGUNTAS(OPCIONAL)
}
