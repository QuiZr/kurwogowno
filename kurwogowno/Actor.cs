using System.Numerics;
using Raylib_cs;

namespace HelloWorld { 
  abstract class Actor {
    public Model model; 
    public Color color; 
    public string name;
    public virtual void Update(float delta) {}
    public void setPosition(Vector3 position) {
      this.position = position;
    }
    public void move(Vector3 position) {
      this.position += position;
    }
    public Vector3 position;
  }
}
