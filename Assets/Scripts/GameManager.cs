using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject _reportWindow;
    [SerializeField] TextMeshProUGUI _message;
    // Start is called before the first frame update
    private void Awake()
    {
        
    }
    void Start()
    {
        StartCoroutine(HideReportWindow());
    }

    IEnumerator HideReportWindow()
    {
        yield return new WaitForSeconds(4);
        _reportWindow.active = false;
        StartCoroutine(ShowReportWindow("Stockholm", 5));
    }


   IEnumerator ShowReportWindow(string city, float secondsBeforeHide)
    {
        yield return new WaitForSeconds(secondsBeforeHide);
        _message.text = "Hey, Cap!\nThe next cargo\nis in " + city.ToString() + " >>> ";
        _reportWindow.active = true;
    }

}
