using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AICharacterBase : MonoBehaviour, AIBase
{

    protected BehaviourTree behaviourTree;

    private NavMeshObstacle _obstacle;

    #region Target
    [Header("Target info")]
    private Vector3 _currentTarget;
    [SerializeField]
    private bool _hasTarget;

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

    [Header("AI inputs")]
    public AIInputAxis inputAxis;

    [Header("Gizmos")]
    public bool ShowGizmos = true;


    #region  SETUP
    protected virtual void Awake()
    {

        Init();
    }

    private NavMeshPath path;

    protected virtual void Init()
    {
        _obstacle = GetComponent<NavMeshObstacle>();

        path = new NavMeshPath();

        inputAxis = new AIInputAxis(path, _obstacle);


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

        BTSelector root = new BTSelector();

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

        BTMove move = new BTMove(0.2f, "Move to block");

        parallel.SetNode(move);

        return parallel;
    }
    #endregion

    protected virtual void OnDrawGizmos()
    {
        if (!ShowGizmos) return;

    }


}