using UnityEngine;

public class GizmosCall{
	public Color Color;
	public GizmosShape Shape;
	public Vector3 Origin;
	public Vector3 Destination;
	public float Radius;

	public GizmosCall(Color _color, GizmosShape _shape, Vector3 _origin, Vector3 _destination, float _radius){
		Color = _color;
		Shape = _shape;
		Origin = _origin;
		Destination = _destination;
		Radius = _radius;
	}
}
