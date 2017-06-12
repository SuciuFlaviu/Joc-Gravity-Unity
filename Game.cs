using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// importeaza librariile necesare

public class Game : MonoBehaviour { // clasa Game mosteneste de la clasa MonoBehaviour pentru a putea fi aplicata ca o componenta in Unity


	public Camera mainCamera; // referinta la camera din joc 
	public GameObject startButton; // referinta la butonul de start
	private bool gameIsRunning=false; // variabila booleana care contine informatie referitoare la stdiul in care este jocul
	public GameObject[] things; // vector de obiecte care pot aparea in joc
	public int numberOfThings=0; // numarul current de obiecte din joc
	private GameObject thing; // 
	public GameObject scoreText; // referinta la textul care afiseaza scorul
	public int score=0; // scor


	private Vector3 gravity; // variabila care determina directia in care va fi aplicata forta gravitationala a jocului



	void Start()// functie din clasa MonoBehaviour care este apelata la instantierea obiectului care are aceasta clasa ca componenta
	{
		Button btn = startButton.GetComponent<Button>(); // se gaseste componenta de clasa "Button" din obiectul startButton
		btn.onClick.AddListener(StartGame); // seteaza ca in evenimenul in care se da click pe buton functia StartGame() este apelata
		gravity = new Vector3 (9,9,9); // gravitatia este setata astfel incat sa fie anulata pe toate axele

	}

	void StartGame()
	{
		numberOfThings=0;
		score=0;
		Debug.Log("Buton Apasat");
		startButton.SetActive(false); // dezactiveaza butonul de start
		gameIsRunning=true;
		StartCoroutine (spawnThings ()); // porneste o functie apelata de mai multe ori numita spawnThings()
	}

	IEnumerator spawnThings ()   // functie de tip Ienumerator, o functie in care se poate astepta 


	{	
		yield return new WaitForSeconds (1); // se asteapta 1 secunda
		while (gameIsRunning) 
		{	
			if(numberOfThings<50) // daca sunt sub 50 de obiecte pe ecran
			{
				int i;
				for (i = 1; i <= Random.Range(6,10); i++)  // vor aparea intre 6 si 10 obiecte 
				{
					thing = things [Random.Range(0,things.Length)];
					// este instantiat un obiect 
					Vector3 spawnPosition = new Vector3 (Random.Range (-2.5f, 2.5f), Random.Range (-3.5f, 3.5f), 0);
					// obiectele vor aparea intr-o locaite aleatoare pe ecran intre coordonatele specificate
					Quaternion spawnRotation = new Quaternion ();
					// rotatia este mereu cea implicita
					Instantiate (thing, spawnPosition, spawnRotation);
					// crearea propriu zisa a obiectelor
					numberOfThings++;
					yield return new WaitForSeconds (Random.Range(0.1f,0.5f));
					//se asteapta intre crearea a 2 obiecte consecutive

				}
			}
			yield return new WaitForSeconds (2); //asteapta 2 secunde
		}
	}

	void Update ()  // funcie care este apelata la fiecare frame de randare a jocului
	{
		if(gameIsRunning)
		{

			Physics2D.gravity = 9.8f * Input.acceleration.normalized; // gravitatia este schimbata in functie de pozitia accelerometrului de la telefon

		}



	}


}
