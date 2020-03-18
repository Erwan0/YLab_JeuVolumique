/*
PROJET JEU VOLUMIQUE
ADRIEN MONTCHER
17/03/2020
*/
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
public class PlayerMove : TacticsMove
{ 
    public GameObject Player;
    public Light Lumière;
    public GameObject Clef;
    public GameObject FausseClef;
    public GameObject Tresor;
    public GameObject Trou;
    public GameObject Torche;
    public GameObject Pic1;
    public GameObject Pic2;
    public GameObject Pic3;
    public GameObject Soin;
    public GameObject Serpent;
    public Image Coeur1;
    public Image Coeur2;
    public Image Coeur3;
    public Text Chrono;
    public Text Tour;
    public Text Evenements;
    public Image ImageClef;
    public float time = 0;
    public int Tpevent = 0;
    public int Ntour = 1;
    public bool clef = false;
    public bool poison = false;
    public int Tpoison = 0;
    public int vie = 3;
    public static string joueur ="player";
    public List<int> l0 = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    public List<int> l1 = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    public List<int> l2 = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    public List<int> l3 = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    public List<int> l4 = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    public List<int> l5 = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

    // initialisation lors du lancement de la scene
    void Start()
    {
        Init();

        Player = GameObject.Find("Player");
        Clef = GameObject.Find("Clef");
        FausseClef = GameObject.Find("FausseClef");
        Tresor = GameObject.Find("Tresor");
        Trou = GameObject.Find("Trou");
        Torche = GameObject.Find("Light");
        Torche = GameObject.Find("Torche");
        Pic1 = GameObject.Find("Pic1");
        Pic2 = GameObject.Find("Pic2");
        Pic3 = GameObject.Find("Pic3");
        Soin = GameObject.Find("Soin");
        Serpent = GameObject.Find("Serpent");
        Tour = GameObject.Find("Tour").GetComponent<Text>();
        Evenements = GameObject.Find("Evenements").GetComponent<Text>();
        Chrono = GameObject.Find("Chrono").GetComponent<Text>();
        Coeur1 = GameObject.Find("Coeur1").GetComponent<Image>();
        Coeur2 = GameObject.Find("Coeur2").GetComponent<Image>();
        Coeur3 = GameObject.Find("Coeur3").GetComponent<Image>();
        ImageClef = GameObject.Find("ImageClef").GetComponent<Image>();

        //Placer le joueur avec les données recup dans scène avant
        float x = PlayerPrefs.GetFloat("x");
        float z = PlayerPrefs.GetFloat("z");
        Player.transform.position = new Vector3(x, 1.25f, z);
        Lumière.transform.position = new Vector3(x, 8f, z);
        Debug.Log("x=" + x + "| z=" + z);
        PlayerPrefs.DeleteKey("x");
        PlayerPrefs.DeleteKey("z");

        //On ecarte les bonus/malus du player
        x =x+5.99f;
        z =z+ 5.99f;
        Debug.Log("x=" + (int)x + "| z=" + (int)z);
        switch ((int)x)
        {
            case 0:
                l0.Remove((int)z + 1);
                l0.Remove((int)z);
                l0.Remove((int)z -1);
                l1.Remove((int)z + 1);
                l1.Remove((int)z);
                l1.Remove((int)z - 1);
                break;
            case 1:
                l0.Remove((int)z + 1);
                l0.Remove((int)z);
                l0.Remove((int)z - 1);
                l1.Remove((int)z + 1);
                l1.Remove((int)z);
                l1.Remove((int)z - 1);
                l2.Remove((int)z + 1);
                l2.Remove((int)z);
                l2.Remove((int)z - 1);
                break;
            case 2:
                l3.Remove((int)z + 1);
                l3.Remove((int)z);
                l3.Remove((int)z - 1);
                l1.Remove((int)z + 1);
                l1.Remove((int)z);
                l1.Remove((int)z - 1);
                l2.Remove((int)z + 1);
                l2.Remove((int)z);
                l2.Remove((int)z - 1);
                break;
            case 3:
                l3.Remove((int)z + 1);
                l3.Remove((int)z);
                l3.Remove((int)z - 1);
                l4.Remove((int)z + 1);
                l4.Remove((int)z);
                l4.Remove((int)z - 1);
                l2.Remove((int)z + 1);
                l2.Remove((int)z);
                l2.Remove((int)z - 1);
                break;
            case 4:
                l3.Remove((int)z + 1);
                l3.Remove((int)z);
                l3.Remove((int)z - 1);
                l4.Remove((int)z + 1);
                l4.Remove((int)z);
                l4.Remove((int)z - 1);
                l5.Remove((int)z + 1);
                l5.Remove((int)z);
                l5.Remove((int)z - 1);
                break;
            case 5:
                l4.Remove((int)z + 1);
                l4.Remove((int)z);
                l4.Remove((int)z - 1);
                l5.Remove((int)z + 1);
                l5.Remove((int)z);
                l5.Remove((int)z - 1);
                break;
        }

        //On appelle les cases speciale aléatoire
        Placealea(Tresor);
        Placealea(Clef);
        Placealea(FausseClef);
        Placealea(Trou);
        Placealea(Torche);
        Placealea(Pic1);
        Placealea(Pic2);
        Placealea(Pic3);
        Placealea(Soin);
        Placealea(Serpent);
    }

