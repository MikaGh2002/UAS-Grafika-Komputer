using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;

namespace _3D_Object
{
    class Program
    {
        static void Main(string[] args)
        {
            var ourWindow = new NativeWindowSettings()
            {
                Size = new Vector2i(1920, 1080),
                Title = "ConsoleApp1"
            };

            using (var window = new Window(GameWindowSettings.Default, ourWindow))
            {
                window.Run();
            }
        }
    }
}
