using System.Numerics;
using Raylib_cs;

namespace HelloWorld
{
    static class Program
    {
        public static void Main()
        {
            Raylib.InitWindow(500, 480, "Hello World");
            Raylib.SetTargetFPS(60);
            var model = Raylib.LoadModel("quadrocopter.obj");
            Camera3D camera = new Camera3D
            {
                position = new Vector3(0.0f, 10.0f, 10.0f), // Camera position
                target = new Vector3(0.0f, 0.0f, 0.0f), // Camera looking at point
                up = new Vector3(0.0f, 1.0f, 0.0f), // Camera up vector (rotation towards target)
                fovy = 45.0f, // Camera field-of-view Y
                projection = CameraProjection.CAMERA_PERSPECTIVE // Camera mode type
            };
            Raylib.SetCameraMode(camera, CameraMode.CAMERA_ORBITAL);
            while (!Raylib.WindowShouldClose())
            {
                Raylib.UpdateCamera(ref camera);

                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.WHITE);
                Raylib.BeginMode3D(camera);
                Raylib.DrawModelWires(model, Vector3.Zero, 1f, Color.RAYWHITE);
                Raylib.EndMode3D();
                Raylib.DrawFPS(10, 10);
                Raylib.EndDrawing();
            }

            Raylib.CloseWindow();
        }
    }
}