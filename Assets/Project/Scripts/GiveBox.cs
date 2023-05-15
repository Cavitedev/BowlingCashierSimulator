using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveBox : MonoBehaviour
{
    [SerializeField] private Material invisibleMaterial;
    [SerializeField] private Material invalidMaterial;
    [SerializeField] private Material validMaterial;

    [SerializeField] private GameObject customer;
    
    
    [SerializeField] private ShelfBox[] shelfBoxes;

    private int _sizeRequest;
    public int SizeRequest
    {
        get { return _sizeRequest; }
        set
        {
            _sizeRequest = value;
            foreach (ShelfBox shelfbox in shelfBoxes)
            {
                CheckShelfbox(shelfbox);
            }
            
            customer.SetActive(IsRequestingShoe());
        }
    }




    // Start is called before the first frame update
    void Start()
    {
        foreach (ShelfBox shelfbox in shelfBoxes)
        {
            shelfbox.OnPickableChange += PickableChange;
        }
    }
    
    public void PickableChange(ShelfBox shelfBox)
    {
        CheckShelfbox(shelfBox);
    }

    public void CheckShelfbox(ShelfBox shelfbox)
    {

        if (NoShoeRequest())
        {
            shelfbox.SetInactiveMaterial(invisibleMaterial);
        }else if (IsInvalidShoe(shelfbox))
        {
            shelfbox.SetInactiveMaterial(invalidMaterial);
        }
        else
        {
            shelfbox.SetInactiveMaterial(validMaterial);
        }
    }

    private bool IsRequestingShoe() => !NoShoeRequest();

    private bool NoShoeRequest()
    {
        return _sizeRequest == 0;
    }

    private bool IsInvalidShoe(ShelfBox shelfbox)
    {
        return shelfbox.RestPickable == null || _sizeRequest != shelfbox.RestPickable.shoe.GetShoeSize();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            SizeRequest = Random.Range(37, 41);
            Debug.Log($"Requesting {SizeRequest}");
        }
    }
}