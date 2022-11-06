using System.Numerics;
using Raylib_cs;

namespace kurwogowno { 
  abstract class Actor {
    public Model Model; 
    public Color Color; 
    public string Name;

    public Vector3 Position;
    public Vector3 RotationAxis;
    public float RotationAngle;


    public virtual void Update(float delta) {}
    public void SetPosition(Vector3 position) {
      Position = position;
    }
    public void Move(Vector3 position) {
      Position += position;
    }
  }
}
