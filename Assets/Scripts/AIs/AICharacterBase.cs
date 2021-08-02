using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AICharacterBase : MonoBehaviour, AIBase
{

    protected BehaviourTree behaviourTree;

    protected NavMeshObstacle _obstacle;

    #region Target
    [Header("Target info")]
    protected Vector3 _currentTarget;
    [SerializeField]
    protected bool _hasTarget;

    protected TeamMember _teamMember;
    protected Character _character;


    [Header("AI inputs")]
    public AIInputAxis inputAxis;

    public AIInputButton inputShoot;

    public AIInputButton inputIntercept;

    public AIInputButton inputPass;

    [Header("Gizmos")]
    public bool ShowGizmos = true;
    public bool ShowTarget = false;

    protected NavMeshPath path;

    public void ChangeTarget(Vector3 target)
    {
        _currentTarget = target;

        _hasTarget = true;
    }

    public void DisableTarget()
    {
        _currentTarget = Vector3.zero;

        _hasTarget = false;
    }

    public bool CanGetTarget()
    {
        if (!_hasTarget)
            return false;
        return true;
    }

    public Vector3 GetTarget()
    {
        if (CanGetTarget())
            return _currentTarget;
        else
            return Vector3.zero;
    }
    #endregion


    #region  SETUP
    protected virtual void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
        _obstacle = GetComponent<NavMeshObstacle>();

        path = new NavMeshPath();

        inputAxis = new AIInputAxis(path, _obstacle);

        inputShoot = new AIInputButton();
        inputIntercept = new AIInputButton();
        inputPass = new AIInputButton();

        _character = GetComponent<Character>();

        _teamMember = GetComponent<TeamMember>();

        behaviourTree = GetComponent<BehaviourTree>();

    }

    protected virtual void Start()
    {
        SetBehaviour();
    }


    public virtual void SetBehaviour()
    {
        if (!behaviourTree)
        {
            behaviourTree = gameObject.AddComponent<BehaviourTree>();
        }

        BTSelector root = new BTSelector("ROOT");

        root.SetNode(GetBranch());

        behaviourTree.Build(root);

    }


    public virtual void RestartBehaviour()
    {

        SetBehaviour();
        if (behaviourTree)
        {
            behaviourTree.enabled = true;
            behaviourTree.Initialize();
        }
    }

    public virtual void StopBehaviour()
    {
        if (behaviourTree)
        {
            behaviourTree.Stop();
            behaviourTree.enabled = false;

            behaviourTree.StopAllCoroutines();
        }
    }
    #endregion

    #region GET Branch
    public virtual BTNode GetBranch()
    {

        BTParallelSelector parallel = new BTParallelSelector();


        return parallel;
    }
    #endregion

    protected virtual void OnDrawGizmos()
    {
        if (!ShowGizmos) return;

        if (ShowTarget)
        {
            Gizmos.color = Color.black;
            Gizmos.DrawLine(GetTarget(), GetTarget() + (Vector3.up * 5));
        }
    }


}