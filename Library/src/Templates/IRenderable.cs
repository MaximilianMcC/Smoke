public interface IRenderable : IUpdatable
{
	/// <summary>
	/// Render 3D things in here
	/// </summary>
	void Render3D() {}

	/// <summary>
	/// Render 2D things in here
	/// </summary>
	void Render2D() {}
}