    // Appelé toutes les secondes après le début
    void Update()
    {
        //On check le temps d'affichage de l'event 
        if (Evenements.GetComponent<Text>().text != "")
        {
            if(Tpevent+3==(int)time)
            { Evenements.color = new Color(255,222,0,255);Evenements.GetComponent<Text>().text = ""; }
        }

        Tour.GetComponent<Text>().text = Ntour.ToString("0");
        Debug.DrawRay(transform.position, transform.forward);

        if (joueur == "player")
        {
            //Si il ne n'est pas en deplacement rechercher les cases accessible
            if (!deplacement)
            {
                FindSelectableTiles();
                CheckMouse();
            }
            else
            {
                Mouvement();
            }
        }
        else
        { }
        //Chronometre
        time += 1 * Time.deltaTime;
        Chrono.GetComponent<Text>().text = time.ToString("0");

        //Deplace la lumière
        float x = Player.transform.position.x;
        float z = Player.transform.position.z;
        Lumière.transform.position = new Vector3(x, 8f, z);

        //Check poisson
        if (poison == true)
        { if ((int)time == (int)Tpoison + 5) { vie--; Debug.Log("Vie=" + vie); Tpoison = (int)time; } }
        //Check la vie
        if (vie == 3) { Coeur1.color = new Color(255, 255, 255, 255); Coeur2.color = new Color(255, 255, 255, 255); Coeur3.color = new Color(255, 255, 255, 255); }
        if (vie == 2) { Coeur1.color = new Color(0, 0, 0, 0); Coeur2.color = new Color(255, 255, 255, 255); Coeur3.color = new Color(255, 255, 255, 255); }
        else
        {
            if (vie == 1) { Coeur1.color = new Color(0, 0, 0, 0); Coeur2.color = new Color(0, 0, 0, 0); Coeur3.color = new Color(255, 255, 255, 255); }
            else
            {
                if (vie == 0)
                {
                    Handheld.Vibrate();
                    Coeur3.color = new Color(0, 0, 0, 0);
                    Debug.Log("Plus de vie, Défaite!");
                    SceneManager.LoadScene("Defaite");
                }
            }
        }

    }

