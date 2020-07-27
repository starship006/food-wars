using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pathfinder : MonoBehaviour
{
	public static Pathfinder instance;
	public Transform seeker, target;
	Grid grid;
	public List<Node> path;

	void Awake()
	{
		grid = GetComponent<Grid>();
		if(instance == null)
        {
			instance = this;
		}		
	}

	void Update()
	{
		//FindPath(seeker.position, target.position);
	}

	public List<Node> FindPath(Vector3 startPos, Vector3 targetPos)
	{
		Node startNode = grid.NodeFromWorldPoint(startPos);
		Node targetNode = grid.NodeFromWorldPoint(targetPos);

		Heap<Node> openSet = new Heap<Node>(grid.gridMaxSize);
		HashSet<Node> closedSet = new HashSet<Node>();
		openSet.Add(startNode);

		while (openSet.Count > 0)
		{
			Node node = openSet.RemoveFirst();			
						
			closedSet.Add(node);

			if (node == targetNode)
			{
				//Debug.Log("Node equals target Node");
				return RetracePath(startNode, targetNode);
				
			}

			foreach (Node neighbour in grid.GetNeighbours(node))
			{
				if (!neighbour.walkable || closedSet.Contains(neighbour))
				{
					continue;
				}

				int newCostToNeighbour = node.gCost + GetDistance(node, neighbour);
				if (newCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
				{
					neighbour.gCost = newCostToNeighbour;
					neighbour.hCost = GetDistance(neighbour, targetNode);
					neighbour.parent = node;

					if (!openSet.Contains(neighbour))
						openSet.Add(neighbour);
				}
			}
		}
		Debug.LogError("Null node path returned");
		return null;
	}

	 List<Node> RetracePath(Node startNode, Node endNode)
	{
		List<Node> pathA = new List<Node>();
		Node currentNode = endNode;

		while (currentNode != startNode)
		{
			pathA.Add(currentNode);
			currentNode = currentNode.parent;
		}
		pathA.Reverse();

		return pathA;
	}

	int GetDistance(Node nodeA, Node nodeB)
	{
		int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
		int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

		if (dstX > dstY)
			return 14 * dstY + 10 * (dstX - dstY);
		return 14 * dstX + 10 * (dstY - dstX);
	}
}