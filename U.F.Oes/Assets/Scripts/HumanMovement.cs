using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HumanMovement : MonoBehaviour
{

   
    [Header("Detection")]
    #region Detection Stuff
    Ray detectionRay;
    [SerializeField] bool ActivateCheck =false;
    [SerializeField] float dangerDistance, mediumDistance, SafeDistance;
    [SerializeField] float DetectionRadius;
    [SerializeField] SphereCollider SC;
    #endregion

    [SerializeField] float FOVResolution;

    #region Human Nav Mesh
    [Header("Paths and Following")]
    [SerializeField] PathObject KitchenPath;
    [SerializeField] PathObject LivingPath;
    [SerializeField] PathObject BathroomPath;
    [SerializeField] PathObject BedroomPath;
    [SerializeField] PathObject GardenPath;

    [SerializeField] PathObject CurrentPath;
    [SerializeField] int CurrentPathIndex;
    [SerializeField] int CurrentPathPoint = 0;
    


    NavMeshAgent humanNavAgent;
    #endregion

    ManagerScript myManager;
    private void Awake()
    {

        SafeDistance = DetectionRadius;
        SC = this.GetComponent<SphereCollider>();
        SC.radius = DetectionRadius;
        detectionRay = new Ray();
        myManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<ManagerScript>();
        humanNavAgent = this.gameObject.GetComponent<NavMeshAgent>();
        CurrentPath = new PathObject();
        CurrentPath = KitchenPath;
        CurrentPathPoint = 0;
    }

    private void FixedUpdate()
    {
        if (myManager.playerTurn == false)
        {
            switch (CurrentPathIndex)
            {
                case 1:
                    CurrentPath = KitchenPath;
                    break;
                case 2:
                    CurrentPath = GardenPath;
                    break;
                case 3:
                    CurrentPath = BathroomPath;
                    break;
                case 4:
                    CurrentPath = BedroomPath;
                    break;
                case 5:
                    CurrentPath = LivingPath;
                    break;
            }

            MoveToWaypoint(CurrentPath);
            AlienDetection();
        }
        
    }
    

    void MoveToWaypoint(PathObject following)
    {
        Transform destPoint = following.pathPoints[CurrentPathPoint];

        humanNavAgent.SetDestination(following.pathPoints[CurrentPathPoint].position);

        if (Vector3.Distance(this.transform.position, destPoint.position) < 1f)
        {
            ActivateCheck=true;
            if (CurrentPathPoint < following.pathPoints.Count - 1)
            {
                CurrentPathPoint++;
            }
            else
            {
                Debug.Log("Switching");
                
                CurrentPathIndex = Random.Range(1, 6);
                CurrentPathPoint = 0;

            }

        }
    }


    void AlienDetection()
    {
        
        if (ActivateCheck)
        {
            Debug.Log("Activates");
            Collider[] objectColliders = Physics.OverlapSphere(this.transform.position, DetectionRadius);

            foreach (Collider Object in objectColliders)
            {

                if (Object.gameObject.tag == "Alien")
                {
                    Vector3 targetDirection = (Object.gameObject.transform.position - this.transform.position).normalized;
                    Ray rayToTarget = new Ray(this.transform.position, targetDirection);
                    RaycastHit rH;
                    if (Physics.Raycast(rayToTarget, out rH, DetectionRadius))
                    {
                        if (rH.collider.gameObject.tag == "Alien")
                        {
                            Debug.DrawLine(this.gameObject.transform.position, rH.collider.gameObject.transform.position, Color.magenta);

                            bool hidden = Hidden(rH.collider.gameObject);
                            if (hidden == false)
                            {

                                Debug.Log(rH.collider.gameObject.name + "Failed the Check");

                            }
                            else
                            {
                                Debug.Log(rH.collider.gameObject.name + "Succeeded");
                            }

                        }
                    }

                }
            }
            Debug.Log("Deactivated");
            ActivateCheck = false ;
            myManager.maxEnergy += myManager.replenRate;
            myManager.playerTurn = true;
        }



        #region My Region
        /*
        Vector3 targetDirection = (other.gameObject.transform.position - this.transform.position).normalized;
        Ray rayToTarget = new Ray(this.transform.position, targetDirection);
        RaycastHit rH;
        if (Physics.Raycast(rayToTarget, out rH, DetectionRadius))
        {
            if (rH.collider.gameObject.tag == "Alien")
            {
                Debug.DrawLine(this.gameObject.transform.position, rH.collider.gameObject.transform.position, Color.magenta);
                if (ActivateCheck)
                {
                    bool hidden = Hidden(rH.collider.gameObject);
                    if (hidden == false)
                    {
                        
                        Debug.Log(rH.collider.gameObject.name + "Failed the Check");

                    }
                    else
                    {
                        Debug.Log(rH.collider.gameObject.name + "Succeeded");
                    }
                }
            }
        }
        */
        #endregion
    }
    
    bool Hidden(GameObject Alien)
    {
        float Distance = Vector3.Distance(this.transform.position, Alien.transform.position);
        int failchance;
        if(Distance <= dangerDistance)
        {
            failchance = 9;
            int newRandom = Random.Range(0, 11);
            Debug.Log("Generated Chance for: " + Alien.name + " is: " + newRandom);
            if (newRandom < failchance)
            {
                ActivateCheck = false;
                Alien.SetActive(false);
                return false;
            }
            else
            {
                ActivateCheck = false;
                return true;
            }
        }
        if(Distance> dangerDistance&&Distance<= mediumDistance)
        {
            failchance = 5;
            int newRandom = Random.Range(0, 11);
            Debug.Log("Generated Chance for: " + Alien.name + " is: " + newRandom);
            if (newRandom < failchance)
            {
                ActivateCheck = false;
                Alien.SetActive(false);
                return false;
            }
            else
            {
                ActivateCheck = false;
                return true;
            }
        }
        if(Distance> mediumDistance && Distance<= SafeDistance)
        {
            failchance = 1;
            int newRandom = Random.Range(0, 11);
            Debug.Log("Generated Chance for: " + Alien.name + " is: " + newRandom);
            if (newRandom < failchance)
            {
                ActivateCheck = false;
                Alien.SetActive(false);
                return false;
            }
            else
            {
                ActivateCheck = false;
                return true;
            }
        }
        else
        {
            return true;
        }
        
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, dangerDistance);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.transform.position, mediumDistance);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, SafeDistance);
    }

    //Code from Sebastian Lague found here: https://www.youtube.com/watch?v=73Dc5JTCmKI

    /*
    void FOVvisualization()
    {
        int count = Mathf.RoundToInt(FOVResolution*360);
        float countAngle = 60 / count;

        List<Vector3> viewPoints= new List<Vector3>();

        for (int i = 0; i < count; i++)
        {
            float angle = transform.eulerAngles.y -30 + countAngle * i;

            RaycastInfo ViewingArea = viewingInfo(angle);
            viewPoints.Add(ViewingArea.point);
        }
        int VertexCount = viewPoints.Count+1;
        Vector3[] vertices = new Vector3[VertexCount];
        int[] triangles = new int[(VertexCount - 2) * 3];
        vertices[0] = Vector3.zero;

        for (int i = 0; i < VertexCount - 1; i++)
        {
            vertices[i + 1] =transform.InverseTransformPoint(viewPoints[i]);

            if (i < VertexCount - 2)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }
        }
        fovMesh.Clear();
        fovMesh.vertices = vertices;
        fovMesh.triangles = triangles;
        fovMesh.RecalculateNormals();
    }


    Vector3 directionCalculator(float angle)
    {
        return new Vector3(Mathf.Sin(angle * Mathf.Rad2Deg), 0, Mathf.Cos(angle * Mathf.Rad2Deg));
    }

    RaycastInfo viewingInfo(float angleInput)
    {
        Vector3 direction = directionCalculator(angleInput);
        RaycastHit rH;
        if (Physics.Raycast(transform.position, direction, out rH, DetectionRadius))
        {
            return new RaycastInfo(true, rH.point, rH.distance, angleInput);
        }
        else
        {
            return new RaycastInfo(false, transform.position+(direction*DetectionRadius), DetectionRadius, angleInput);
        }
    }

    public struct RaycastInfo
    {
        public bool hit;
        public Vector3 point;
        public float distance;
        public float angle;

        public RaycastInfo(bool _hit, Vector3 _point, float _distance, float _angle)
        {
            hit = _hit;
            point = _point;
            distance = _distance;
            angle = _angle;
        }
    }
    */
}
