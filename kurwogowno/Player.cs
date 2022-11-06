using System.Numerics;
using Raylib_cs;
using static Raylib_cs.GamepadAxis;
using static Raylib_cs.Raylib;
using static Raylib_cs.KeyboardKey;


namespace kurwogowno;

class Player : Actor
{
    private bool _antiGravity;
    private Vector3 _speedVector;

    public Player()
    {
        Name = "player";
        Model = LoadModel("quadrocopter.obj");
        Color = Color.RAYWHITE;
    }

    public override void Update(float dt)
    {
        RotationAxis = new Vector3(0, 0, 0);
        var forwardsBackwardsInput = GetGamepadAxisMovement(0, GAMEPAD_AXIS_RIGHT_Y);
        var leftRightInput = GetGamepadAxisMovement(0, GAMEPAD_AXIS_RIGHT_X);

        RotationAxis.X = forwardsBackwardsInput * -1;
        RotationAxis.Y = leftRightInput * -1;
        RotationAngle = (Math.Abs(forwardsBackwardsInput) + Math.Abs(leftRightInput)).Remap(0, 2, 0, 45);

        var rotation = Quaternion.CreateFromAxisAngle(Vector3.Normalize(RotationAxis), RotationAngle.Deg2Rad());
        var gravityVector = new Vector3(0, 0, -0.98f);
        var throttleMapped = GetGamepadAxisMovement(0, GAMEPAD_AXIS_LEFT_Y).Remap(-1f, 1f, 1.4f, -1.4f);
        var thrustVector = new Vector3(0, 0, throttleMapped);
        if (RotationAxis.LengthSquared() > 0)
        {
            thrustVector = Vector3.Transform(thrustVector, rotation);
        }

        if (thrustVector.LengthSquared() > 0)
        {
            Console.WriteLine($"thrust: x: {thrustVector.X}, y: {thrustVector.Y}  z: {thrustVector.Z}");
            _speedVector += thrustVector * dt;
        }

        if (_speedVector.LengthSquared() > 0)
        {
            Console.WriteLine($"speed: x: {_speedVector.X}, y: {_speedVector.Y}  z: {_speedVector.Z}");
        }

        if (IsKeyPressed(KEY_G) || IsGamepadButtonPressed(0, GamepadButton.GAMEPAD_BUTTON_RIGHT_FACE_UP))
        {
            _antiGravity = !_antiGravity;
        }

        if (!_antiGravity)
        {
            _speedVector += gravityVector * dt;
        }

        var friction = _speedVector * -.01f;
        if (friction.LengthSquared() > 0)
        {
            Console.WriteLine($"friction: x: {friction.X}, y: {friction.Y}  z: {friction.Z}");
        }

        _speedVector += friction;
        Move(_speedVector);
    }
}