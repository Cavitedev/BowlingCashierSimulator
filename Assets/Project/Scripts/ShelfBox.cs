using UnityEngine;

public class ShelfBox : MonoBehaviour
{

    public Material inactiveMaterial;
    public Material gazedAtMaterial;

    
    
    private Renderer _renderer;

    public void Start()
    {
        _renderer = GetComponent<Renderer>();
        SetMaterial(false);
    }

    public void OnPointerEnter()
    {

        Debug.Log("Enter");
        SetMaterial(true);
        CircleLoader.Instance.Pick(gameObject);
    }

    /// <summary>
    /// This method is called by the Main Camera when it stops gazing at this GameObject.
    /// </summary>
    public void OnPointerExit()
    {

        
        Debug.Log("Exit");
        SetMaterial(false);
        CircleLoader.Instance.ExitPick();
    }

    /// <summary>
    /// This method is called by the Main Camera when it is gazing at this GameObject and the screen
    /// is touched.
    /// </summary>
    public void OnPointerClick()
    {

        Debug.Log("Click");


        // transform.SetParent(Player.Instance.transform);
    }



    /// <summary>
    /// Sets this instance's material according to gazedAt status.
    /// </summary>
    ///
    /// <param name="gazedAt">
    /// Value `true` if this object is being gazed at, `false` otherwise.
    /// </param>
    private void SetMaterial(bool gazedAt)
    {
        if (inactiveMaterial != null && gazedAtMaterial != null)
        {
            _renderer.material = gazedAt ? gazedAtMaterial : inactiveMaterial;
        }
    }
}