using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class GiveBox : MonoBehaviour
{
    [SerializeField] private Material invisibleMaterial;
    [SerializeField] private Material invalidMaterial;
    [SerializeField] private Material validMaterial;

    [SerializeField] private GameObject customer;
    
    
    [SerializeField] private ShelfBox[] shelfBoxes;

    [SerializeField] private float resetChanceToAppear = 10f;
    [SerializeField] private float chanceToAppear = 10f;
    [SerializeField] private float increasingChance = 1f;
    [SerializeField] private float timeBetweenChecks = 1f;

    [SerializeField] private TextMeshPro textShow;

    private bool _isReturningShoe;
    public bool IsReturningShoe
    {
        get { return _isReturningShoe; }
        set
        {
            _isReturningShoe = value;
            ActivateCustome(IsCustomerThere());
        }
    }
    
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
            
            ActivateCustome(IsCustomerThere());

            textShow.text = _sizeRequest.ToString();
            
            if (_sizeRequest == 0)
            {
                textShow.text = "";
            }
        }
    }




    // Start is called before the first frame update
    void Start()
    {
        foreach (ShelfBox shelfbox in shelfBoxes)
        {
            shelfbox.OnPickableChange += PickableChange;
        }

        Invoke(nameof(ReappearRandomly), 0.5f);
        
        InvokeRepeating(nameof(CheckToAppear), 1f , timeBetweenChecks);
    }
    
    public void PickableChange(ShelfBox shelfBox)
    {
        CheckShelfbox(shelfBox);
    }

    public void CheckShelfbox(ShelfBox shelfbox)
    {
        shelfbox.isCorrectlySet = false;

        if (_isReturningShoe)
        {
            if (shelfbox.HasPickable())
            {
                shelfbox.SetInactiveMaterial(invalidMaterial);
            }
            else
            {
                shelfbox.SetInactiveMaterial(validMaterial);
                shelfbox.isCorrectlySet = true;
            }
            
            if (AreShoesRight())
            {
                LeaveAfterPlay();
            }
        }
        else
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
                shelfbox.isCorrectlySet = true;
            }

            if (AreShoesRight())
            {
                LeaveWithShoes();
            }
        }
        

    }

    private bool IsRequestingShoe() => !NoShoeRequest();

    private bool IsCustomerThere() => IsRequestingShoe() || _isReturningShoe;

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
            RequestShoe();
            Debug.Log($"Requesting {SizeRequest}");
        }
    }

    private void CheckToAppear()
    {
        if (IsCustomerThere() || Player.Instance.IsLookingForward()) return;

        ReappearRandomly();
    }

    private void ReappearRandomly()
    {
        float rngNumber = Random.Range(0, 100f);
        // Debug.Log($"RNG  num={rngNumber}, chance={chanceToAppear}");
        if (rngNumber < chanceToAppear)
        {
            RequestShoe();
        }
        else
        {
            chanceToAppear *= (1 + increasingChance);
        }
    }

    private void RequestShoe()
    {
        chanceToAppear = resetChanceToAppear;
        if (ShoesGivenManager.Instance.IsSomeoneReturningShoes())
        {
            Pickable[] pickables = ShoesGivenManager.Instance.PickablesToReturn();
            IsReturningShoe = true;
            for (int i = 0; i < shelfBoxes.Length; i++)
            {
                shelfBoxes[i].LeavePickObjectOnShelfBox(pickables[i]);
                CheckShelfbox(shelfBoxes[i]);
            }



        }
        else
        {
            SizeRequest = ShoesGivenManager.Instance.RandomSizeToRequest();
        }
        
    }

    private void LeaveWithShoes()
    {
        SizeRequest = 0;

        for (int i = shelfBoxes.Length - 1; i >= 0; i--)
        {
            ShelfBox shelfBox = shelfBoxes[i];
            shelfBox.SetInactiveMaterial(invisibleMaterial);
            shelfBox.RemovePickObjectOnShelfBox();
        }

    }
    
    private void LeaveAfterPlay()
    {
        _isReturningShoe = false;
        
        for (int i = shelfBoxes.Length - 1; i >= 0; i--)
        {
            ShelfBox shelfBox = shelfBoxes[i];
            shelfBox.SetInactiveMaterial(invisibleMaterial);
        }
        ActivateCustome(false);
    }


    public bool AreShoesRight()
    {
        return shelfBoxes.All(s => s.isCorrectlySet);
    }

    private void ActivateCustome(bool isActive)
    {
        customer.SetActive(isActive);
        foreach (ShelfBox shelfBox in shelfBoxes)
        {
            shelfBox.enabled = isActive;
        }
    }
}