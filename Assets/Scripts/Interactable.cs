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
    [SerializeField] private AudioClip interactSound;
    public bool loopAudio = false;
    public TMP_FontAsset newFont;
    public float FontSize = 24f;
    public Vector2 newPosition = new Vector2(0f, -100f);
    public float volume = 1.0f;
    private string blackOutStart = "<mark=#000000ff>";
    private string blackOutEnd = "</mark>";
    private string blackOut;
    private TextMeshProUGUI frontText;
    private TextMeshProUGUI backText;
    private AudioSource soundPlayer;
    [HideInInspector] public bool typing = false;
    // Start is called before the first frame update
    void Start()
    {
        frontText = GameObject.Find("FrontText").GetComponent<TextMeshProUGUI>();
        backText = GameObject.Find("BackText").GetComponent<TextMeshProUGUI>();
        soundPlayer = gameObject.AddComponent<AudioSource>();
    }

    public void TypeWords(string input)
    {
        StopAllCoroutines();
        char[] chars = input.ToCharArray();
        // blackOut = blackOutStart + input + blackOutEnd;
        backText.text = "";
        frontText.text = "";
        StartCoroutine(CharByChar(chars));
        if (interactSound != null)
        {
            soundPlayer.clip = interactSound;
            soundPlayer.loop = loopAudio;
            soundPlayer.volume = volume;
            soundPlayer.Play();
        }
    }

    IEnumerator CharByChar(char[] chars)
    {
        frontText.font = newFont;
        backText.font = newFont;
        frontText.fontSize = FontSize;
        backText.fontSize = FontSize;
        RectTransform posFront = frontText.rectTransform;
        posFront.anchoredPosition = newPosition;
        RectTransform posBack = backText.rectTransform;
        posBack.anchoredPosition = newPosition;
        posFront.sizeDelta = new Vector2(500f, 50f);
        posBack.sizeDelta = new Vector2(500f, 50f);
        backText.text += "<align=center><b>";
        frontText.text += "<align=center><b>";

        typing = true;
        foreach (char c in chars)
        {
            backText.text += blackOutStart + c + blackOutEnd;
            frontText.text += c;
            //speed of characters printing
            yield return new WaitForSeconds(0.03f);
            backText.text += "</b>";
            frontText.text += "</b>";
        }
        typing = false;
        soundPlayer.Stop();
        //cooldown before dissapearing
        yield return new WaitForSeconds(3);
        if (frontText.text == words)
        {
            backText.text = "";
            frontText.text = "";
        }
    }
}
