using System.Numerics;
using Raylib_cs;

namespace HelloWorld {
  class Player: Actor {
    public Player() {
      this.name = "player";
      this.model = Raylib.LoadModel("quadrocopter.obj");
    }  
    public override void Update(float dt) {
      this.move(new Vector3(0,0,-dt));
    }
  } 
}
