using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class bed : MonoBehaviour
{
    public GameObject TV;
    public Canvas canvas;
    public TextMeshProUGUI dayString;

    private string weekText;
    private string[] weekList;

    public float fadeDuration = 2.0f;
    public float stayDuration = 5.0f;
    private float startTime;
    private bool fadingIn;
    private bool fadingOut;


    // Start is called before the first frame update
    void Start()
    {
        weekList = new string[]
        {
            "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"
        };
        canvas.GetComponent<CanvasGroup>().alpha = 0f;
    }

    public void sleep()
    {
        Debug.Log("hi");
        eventsController tele = GameObject.Find("TV").GetComponent<eventsController>();
        string weekText = weekList[tele.returnN()];
        dayString.text = weekText;


        float elapsedTime = Time.time - startTime;

        if (fadingIn)
        {
            canvas.GetComponent<CanvasGroup>().alpha = Mathf.Clamp01(elapsedTime / fadeDuration);

            if (elapsedTime >= fadeDuration)
            {
                fadingIn = false;
                startTime = Time.time;
            }
        }
        else if (!fadingIn && !fadingOut)
        {
            if (elapsedTime >= stayDuration)
            {
                fadingOut = true;
                startTime = Time.time;
            }
        }
        else if (fadingOut)
        {
            canvas.GetComponent<CanvasGroup>().alpha = 1f - Mathf.Clamp01((elapsedTime - stayDuration) / fadeDuration);

            if (elapsedTime - stayDuration >= fadeDuration)
            {
                return;
            }
        }
    }
}
