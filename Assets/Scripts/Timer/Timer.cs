using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Timer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]

    private TextMeshProUGUI Text;

    [SerializeField]

    private float speed = 1;

    private float TimerDisplay = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(speed > 0)
        {
            TimerDisplay += Time.deltaTime * speed;
            Text.SetText(TimerDisplay.ToString("N2"));
        }
    }
}
