extends Button

var item: Manager.Item

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	var texture_rect: TextureRect = get_node("MarginContainer/HBoxContainer/Icon")
	var label: Label = get_node("MarginContainer/HBoxContainer/Label")
	var amount: Label = get_node("MarginContainer/HBoxContainer/Amount")
	var price: Label = get_node("MarginContainer/HBoxContainer/Price")
	
	label.text = item.stats.name
	amount.text = Utils.large_float_to_string(item.quantity)

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	pass
