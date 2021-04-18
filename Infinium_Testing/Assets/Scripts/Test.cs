using UnityEngine;

public class Test : MonoBehaviour
{
    private SphereCollider sc;
    public float raduis = 40f;
    [SerializeField] private GameObject dock;
    public bool inRange;
    private float distanceToTarget;

    // Start is called before the first frame update
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, raduis);
    }

    private void Start()
    {
        sc = gameObject.AddComponent<SphereCollider>();
        sc.isTrigger = true;
        sc.radius = raduis;
    }

    // Update is called once per frame
    private void Update()
    {
        //var overlap =  Physics.OverlapSphere(transform.position, raduis, 8);
        // if (overlap.Length <= raduis) inRange = true;
        // else inRange = false;

        //distanceToTarget = Vector3.Distance(dock.transform.position, transform.position);
        //if (distanceToTarget <= raduis) inRange = true;
        //else inRange = false;
    }

    public bool GetInRange()
    {
        return inRange;
    }

    public GameObject GetDock()
    {
        if (dock != null)
            return dock;
        else return null;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<DockingClass>())
        {
            inRange = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<DockingClass>())
        {
            dock = other.gameObject;
            //Debug.Log(dock.name);
        }
         
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<DockingClass>())
        {
            inRange = false;
            dock = null;
        }
        
    }
}