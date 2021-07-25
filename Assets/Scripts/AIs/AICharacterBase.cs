using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICharacterBase : MonoBehaviour, AIBase
{

    protected BehaviourTree behaviourTree;

    [Header("Gizmos")]
    public bool ShowGizmos = true;


    #region  SETUP
    protected virtual void Awake()
    {

        Init();
    }

    protected virtual void Init()
    {

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


        return parallel;
    }
    #endregion

    protected virtual void OnDrawGizmos()
    {
        if (!ShowGizmos) return;

    }


}