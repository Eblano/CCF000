using UnityEngine;
using System.Collections;

public class TriggerTutorial : MonoBehaviour
{
    public TutotialScriptTwo TutorialScript;
    private bool used = false;
    public bool Exit = false;
    public int NumStep = 0;

    void OnTriggerExit(Collider other)
    {
        if(!used && Exit && other.GetComponent<Man>() && other.GetComponent<Man>().player)
        {
            used = true;
            TutorialScript.Step(NumStep);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!used && !Exit && other.GetComponent<Man>() && other.GetComponent<Man>().player)
        {
            used = true;
            TutorialScript.Step(NumStep);
        }
    }
}
