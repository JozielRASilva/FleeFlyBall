using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BehaviourTree : MonoBehaviour
{

    public TextMeshProUGUI textPro;

    private string nodes;

    private AICharacterBase _aiCharacter;
    public AICharacterBase aICharacter { get => _aiCharacter; }

    private void Nodes()
    {
        GUILayout.Label($"{nodes}");
    }

    private BTNode root;

    public Coroutine execution;

    void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        _aiCharacter = GetComponent<AICharacterBase>();

        execution = StartCoroutine(Execute());
    }

    public void Stop()
    {
        if (execution != null)
            StopCoroutine(execution);
    }

    public void Build(BTNode _root)
    {
        root = _root;
    }

    IEnumerator Execute()
    {
        while (true)
        {
            if (root != null) yield return StartCoroutine(root.Run(this));
            else yield return null;
        }
    }

    private void LateUpdate()
    {
        nodes = GetNodes();

        if (textPro)
            textPro.text = nodes;
    }

    private string GetNodes()
    {
        return GetWriteNode(root);
    }

    private string GetWriteNode(BTNode node)
    {
        string value = $"{node.name} : {node.status.ToString()}";

        foreach (var _node in node.children)
        {
            value += $"\n {GetWriteNode(_node)}";
        }

        return value;
    }

}
