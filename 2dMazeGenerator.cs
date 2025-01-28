// 2d proceedural maze generator

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GroundState
{
    Wall,
    Empty,
    Start
}

public class Gen2DMaze : MonoBehaviour
{
    public int width = 10; // Width of the maze
    public int height = 10; // Height of the maze
    public Vector2Int startPos = new Vector2Int(0, 0); // Starting position
    private List<MazeNode> nodes;

    public List<MazeNode> GenerateMaze()
    {
        int nodeIndex;
        int nodeCount;

        nodes = new List<MazeNode>();
        List<MazeNode> pathList = new List<MazeNode>();
        List<MazeNode> wallList = new List<MazeNode>();
        List<Vector2Int> neighbours = new List<Vector2Int>();

        // Initialize the maze with walls
        MazeNode[,] mazeGrid = new MazeNode[width, height];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                MazeNode node = new MazeNode(x, y, null);
                node.State = GroundState.Wall;
                mazeGrid[x, y] = node;
                nodes.Add(node);
            }
        }

        // Set the start position
        MazeNode startNode = mazeGrid[startPos.x, startPos.y];
        startNode.State = GroundState.Start;
        pathList.Add(startNode);

        // Add neighbors of the start node to the wall list
        neighbours = GetNeighbours(startPos.x, startPos.y);
        foreach (var neighbour in neighbours)
        {
            MazeNode neighbourNode = mazeGrid[neighbour.x, neighbour.y];
            if (neighbourNode.State == GroundState.Wall)
            {
                wallList.Add(neighbourNode);
            }
        }

        // Generate the maze
        while (wallList.Count > 0)
        {
            int newIndex = Random.Range(0, wallList.Count);
            MazeNode current = wallList[newIndex];

            // Get neighbors and count the number of empty ones
            neighbours = GetNeighbours(current.X, current.Y);
            nodeCount = 0;
            foreach (var neighbour in neighbours)
            {
                MazeNode neighbourNode = mazeGrid[neighbour.x, neighbour.y];
                if (neighbourNode.State == GroundState.Empty || neighbourNode.State == GroundState.Start)
                {
                    nodeCount++;
                }
            }

            // If exactly one neighbor is empty, carve a path
            if (nodeCount == 1)
            {
                current.State = GroundState.Empty;
                pathList.Add(current);

                foreach (var neighbour in neighbours)
                {
                    MazeNode neighbourNode = mazeGrid[neighbour.x, neighbour.y];
                    if (neighbourNode.State == GroundState.Wall && !wallList.Contains(neighbourNode))
                    {
                        wallList.Add(neighbourNode);
                    }
                }
            }

            wallList.RemoveAt(newIndex);
        }

        return nodes;
    }

    // Get neighbors of a specific cell
    private List<Vector2Int> GetNeighbours(int x, int y)
    {
        List<Vector2Int> neighbours = new List<Vector2Int>();

        if (x > 0) neighbours.Add(new Vector2Int(x - 1, y));     // Left
        if (x < width - 1) neighbours.Add(new Vector2Int(x + 1, y)); // Right
        if (y > 0) neighbours.Add(new Vector2Int(x, y - 1));     // Down
        if (y < height - 1) neighbours.Add(new Vector2Int(x, y + 1)); // Up

        return neighbours;
    }

    // Debugging utility to display the maze in the Unity console
    public void DisplayMaze()
    {
        foreach (var node in nodes)
        {
            Debug.Log($"Node: ({node.X}, {node.Y}) -> {node.State}");
        }
    }
}

public class MazeNode
{
    public int X { get; set; }
    public int Y { get; set; }
    public GroundState State { get; set; }
    public object Contents { get; set; } // Placeholder for any additional data (e.g., obstacles)

    public MazeNode(int x, int y, object contents)
    {
        X = x;
        Y = y;
        Contents = contents;
    }
}
