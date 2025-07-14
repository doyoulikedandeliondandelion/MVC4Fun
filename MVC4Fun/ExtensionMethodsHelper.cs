using Newtonsoft.Json;

namespace MVC4Fun
{
    public static class ExtensionMethodsHelper
    {
        public static string ToJsonStr(this object o, string def = "")
        {
            try
            {
                return JsonConvert.SerializeObject(o);
            }
            catch
            {
                return def;
            }
        }
    }
}