    void CheckMouse()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Tile")
                {
                    Tile t = hit.collider.GetComponent<Tile>();

                    if (t.selectable)
                    {
                        //Recup données emplacement joueur pour le suivre
                        MoveToTile(t);
                        //Compteur de tour à chaque mouvement  
                        Ntour++;
                        Tour.GetComponent<Text>().text = Ntour.ToString("0");
                        float x = Player.transform.position.x;
                        float z = Player.transform.position.z;
                        Debug.Log("x=" + x + "| z=" + z);
                    }
                }
            }
        }
    }
    //Si le joueur est sur une case specifique
    void OnTriggerEnter(Collider contact)
    {
        Debug.Log(contact.name);
        if(contact.name == "Clef")
        {
            Evenements.GetComponent<Text>().text = "Une Clef! Où est le trésor?";
            Tpevent = (int)time;
            clef = true;
            ImageClef.color = new Color (0,255,0,255);
            Destroy(Clef);
        }
        if (contact.name == "FausseClef")
        {
            Evenements.GetComponent<Text>().text = "Oups! Fausse Clef";
            Tpevent = (int)time;
            Destroy(FausseClef);
        }
        if (contact.name == "Tresor")
        {
            if (clef == true)
            {
                //A la victoire on charge le scene win, on recup le pseudo qu'on detruit et crée un résultat av une clef qui s'incremente
                Evenements.GetComponent<Text>().text = "Victoire";
                Tpevent = (int)time;
                Debug.Log("Victoire!!");
                string Pseudo = PlayerPrefs.GetString("Pseudo");
                int i = 0;
                while (PlayerPrefs.HasKey(i.ToString()))
                {
                    i++;
                }
                Handheld.Vibrate();
                PlayerPrefs.SetString(i.ToString(), Pseudo +"/"+ time);
                SceneManager.LoadScene("Victoire");
                Destroy(Tresor);
            }
        }
        if (contact.name == "Trou")
        {
            Evenements.GetComponent<Text>().text = "Oups! Chute dans le trou";
            Tpevent = (int)time;
            Handheld.Vibrate();
            Trou.GetComponent<Renderer>().enabled = true;
            Debug.Log("Tombé, Défaite!");
            SceneManager.LoadScene("Defaite");     
        }
        if (contact.name == "Torche")
        {
            Evenements.GetComponent<Text>().text = "Cool! Une torche";
            Tpevent = (int)time;
            Lumière.spotAngle = 35;
            Destroy(Torche);
        }
        if (contact.name == "Pic1")
        {
            Evenements.GetComponent<Text>().text = "Aie! des Pics";
            Tpevent = (int)time;
            vie--;
            Debug.Log("Vie=" + vie);
            Pic1.GetComponent<Renderer>().enabled = true;
        }
        if (contact.name == "Pic2")
        {
            Evenements.GetComponent<Text>().text = "Aie! des Pics";
            Tpevent = (int)time;
            vie--;
            Debug.Log("Vie=" + vie);
            Pic2.GetComponent<Renderer>().enabled = true;
        }
        if (contact.name == "Pic3")
        {
            Evenements.GetComponent<Text>().text = "Aie! des Pics";
            Tpevent = (int)time;
            vie--;
            Debug.Log("Vie=" + vie);
            Pic3.GetComponent<Renderer>().enabled = true;
        }
        if (contact.name == "Soin")
        {
            Evenements.GetComponent<Text>().text = "Super! Je me sens mieux";
            Tpevent = (int)time;
            Player.GetComponent<Renderer>().material.color = new Color(255, 150, 0, 255);
            if (vie < 3) { vie++; }
            poison = false;
            Debug.Log("Vie=" + vie);
        }
        if (contact.name == "Serpent")
        {
            Evenements.GetComponent<Text>().text = "Empoisonné! Vite des Soins";
            Evenements.color = Color.green;
            Tpoison = (int)time;
            Player.GetComponent<Renderer>().material.color = Color.green;
            poison = true;
        }
    }

    //Generation random
    void Placealea(GameObject obj)
    {
        //on verifie si l'emplacement av une liste des coordonnées dispo
        int moinsx = Random.Range(0, 5); // Génère un entier compris entre 0 et 5
        int moinsz = Random.Range(0, 9); // Génère un entier compris entre 0 et 10
        float x = moinsx - 5.40f;
        float z = moinsz - 5.55f;

        switch (moinsx)
        {
            case 0:
                while (l0.Contains(moinsz) != true) 
                {
                    moinsz = Random.Range(0, 9); // Génère un entier compris entre 0 et 10
                    z = -5.5f + moinsz;
                }
                l0.Remove(moinsz);
                obj.transform.position = new Vector3(x, 1.5f, z);
                Debug.Log(obj.name + " x=" + moinsx + "| z=" + moinsz);
                break;
            case 1:
                while (l1.Contains(moinsz) != true)
                {
                    moinsz = Random.Range(0, 9); // Génère un entier compris entre 0 et 10
                    z = -5.5f + moinsz;
                }
                l1.Remove(moinsz);
                obj.transform.position = new Vector3(x, 1.5f, z);
                Debug.Log(obj.name + " x=" + moinsx + "| z=" + moinsz);
                break;
            case 2:
                while (l2.Contains(moinsz) != true)
                {
                    moinsz = Random.Range(0, 9); // Génère un entier compris entre 0 et 10
                    z = -5.5f + moinsz;
                }
                l2.Remove(moinsz);
                obj.transform.position = new Vector3(x, 1.5f, z);
                Debug.Log(obj.name + " x=" + moinsx + "| z=" + moinsz);
                break;
            case 3:
                while (l3.Contains(moinsz) != true)
                {
                    moinsz = Random.Range(0, 9); // Génère un entier compris entre 0 et 10
                    z = -5.5f + moinsz;
                }
                l3.Remove(moinsz);
                obj.transform.position = new Vector3(x, 1.5f, z);
                Debug.Log(obj.name + " x=" + moinsx + "| z=" + moinsz);
                break;
            case 4:
                while (l4.Contains(moinsz) != true)
                {
                    moinsz = Random.Range(0, 9); // Génère un entier compris entre 0 et 10
                    z = -5.5f + moinsz;
                }
                l4.Remove(moinsz);
                obj.transform.position = new Vector3(x, 1.5f, z);
                Debug.Log(obj.name + " x=" + moinsx + "| z=" + moinsz);
                break;
            case 5:
                while (l5.Contains(moinsz) != true)
                {
                    moinsz = Random.Range(0, 9); // Génère un entier compris entre 0 et 10
                    z = -5.5f + moinsz;
                }
                l5.Remove(moinsz);
                obj.transform.position = new Vector3(x, 1.5f, z);
                Debug.Log(obj.name + " x=" + moinsx + "| z=" + moinsz);
                break;
        }

    }
}