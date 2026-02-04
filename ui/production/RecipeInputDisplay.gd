extends PanelContainer

@export var icon: Texture2D
@export var required_quantity: float
@export var quantity: float

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	var texture_rect: TextureRect = get_node("Input 1/TextureRect")
	texture_rect.texture = icon
	
	var label: Label = get_node("Input 1/Label")
	label.text ="%d / %d" % [quantity, required_quantity] 


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	pass
