using System.Numerics;
using Raylib_cs;

namespace HelloWorld
{
    static class Program
    {
        public static void Main()
        {
            List<Actor> scene = new List<Actor>();
            Raylib.InitWindow(500, 480, "Hello World");
            Raylib.SetTargetFPS(60);
            var player = new Player();
            player.setPosition(new Vector3(0,0,0));
            scene.Add(player); 
            Camera3D camera = new Camera3D
            {
                position = new Vector3(0.0f, 10.0f, 10.0f), // Camera position
                target = new Vector3(0.0f, 0.0f, 0.0f), // Camera looking at point
                up = new Vector3(0.0f, 0f, -1.0f), // Camera up vector (rotation towards target)
                fovy = 45.0f, // Camera field-of-view Y
                projection = CameraProjection.CAMERA_PERSPECTIVE // Camera mode type
            };
            //Raylib.SetCameraMode(camera, CameraMode.CAMERA_ORBITAL);
            long lastT = currentTime(); 
            while (!Raylib.WindowShouldClose())
            {
                var time = currentTime();
                float deltaT = (time-lastT)/1000f;
                lastT = time;
                camera.target = player.position;
                Raylib.UpdateCamera(ref camera);

                foreach (var actor in scene) {
                  actor.Update(deltaT);
                }
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.WHITE);
                Raylib.BeginMode3D(camera);
                foreach (var actor in scene) {
                  Raylib.DrawModel(actor.model, actor.position, 1f, Color.RAYWHITE);
                }
                Raylib.EndMode3D();
                Raylib.DrawFPS(10, 10);
                Raylib.EndDrawing();
            }

            Raylib.CloseWindow();
        }
        public static long currentTime() {
            return DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        }
    }
}
