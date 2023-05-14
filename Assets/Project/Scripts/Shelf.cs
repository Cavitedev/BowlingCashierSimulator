using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Shelf : MonoBehaviour
{
    [SerializeField] private int totalShoes = 24;
    [SerializeField] private int actualTotal = 0;

    
    [SerializeField] private ShoeSizeAndAmount[] sizes;

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
        

    }


}

[System.Serializable]
public class ShoeSizeAndAmount
{
    public int size;
    public int amount;
}