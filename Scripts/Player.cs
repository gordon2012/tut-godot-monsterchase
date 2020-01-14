using System;
using Godot;

public class Player : KinematicBody2D {
    private Vector2 movement = Vector2.Zero;
    private float move_Speed = 400f;
    private float gravity = 20f;
    private float jump_Force = -900f;
    private Vector2 up_Dir = Vector2.Up;
    private AnimatedSprite animation;

    public override void _Ready () {
        animation = GetNode<AnimatedSprite> ("Animation");
    }

    public override void _PhysicsProcess (float delta) {
        PlayerMovement ();
    }

    void PlayerMovement () {
        movement.y += gravity;

        if (Input.IsActionPressed ("move_right")) {
            movement.x = move_Speed;
            AnimateMovement (true, true);
        } else if (Input.IsActionPressed ("move_left")) {
            movement.x = -move_Speed;
            AnimateMovement (true, false);
        } else {
            movement.x = 0f;
            AnimateMovement (false, true);
        }

        if (IsOnFloor ()) {
            if (Input.IsActionJustPressed ("jump")) {
                movement.y = jump_Force;
            }
        }

        movement = MoveAndSlide (movement, up_Dir);
    }

    void AnimateMovement (bool moving, bool moveRight) {
        if (moving) {
            animation.Play ("Walk");
            animation.FlipH = !moveRight;
        } else {
            animation.Play ("Idle");
        }
    }

    void _on_Body_entered (PhysicsBody2D body) {
        if (body.IsInGroup ("Enemy")) {
            GetParent ().GetNode<CameraFollow> ("Main Cam").playerDied = true;
            GetParent<Gameplay> ().PlayerDied ();
            QueueFree ();
        }
    }
}
