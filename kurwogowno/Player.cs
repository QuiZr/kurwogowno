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

      this.rotationAxis = new Vector3(0,0,0);
      if (IsKeyDown(KEY_UP)) {
        this.rotationAngle = 21f;
        rotationAxis.X = 1;
      } else if (IsKeyDown(KEY_DOWN)) {
        this.rotationAngle = 21f;
        rotationAxis.X = -1;
      }
      if (IsKeyDown(KEY_LEFT)) {
        this.rotationAngle = 21f;
        rotationAxis.Y = 1;
      } else if (IsKeyDown(KEY_RIGHT)) {
        this.rotationAngle = 21f;
        rotationAxis.Y = -1;
      }
      var rotation = Quaternion.CreateFromAxisAngle(Vector3.Normalize(rotationAxis), rotationAngle );
      Console.WriteLine(rotation);
      var gravityVector = new Vector3(0,0,-dt*0.98f);
      var thrustVector = new Vector3(0,0,0);
      if (IsKeyDown(KEY_SPACE)) {
        thrustVector += new Vector3(0,0,dt*1.2f);
        if (rotationAxis.LengthSquared() > 0) {
          Console.WriteLine($"before, x: {thrustVector.X}, y: {thrustVector.Y}  z: {thrustVector.Z}");
          thrustVector = Vector3.Transform(thrustVector, rotation);
          Console.WriteLine($"after, x: {thrustVector.X}, y: {thrustVector.Y}  z: {thrustVector.Z}");
        }
      }
      speedVector += gravityVector;
      speedVector += thrustVector;

      var friction = speedVector*-1f*speedVector.LengthSquared();
      speedVector += friction;
      this.move(this.speedVector);


    }
  } 
}
