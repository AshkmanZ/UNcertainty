using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueHandler : MonoBehaviour
{
    public Image dialogueBox;
    public TextMeshProUGUI dialogueText;
    public string[] Dialogue;

    private void Start()
    {
        StartCoroutine(ShowDialogue(Dialogue));
    }

    public IEnumerator ShowDialogue(string [] Dialogue)
    {
        foreach (string dialogue in Dialogue)
        {

            foreach (char letter in dialogue)
            {
                dialogueText.text += letter;
                yield return new WaitForSeconds(0.2f);
            }
            yield return new WaitForSeconds(2);

            dialogueText.text = "";
        }
    }
}
