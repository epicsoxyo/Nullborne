using UnityEngine;

using Nullborne.AI;
using Cinemachine;
using Nullborne.Dialogue;



public class Statue : MonoBehaviour
{

    private ParticleSystem particleSystem_;
    private MeshRenderer meshRenderer_;

    [SerializeField] private DialogueAsset statueDialogue_;
    [SerializeField] private DialogueAsset statueDialogue2_;
    private bool triggeredDialogue2_ = false;



    private void Awake()
    {
        particleSystem_ = GetComponent<ParticleSystem>();
        meshRenderer_ = GetComponent<MeshRenderer>();
    }



    public void Explode()
    {
        particleSystem_.Play();
        meshRenderer_.enabled = false;
        FindFirstObjectByType<TurretAI>().Distract(transform);
    }



    public void Sound()
    {

        DialogueManager.instance.OpenDialogue(statueDialogue_);
        DialogueManager.instance.dialogueEnd.AddListener(() =>
        {
            if(!triggeredDialogue2_)
            {
                triggeredDialogue2_ = true;
                DialogueManager.instance.OpenDialogue(statueDialogue2_);
            }
        });

    }

}
