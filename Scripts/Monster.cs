using System;
using Godot;

public class Monster : KinematicBody2D {
    private float moveSpeed = 600f;
    private float gravity = 20f;
    private Vector2 movement;

    public bool moveLeft;
    public bool isGhost;

    private float min_X = -6500f, max_X = 6500f;

    public override void _Ready () {
        if (this.Name == "Ghost") {
            isGhost = true;
        }

        if (moveLeft) {
            moveSpeed *= -1;
            GetNode<AnimatedSprite> ("Animation").FlipH = true;
        }
    }

    public override void _PhysicsProcess (float delta) {
        if (!isGhost) {
            movement.y += gravity;
        }

        movement.x = moveSpeed;
        movement = MoveAndSlide (movement);

        if (Position.x > max_X || Position.x < min_X) {
            QueueFree ();
        }
    }
}
