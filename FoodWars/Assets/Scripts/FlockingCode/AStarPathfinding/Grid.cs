using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public Transform player;
    public LayerMask unwalkableMask;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    Node[,] grid;


    float nodeDiameter;
    int gridSizeX, gridSizeY;  //number of nodes within the grid
    Vector2 worldBottomLeft;

    private void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        CreateGrid();
    }

    private void Update()
    {
       // CreateGrid();
    }


    public void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        worldBottomLeft = (Vector2) transform.position - Vector2.right * gridWorldSize.x / 2 - Vector2.up * gridWorldSize.y / 2;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector2 worldPoint = worldBottomLeft + Vector2.right * (nodeDiameter * x + nodeRadius) + Vector2.up * (nodeDiameter * y + nodeRadius);
                bool walkable = !(Physics2D.OverlapCircle(worldPoint, nodeRadius,unwalkableMask));

                grid[x, y] = new Node(walkable, worldPoint, x, y);

            }
        }
    }

    public Node NodeFromWorldPoint(Vector2 worldPosition)
    {
        float percentX = (worldPosition.x -worldBottomLeft.x) / gridWorldSize.x;
        float percentY = (worldPosition.y -worldBottomLeft.y) / gridWorldSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
        return grid[x,y];
    }


    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for(int y = -1; y <=1; y++)
            {
                if(x == 0 && y == 0)
                {
                    continue;
                }

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if(checkX >= 0 && checkX < gridSizeX && checkY >=0 && checkY < gridSizeY)
                {
                    neighbours.Add(grid[checkX, checkY]);
                }
            }
        }
        return neighbours;
    }

    
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector2(gridWorldSize.x, gridWorldSize.y));

        if(grid != null)
        {
            Node playerNode = NodeFromWorldPoint(player.position);
            foreach (Node n in grid)
            {
                Gizmos.color = (n.walkable) ? Color.white : Color.red;
                if(playerNode == n)
                {
                    Gizmos.color = Color.cyan;
                }                
                Gizmos.DrawCube(n.worldPosition, Vector2.one * (nodeDiameter - 0.1f));
            }
        }
    }

    
}
