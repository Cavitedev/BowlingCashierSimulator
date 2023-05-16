using System.Collections;
using System.Collections.Generic;
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
    
    
    private int _sizeRequest;
    public int SizeRequest
    {
        get { return _sizeRequest; }
        set
        {
            if (_sizeRequest == 0)
            {
                textShow.text = "";
            }
            _sizeRequest = value;
            foreach (ShelfBox shelfbox in shelfBoxes)
            {
                CheckShelfbox(shelfbox);
            }
            
            customer.SetActive(IsRequestingShoe());
            textShow.text = _sizeRequest.ToString();
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
            RequestShoe();
            Debug.Log($"Requesting {SizeRequest}");
        }
    }

    private void CheckToAppear()
    {
        if (IsRequestingShoe() || Player.Instance.IsLookingForward()) return;

        ReappearRandomly();
    }

    private void ReappearRandomly()
    {
        float rngNumber = Random.Range(0, 100f);
        Debug.Log($"RNG  num={rngNumber}, chance={chanceToAppear}");
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
        SizeRequest = Random.Range(37, 41);
    }
}