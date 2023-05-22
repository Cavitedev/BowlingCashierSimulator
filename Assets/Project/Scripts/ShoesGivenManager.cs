using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class ShoesGivenManager : MonoBehaviour
{

    public static ShoesGivenManager Instance;

    private List<PickableAndSize> _shoesList = new List<PickableAndSize>();
    public ShoeSizeAndAmount[] sizesLeft;
    public ShoeSizeAndAmount[] sizesGiven;

     [SerializeField] private float chanceReturnPerPair = 0.1f;
    
    
    private void Awake()
    {
        Instance = this;
    }

    public void AddShoeIntoPlayzone(Pickable shoe)
    {
        Debug.Log("Add Shoe");
        Transform parentObj = shoe.pickTransform;

        parentObj.SetParent(transform);
        parentObj.gameObject.SetActive(false);
        int shoeSize = shoe.shoe.GetShoeSize();
        _shoesList.Add(new PickableAndSize(shoe, shoe.shoe.GetShoeSize()));
        

        sizesGiven.First(s => s.size == shoeSize).amount += 1;

    }
    
    public Pickable RemoveShoeFromPlayzone(int shoeSize)
    {
        Debug.Log("Remove Shoe");
        PickableAndSize pickableAndSize = _shoesList.First(s => s.size == shoeSize);
        _shoesList.Remove(pickableAndSize);
        pickableAndSize.pickable.pickTransform.gameObject.SetActive(true);
        sizesLeft.First(s => s.size == shoeSize).amount += 1;
        sizesGiven.First(s => s.size == shoeSize).amount -= 1;
        return pickableAndSize.pickable;
    }

    public bool IsSomeoneReturningShoes()
    {
        float rng = Random.Range(0f, 1f);
        Debug.Log($"rng {rng} < {chanceReturnPerPair * _shoesList.Count} = {rng < chanceReturnPerPair * _shoesList.Count}");
        return rng < chanceReturnPerPair * _shoesList.Count;
    }

    public int RandomSizeToRequest()
    {
        int[] candidates = sizesLeft.Where(s => s.amount >= 2).Select(s => s.size).ToArray();
        int sizeChosen = candidates.Random();
        sizesLeft.First(s => s.size == sizeChosen).amount -= 2;
        return sizeChosen;
    }
    
    public int RandomSizeToReturn()
    {
        int[] candidates = sizesGiven.Where(s => s.amount >= 2).Select(s => s.size).ToArray();
        return candidates.Random();
    }

    public Pickable[] PickablesToReturn()
    {
        int sizeToReturn = RandomSizeToReturn();
        Pickable p1 = RemoveShoeFromPlayzone(sizeToReturn);
        Pickable p2 = RemoveShoeFromPlayzone(sizeToReturn);
        
        return new[] {p1, p2};
    }
    
}

public class PickableAndSize
{
    public Pickable pickable;
    public int size;

    public PickableAndSize(Pickable pickable, int size)
    {
        this.pickable = pickable;
        this.size = size;
    }
}

