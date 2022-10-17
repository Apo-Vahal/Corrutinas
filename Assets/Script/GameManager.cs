using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
public class GameManager : MonoBehaviour
{
    public Text textScore; //box text for score
    public int score; //point count
    public float fadeSpeed;


    private void Start()
    {
        StreamReader sr = new StreamReader(Application.persistentDataPath + "Score.txt");

        score = int.Parse(sr.ReadLine());
        textScore.text = "Score: " + score;

    }


    public void RefreshScore()
    {
        score += 100;
        textScore.text = "Score: " + score;
    }

   


    public void StartFadeOut()
    {
        StartCoroutine(FadeOut(fadeSpeed));
    }

    IEnumerator FadeOut(float fadeSpeed)
    {

        Color matColor = textScore.color;
        float alphaValue = textScore.color.a;

        while (textScore.color.a > 0f)
        {
            alphaValue -= Time.deltaTime / fadeSpeed;
            textScore.color = new Color(matColor.r, matColor.g, matColor.b, alphaValue);
            yield return null;
        }
        textScore.color = new Color(matColor.r, matColor.g, matColor.b, 0f);
        StartCoroutine(FadeIn(fadeSpeed));
    }

    IEnumerator FadeIn(float fadeSpeed)
    {
        RefreshScore();
        Color matColor = textScore.color;
        float alphaValue = textScore.color.a;

        while (textScore.color.a < 1f)
        {
            alphaValue += Time.deltaTime / fadeSpeed;
            textScore.color = new Color(matColor.r, matColor.g, matColor.b, alphaValue);
            yield return null;
        }
        textScore.color = new Color(matColor.r, matColor.g, matColor.b, 1f);
    }

    private void OnDisable()
    {
        FileStream fs = File.Create(Application.persistentDataPath + "Score.txt");

        StreamWriter sw = new(fs);

        sw.WriteLine(score);


        sw.Close();
        fs.Close();
    }





}
