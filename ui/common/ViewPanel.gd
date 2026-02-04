class_name ViewPanel
extends CanvasLayer


# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	Manager.on_view_change(on_view_change)

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	pass

func on_view_change(new_view: String):
	if new_view == name:
		show()
	else: 
		hide()