using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.KeyboardKey;


namespace HelloWorld {
  class Player: Actor {
    private Boolean antiGravity = false;
    private Vector3 speedVector;
    public Player() {
      this.name = "player";
      this.model = Raylib.LoadModel("quadrocopter.obj");
      this.color = Color.RAYWHITE;
    }  
    public override void Update(float dt) {

      this.rotationAxis = new Vector3(0,0,0);
      if (IsKeyDown(KEY_UP)) {
        this.rotationAngle = 45f;
        rotationAxis.X = 1;
      } else if (IsKeyDown(KEY_DOWN)) {
        this.rotationAngle = 45f;
        rotationAxis.X = -1;
      }
      if (IsKeyDown(KEY_LEFT)) {
        this.rotationAngle = 45f;
        rotationAxis.Y = 1;
      } else if (IsKeyDown(KEY_RIGHT)) {
        this.rotationAngle = 45f;
        rotationAxis.Y = -1;
      }
      var rotation = Quaternion.CreateFromAxisAngle(Vector3.Normalize(rotationAxis), deg2rad(rotationAngle));
      var gravityVector = new Vector3(0,0,-0.98f);
      var thrustVector = new Vector3(0,0,0);
      if (IsKeyDown(KEY_A)) {
        thrustVector += new Vector3(0,0,1.4f);
      } else if (IsKeyDown(KEY_Z)) {
        thrustVector += new Vector3(0,0,-1.4f);
      }
      if (rotationAxis.LengthSquared() > 0) {
        thrustVector = Vector3.Transform(thrustVector, rotation);
      }
      if (thrustVector.LengthSquared() > 0) {
        Console.WriteLine($"thrust: x: {thrustVector.X}, y: {thrustVector.Y}  z: {thrustVector.Z}");
        speedVector += thrustVector*dt;
      }
      if (speedVector.LengthSquared() > 0) {
        Console.WriteLine($"speed: x: {speedVector.X}, y: {speedVector.Y}  z: {speedVector.Z}");
      }

      if (IsKeyPressed(KEY_G)) {
        this.antiGravity = !this.antiGravity;
      }
      if (!antiGravity) {
        speedVector += gravityVector*dt;
      }
      var friction = speedVector*-.01f;
      if (friction.LengthSquared() > 0) {
        Console.WriteLine($"friction: x: {friction.X}, y: {friction.Y}  z: {friction.Z}");
      }
      speedVector += friction;
      this.move(this.speedVector);


    }
    public static float deg2rad(float deg) {
      return (deg * (float)Math.PI)/180f; 
    }
  } 

}
