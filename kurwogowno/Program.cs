using System.Numerics;
using Examples;
using Raylib_cs;

namespace HelloWorld
{
    static class Program
    {
        public static void Main()
        {
            List<Actor> scene = new List<Actor>();
            Raylib.InitWindow(1280, 720, "Hello World");
            Raylib.SetTargetFPS(60);
            var player = new Player();
            player.setPosition(new Vector3(0,0,0));
            scene.Add(player);

            // load lightning shader
            var shader = Raylib.LoadShader("lighting.vs", "lighting.fs");
            unsafe
            {
                shader.locs[(int)ShaderLocationIndex.SHADER_LOC_VECTOR_VIEW] = Raylib.GetShaderLocation(shader, "viewPos");
            }
            
            // set ambient lightning
            var ambientLoc = Raylib.GetShaderLocation(shader, "ambient");
            Raylib.SetShaderValue(shader, ambientLoc, new float[] {0.1f, 0.1f, 0.1f, 1f}, ShaderUniformDataType.SHADER_UNIFORM_VEC4);

            // create lights (max 4 per scene with this shit shader)
            var lights = new Light[4];
            lights[0] = Rlights.CreateLight(0, LightType.LIGHT_POINT, new Vector3(-2, 1, -2), Vector3.Zero, Color.PINK, shader);

            var map = new Map();
            unsafe
            {
                player.model.materials[0].shader = shader;
                map.model.materials[0].shader = shader;
            }
            scene.Add(map);
            Camera3D camera = new Camera3D
            {
                position = new Vector3(15.0f, 15.0f, 0.0f), // Camera position
                target = new Vector3(0.0f, 0.0f, 0.0f), // Camera looking at point
                up = new Vector3(0.0f, 0f, 1.0f), // Camera up vector (rotation towards target)
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
                  Raylib.DrawModelEx(actor.model, actor.position, actor.rotationAxis, actor.rotationAngle, new Vector3(1f, 1f, 1f), actor.color);
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
