using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Shelf : MonoBehaviour
{
    [SerializeField] private int totalShoes = 24;
    [SerializeField] private int actualTotal = 0;

    
    public ShoeSizeAndAmount[] sizes;

    public static Shelf Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void OnValidate()
    {
        totalShoes = GetComponentsInChildren<Shoe>().Length;

        actualTotal = sizes.Aggregate(0, (cur, next) => cur + next.amount);
        if (actualTotal > totalShoes)
        {
            Debug.LogError("Amount of shoes exceeds the current amount set for the shelf");
        }
    }

    private void Start()
    {
        StartMenu.Instance.onGameStart += RandomizeShelf;
    }

    private void RandomizeShelf()
    {
        Shoe[] shoes = GetComponentsInChildren<Shoe>();

        shoes.Shuffle();

        int shoeSizeIndex = 0;
        int shoeCountInSize = 0;
        int i;
        for (i = 0; i < actualTotal; i++)
        {

            
            ShoeSizeAndAmount shoeSize = sizes[shoeSizeIndex];
            if (shoeCountInSize < shoeSize.amount)
            {

                shoeCountInSize++;
            }
            else
            {
                shoeSize = sizes[shoeSizeIndex + 1];
                shoeCountInSize = 1;
                shoeSizeIndex++;
            }
            
            shoes[i].SetShoeSize(shoeSize.size);
        }

        for (;  i < totalShoes; i++)
        {
            shoes[i].Destroy();
        }

        ShoesGivenManager.Instance.sizesLeft = sizes;
        ShoesGivenManager.Instance.sizesGiven = sizes.Select(s =>  new ShoeSizeAndAmount(s.size, 0)).ToArray();



    }


}

[System.Serializable]
public class ShoeSizeAndAmount
{
    public int size;
    public int amount;

    public ShoeSizeAndAmount(int size, int amount)
    {
        this.size = size;
        this.amount = amount;
    }
}