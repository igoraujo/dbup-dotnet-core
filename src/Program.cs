using DbUp;
using DotNetEnv;
using System;
using System.Linq;
using System.Threading;

namespace dbup.src
{
    class Program
    {
        private static bool _running;
        public static void Main(string[] args)
        {
            _running = true;

            Console.WriteLine("Iniciando DBUP...");

            while (_running)
            {
                Migrations(args);
                Thread.Sleep(2000);
            }

            Console.WriteLine("precione qualquer tecla para sair");
            Console.ReadKey();
        }

        private static void Migrations(string[] args)
        {
            new DotEnv().Load();

            string server = Env.GetString("SERVER");
            string database = Env.GetString("DATABASE");
            string userId = Env.GetString("USER_ID");
            string pass = Env.GetString("PASSWORD");
            string path = Env.GetString("PATH");

            var connectionString = args.FirstOrDefault() ?? $@"Server={server};Database={database};User Id={userId};Password={pass}";

            var upgrader = DeployChanges.To
               .SqlDatabase(connectionString)
               //.JournalToSqlTable("MySchema", "MyTable") // mudar o nome padrao da tabela de versionamento
               .WithScriptsFromFileSystem(path)
               //.WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
               .LogToConsole()
               .Build();

            var result = upgrader.PerformUpgrade();

            var scriptsExecuted = result.Scripts;

            if (scriptsExecuted.FirstOrDefault() == null)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Success!");
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine(" - Not Content!");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Success!");

                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkCyan;

                Console.WriteLine("Executed:");
                var scriptsExecuteds = scriptsExecuted.ToList();

                foreach (var item in scriptsExecuteds)
                {
                    Console.WriteLine($" - {item.Name}");
                }

                Console.ResetColor();
            }

            if (!result.Successful)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(result.Error);
                Console.ResetColor();

                Console.ReadLine();
            }

        }
    }
}
