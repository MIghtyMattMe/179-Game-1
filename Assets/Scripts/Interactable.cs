using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    [SerializeField] private string words = "Test string\nSecond Line\nThird Line";
    private string blackOutStart = "<mark=#000000ff>";
    private string blackOutEnd = "</mark>";
    private string blackOut;
    private TextMeshProUGUI frontText;
    private TextMeshProUGUI backText;
    [HideInInspector] public bool typing = false;
    // Start is called before the first frame update
    void Start()
    {
        frontText = GameObject.Find("FrontText").GetComponent<TextMeshProUGUI>();
        backText = GameObject.Find("BackText").GetComponent<TextMeshProUGUI>();
        //TypeWords();
    }

    public void TypeWords()
    {
        StopAllCoroutines();
        char[] chars = words.ToCharArray();
        blackOut = blackOutStart + words + blackOutEnd;
        backText.text = blackOut;
        frontText.text = "";
        StartCoroutine(CharByChar(chars));
    }

    IEnumerator CharByChar(char[] chars)
    {
        typing = true;
        foreach (char c in chars)
        {
            frontText.text += c;
            //speed of characters printing
            yield return new WaitForSeconds(0.1f);
        }
        typing = false;
        //cooldown before dissapearing
        yield return new WaitForSeconds(3);
        backText.text = "";
        frontText.text = "";
    }
}
