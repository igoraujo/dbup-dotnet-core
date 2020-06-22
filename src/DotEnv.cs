using System.IO;

namespace dbup.src
{
    public class DotEnv
    {
        public void Load()
        {
            using (var stream = File.OpenRead(@"./.env"))
            {
                DotNetEnv.Env.Load(stream);
            }
        }
    }
}
