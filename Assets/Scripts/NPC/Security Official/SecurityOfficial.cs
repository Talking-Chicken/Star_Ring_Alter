using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class SecurityOfficial : MonoBehaviour
{
    [Header("Drink")]
    public int drunkLevel; //after the checkdrunk, drunk will translate to dunk level
    public int drunk;
    public int[] drunkCheckpoints;
    public int liquor = 15;
    [SerializeField] private int liquorMax = 15;
    [SerializeField] private GameObject drinkingIcon;

    private CharacterTraits character;
    private InteractiveObj interactiveObj;
    void Start()
    {
        character = GetComponent<CharacterTraits>();
        interactiveObj = GetComponentInChildren<InteractiveObj>();

        //adding "randomNum" function to Yarn Spinner, so thart security official can say random line when player adding drinks to him
    }

    
    void Update()
    {
        checkDrunkLevel();
    }

    [YarnCommand("drink")]
    public void drink() //when drinking, it will also check whether liquor has left
    {
        drinkingIcon.SetActive(true);
        if (liquor >= 5)
        {
            drunk += 10;
            liquor -= 5;
        } else
        {
            liquor = 0;
        }
    }

    [YarnCommand("randomize")]
    public float randomizeDialogueNum(float f1, float f2)
    {
        float randomNum = Random.Range(f1, f2);
        return randomNum;
    }

    [YarnCommand("addDrink")]
    public void addDrink()
    {
        liquor = liquorMax;
    }

    [YarnCommand("changeDialogueStartingNode")]
    public void changeDialogueStartingNode(string node)
    {
        if (node != "")
        {
            character.startNode = node;
            Debug.Log("changing node");
        }
    }

    public void checkDrunkLevel()
    {
        for (int i = drunkCheckpoints.Length - 1; i >= 0; i--)
        {
            if (drunk >= drunkCheckpoints[i])
            {
                drunkLevel =  i;
                break;
            }
        }
    }
}
