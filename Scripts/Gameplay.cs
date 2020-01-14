using System;
using Godot;

public class Gameplay : Node {
    [Export]
    private PackedScene greenZombie, redZombie, ghost;

    private Vector2 spawn_Left = new Vector2 (-6300f, 475f);
    private Vector2 spawn_Right = new Vector2 (6300f, 475f);

    private Timer restart;

    public override void _Ready () {
        restart = GetNode<Timer> ("Restart");
    }

    public override void _Process (float delta) {

    }

    void _on_Monster_Spawn () {
        int randEnemy = new Random ().Next (0, 3);

        Monster newMonster = null;

        switch (randEnemy) {
            case 0:
                newMonster = greenZombie.Instance () as Monster;
                break;
            case 1:
                newMonster = redZombie.Instance () as Monster;
                break;
            case 2:
                newMonster = ghost.Instance () as Monster;
                break;
        }

        Vector2 temp;

        if (new Random ().Next (0, 2) > 0) {
            temp = spawn_Right;
            newMonster.moveLeft = true;
        } else {
            temp = spawn_Left;
        }

        newMonster.SetPosition (temp);
        AddChild (newMonster);
    }

    public void PlayerDied () {
        restart.Start ();
    }

    void _on_Player_Died () {
        GetTree ().ReloadCurrentScene ();
    }
}
