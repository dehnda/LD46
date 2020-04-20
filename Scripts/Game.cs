using Godot;
using System.Collections.Generic;

public class Game : Node2D
{
    public List<string> Levels = new List<string>();
    private PackedScene menuScene = null;
    private Transition transition = null;
    private Player player = null;
    private bool playerProcessing = true;

    private int currentId = -1;
    private Node currentLevel = null;
    public override void _Ready()
    {
        player = GetNode<Player>("Player");

        menuScene = ResourceLoader.Load<PackedScene>("res://Scenes/Menu.tscn");

        transition = GetNode<Transition>("Transition");

        VisualServer.SetDefaultClearColor(new Color(0f, 0f, 0f, 1));

        // add scenes
        Levels.Add("res://Scenes/Level1.tscn");
        Levels.Add("res://Scenes/Level2.tscn");

        LoadNextLevel();
    }

    public void LoadNextLevel()
    {
        currentId++;

        if (currentId < Levels.Count)
        {
            var level = ResourceLoader.Load<PackedScene>(Levels[currentId]);

            if (currentLevel != null)
            {
                RemoveChild(currentLevel);
                (currentLevel as Node2D).QueueFree();
                currentLevel = null;
            }
            currentLevel = level.Instance();
            AddChild(currentLevel);
        }

    }

    public void GameOver()
    {

        GetTree().ChangeSceneTo(menuScene);
    }
    public void TogglePauseMode()
    {
        player.ToggleProcessing();
    }

}
