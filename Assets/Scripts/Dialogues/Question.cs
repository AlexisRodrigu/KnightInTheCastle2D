using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityExtensions;

public struct Options
{
    [TextArea(2, 4)]
    public string option; //OPCION
    public Conversation convResult; //RESULTADO DE LA CONVERSACION AL ELEGIR UNA OPCION
}

[CreateAssetMenu(fileName = "Question", menuName = "Dialogue System / New Question", order = 2)]
public class Question : ScriptableObject
{
    [TextArea(3, 5)]
    public string question; //PREGUNTA 
    [ReorderableList]
    public Options[] options; //ARRAY DE LAS OPCIONES IGUAL MARCADAS COMO LISTA ORDENADA 
}