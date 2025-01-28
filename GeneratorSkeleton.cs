// skeleton proceedural generator for mazes, levels, maps, stories, etc. 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProceduralGenerator<T>
{
    protected int width;
    protected int height;

    // Initialization
    public ProceduralGenerator(int width, int height)
    {
        this.width = width;
        this.height = height;
    }

    // Abstract method to be implemented by specific generators
    public abstract List<T> Generate();

    // Utility: Random number generator
    protected int GetRandomInt(int min, int max)
    {
        return Random.Range(min, max);
    }

    // Utility: Get random position within bounds
    protected Vector2Int GetRandomPosition()
    {
        return new Vector2Int(Random.Range(0, width), Random.Range(0, height));
    }
}

public class MazeGenerator : ProceduralGenerator<MazeNode>
{
    public MazeGenerator(int width, int height) : base(width, height) { }

    public override List<MazeNode> Generate()
    {
        List<MazeNode> maze = new List<MazeNode>();
        // Custom maze generation logic here
        Debug.Log("Generating Maze...");
        return maze;
    }
}

public class StoryGenerator : ProceduralGenerator<string>
{
    public StoryGenerator(int width, int height) : base(width, height) { }

    public override List<string> Generate()
    {
        List<string> storyNodes = new List<string>();
        // Custom story generation logic here
        Debug.Log("Generating Story...");
        return storyNodes;
    }
}
