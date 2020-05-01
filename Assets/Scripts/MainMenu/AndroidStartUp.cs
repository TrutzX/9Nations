using Classes;
using Libraries;
using Libraries.Helps;
using UI;

namespace MainMenu
{
    public class AndroidStartUp : IRun
    {
        public void Run()
        {
            LSys.tem.options["centermouse"].SetValue("true");
        }

        public string ID()
        {
            return "android";
        }
    }
}