using UnityEngine;
using UnityEngine.Serialization;

public class ShelfBox : MonoBehaviour
{

    public Material inactiveMaterial;
    public Material gazedAtMaterial;

    
    
    private Renderer _renderer;


    private int _shoeCount = 0;
    [FormerlySerializedAs("leftTransforms")] [SerializeField] private Transform[] restTransforms;
    private Pickable[] _restPickables;
    
    
    
    public void Start()
    {
        _renderer = GetComponent<Renderer>();
        _restPickables = new Pickable[restTransforms.Length];
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


        
        if (Player.Instance.IsPicking())
        {
            Pickable pickedObject = Player.Instance.PickedObject;
            if (_shoeCount < restTransforms.Length)
            {
                Transform restTransform = restTransforms[_shoeCount];
                _restPickables[_shoeCount] = pickedObject;
                Player.Instance.Detach(restTransform);
                pickedObject.pickTransform.SetPositionAndRotation(restTransform.position, restTransform.rotation);
                pickedObject.OnPointerExit();
                pickedObject.enabled = false;
                _shoeCount++;
            }
        }
        else
        {
            if (_shoeCount > 0)
            {
                _shoeCount--;
                Pickable pickedObject = _restPickables[_shoeCount];
                Player.Instance.Attach(pickedObject);
                pickedObject.enabled = true;

            }
        }
        

        CircleLoader.Instance.ExitPick();
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