# temp dump

Every game object has a transform. A transform has the position, rotation, and scale of a game object. This is always relative to the parent. If the game object is the parent, it is relative to the world. If you are doing maths on something then you shouldn't do it with transforms, instead use their properties.