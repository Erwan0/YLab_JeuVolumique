/*
PROJET JEU VOLUMIQUE
ADRIEN MONTCHER
17/03/2020
*/
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public InputField P;
    public TouchScreenKeyboard clavier;

    void Start()
    {
        P = GameObject.Find("InputPseudo").GetComponent<InputField>();
        if (PlayerPrefs.HasKey("Pseudo"))
        {
            P.text=PlayerPrefs.GetString("Pseudo");
        }
    }
    
    public void Ouvrirclavier()
    {
        clavier = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
    }
    public void MenuversRegles()
    {
        Handheld.Vibrate();
        P = GameObject.Find("InputPseudo").GetComponent<InputField>();
        string Pseudo = P.text;
        if (Pseudo != "")
        {
            PlayerPrefs.SetString("Pseudo", Pseudo);
            SceneManager.LoadScene("Regles", LoadSceneMode.Single);
        }
    }
    public void MenuversResultats()
    {
        SceneManager.LoadScene("Resultats", LoadSceneMode.Single);
    }
    public void MenuversOptions()
    {
        SceneManager.LoadScene("Options", LoadSceneMode.Single);
    }
    public void QuitterJeu()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }


}
