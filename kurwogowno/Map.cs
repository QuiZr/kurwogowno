using System.Numerics;
using Raylib_cs;

namespace HelloWorld {
  class Map: Actor {
    public Map() {
      this.name = "map";
      this.model = Raylib.LoadModel("map.obj");
      this.color = Color.GREEN;

    }  
    public override void Update(float dt) {
    }
  } 
}
