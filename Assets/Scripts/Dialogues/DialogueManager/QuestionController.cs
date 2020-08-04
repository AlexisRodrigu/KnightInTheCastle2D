using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class QuestionController : MonoBehaviour
{
    private GameObject buttonPref;
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private Transform optionContainer;
    private List<Button> poolButtons = new List<Button>();

    private void Start()
    {

    }
    public void ActiveButtons(int amount, string title, Options[] options)
    {
        questionText.text = title;
        if (poolButtons.Count >= amount)
        {
            for (int i = 0; i < poolButtons.Count; i++)
            {
                if (i < amount)
                {
                    poolButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = options[i].option;
                    poolButtons[i].onClick.RemoveAllListeners();
                    Conversation co = options[i].convResult;
                    poolButtons[i].onClick.AddListener(() => GiveFuncionBtns(co));
                    poolButtons[i].gameObject.SetActive(true);
                }
                else
                {
                    poolButtons[i].gameObject.SetActive(false);
                }
            }
        }else{
            int amountRest = (amount - poolButtons.Count);
            for (int i = 0; i < amountRest; i++)
            {
                var newBtn = Instantiate(buttonPref,optionContainer).GetComponent<Button>();
                newBtn.gameObject.SetActive(true);
                poolButtons.Add(newBtn);
            }
            ActiveButtons(amount,title,options);
        }
    }
    public void GiveFuncionBtns(Conversation conv)
    {
        DialogueManager.instance.SetConversation(conv,null);
    }
}
