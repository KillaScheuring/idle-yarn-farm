using Godot;
using System;

public partial class Camera2d : Camera2D
{
	[Export] public float ZoomSpeed = 0.05f;
	[Export] public Vector2 MinZoom = new Vector2(0.15f, 0.15f);
	[Export] public Vector2 MaxZoom = new Vector2(1.0f, 1.0f);

	public override void _UnhandledInput(InputEvent @event)
	{
		// Zooming
		if (@event is InputEventMouseButton mouseButton)
		{
			if (mouseButton.ButtonIndex == MouseButton.WheelUp)
			{
				ZoomCamera(ZoomSpeed);
			}
			else if (mouseButton.ButtonIndex == MouseButton.WheelDown)
			{
				ZoomCamera(-ZoomSpeed);
			}
		}

		// Panning
		if (@event is InputEventMouseMotion mouseMotion && Input.IsMouseButtonPressed(MouseButton.Right))
		{
			// As Zoom value gets smaller (zoomed out), the movement vector gets larger
			Position -= mouseMotion.Relative / Zoom;
		}
	}

	private void ZoomCamera(float delta)
	{
		Vector2 newZoom = Zoom + new Vector2(delta, delta);
		Zoom = newZoom.Clamp(MinZoom, MaxZoom);
	}
}

// Called when the node enters the scene tree for the first time.
