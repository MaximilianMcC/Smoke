using System.Numerics;

namespace Smoke;
public abstract class Transform : Component;

public class Transform3D : Transform
{
	//? All these are local (according to the parent)
	public Vector3 Position;
	public Quaternion Rotation;
	public Vector3 Scale = Vector3.One;

	//? Position in the world
	public Vector3 ActualPosition
	{
		get
		{
			// Check for if we have a parent
			if (GameObject.Parent == null) return Position;

			// Combine ourself with the parent to get the actual position
			Transform3D parentsTransform = GameObject.Parent.GetComponent<Transform3D>();
			return Vector3.Transform(Position, parentsTransform.Matrix);
		}
	}

	public Matrix4x4 Matrix
	{
		get
		{
			// Create a matrix from position, rotation, and scale
			//? must be done in this order I think
			Matrix4x4 localMatrix = 
				Matrix4x4.CreateScale(Scale) *
				Matrix4x4.CreateFromQuaternion(Rotation) *
				Matrix4x4.CreateTranslation(Position);
			
			// Check for if we have a parent
			if (GameObject.Parent == null) return localMatrix;

			// Combine ourself with the parent to get the actual matrix
			Transform3D parentsTransform = GameObject.Parent.GetComponent<Transform3D>();
			return localMatrix * parentsTransform.Matrix; //! MIGHT NEED TO SWAP LEFT/RIGHT
		}
	}

	// eve arsenal lore
}

public class Transform2D : Transform
{
	//? All these are local (according to the parent)
	public Vector2 Position;
	public float Rotation;
	public Vector2 Scale = Vector2.One;

	//? Position in the world
	public Vector2 ActualPosition
	{
		get
		{
			// Check for if we have a parent
			if (GameObject.Parent == null) return Position;

			// Combine ourself with the parent to get the actual position
			Transform2D parentsTransform = GameObject.Parent.GetComponent<Transform2D>();
			return parentsTransform.ActualPosition + Position;
		}
	}

	// eve arsenal lore
}