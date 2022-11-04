using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.KeyboardKey;


namespace HelloWorld {
  class Player: Actor {
    private Vector3 speedVector;
    public Player() {
      this.name = "player";
      this.model = Raylib.LoadModel("quadrocopter.obj");
      this.color = Color.RAYWHITE;
    }  
    public override void Update(float dt) {

      var maxVerticalSpeed = 1f; 
      var gravityVector = new Vector3(0,0,-dt*0.98f);
      var thrustVector = new Vector3(0,0,0);
      if (IsKeyDown(KEY_SPACE)) {
        thrustVector += new Vector3(0,0,dt*1.2f);
      }
      speedVector += gravityVector;
      speedVector += thrustVector;

      var friction = speedVector*-1f*speedVector.LengthSquared();
      speedVector += friction;
      this.move(this.speedVector);
    }
  } 
}
