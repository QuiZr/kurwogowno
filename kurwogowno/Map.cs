using Raylib_cs;

namespace kurwogowno;

class Map : Actor
{
    public Map()
    {
        Name = "map";
        Model = Raylib.LoadModel("map.obj");
        Color = Color.GREEN;
    }

    public override void Update(float dt)
    {
    }
}