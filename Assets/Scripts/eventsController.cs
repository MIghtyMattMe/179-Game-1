using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eventsController : MonoBehaviour
{
    private string[] events;
    private int n;
    private bool active;

    // Start is called before the first frame update
    void Start()
    {
        active = true;
        events = new string[]
        {
            "Why you should be fearful about loosening migrant policies; is your job at risk?",
            "Hide your children: Helpless christian woman harassed by drag marchers during pride rally",
            "Armed masked bandits terrorizing white teens near school in Dem-run city",
            "state defunds police, gang violence escalates: 'There's nothing we can do",
            "Man charged over threats to shoot up mega market: What's next?", 
            "Serial killer still at large: reasons why it could be your neighbor",
            "Burglary last night: Dem homeowner fails to defend home: You are next."
        };
        int n = 0;
    }

    //return events, and then pop or trigger door event
    public string returnEvent()
    {
        string output = events[n];
        n += 1;
        active = false;
        return output;
    }

    public int returnN()
    {
        return n;
    }

    public void reactivate()
    {
        active = true;
    }
}
