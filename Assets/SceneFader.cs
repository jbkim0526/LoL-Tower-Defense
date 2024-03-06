using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
    public Image image;
    public AnimationCurve curve;

    private void Start()
    {
        StartCoroutine(FadeIn());

    }

    public void Fadeto (string scene)
    {

        StartCoroutine(FadeOut(scene));

    }

    IEnumerator FadeIn()
    {

        float t = 1f;
        // Fading Effect 

        while(t > 0f )
        {
            t -= Time.deltaTime *1f;

            float a = curve.Evaluate(t);
            image.color = new Color (0f,0f,0f,a);
            yield return 0;



        }

        //Load a scene 



    }



    IEnumerator FadeOut(string scene)
    {

        float t = 0f;
        // Fading Effect 

        while (t < 1f)
        {
            t += Time.deltaTime * 1f;

            float a = curve.Evaluate(t);
            image.color = new Color(0f, 0f, 0f, a);
            yield return 0;



        }

        //Load a scene 

        SceneManager.LoadScene(scene);

    }



}
