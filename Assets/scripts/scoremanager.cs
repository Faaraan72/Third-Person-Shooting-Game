using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class scoremanager : MonoBehaviour
{
    public int pkills;
    public int enekills;
    public Text enekilltxt;
    public Text pkilltxt;
    public Text maintext;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("pkills"))
        {
            pkills = PlayerPrefs.GetInt("0");
        }
        if (PlayerPrefs.HasKey("enekills"))
        {
            enekills = PlayerPrefs.GetInt("0");
        }
    }
    private void Update()
    {
        StartCoroutine(winorloose());
    }
    IEnumerator winorloose()
    {
        pkilltxt.text = "" + pkills;
        enekilltxt.text = "" + enekills;

        if(pkills >= 10)
        {
            //Time.timeScale = 0f;
            pkilltxt.text = "10";
            maintext.text = "BLUE TEAM VICTORY";
            PlayerPrefs.SetInt("pkills", pkills);
            yield return new WaitForSeconds(4f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            Cursor.lockState = CursorLockMode.None;
            //

        }
        else if(enekills >= 10)
        {
           // Time.timeScale = 0f;
            enekilltxt.text = "10";
            maintext.text = "RED TEAM VICTORY";
            PlayerPrefs.SetInt("enekills", enekills);
            yield return new WaitForSeconds(4f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            Cursor.lockState = CursorLockMode.None;
            //

        }


    }
}
