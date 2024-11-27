using CounterThread;

class Program
{
    static Dictionary<int, Counter> counters = new Dictionary<int, Counter>();
    static bool exitProgram = false;

    static void Main(string[] args)
    {
        while (!exitProgram)
        {
            Console.Clear();
            Console.WriteLine("=== MENÚ DE CONTADORES ===");
            Console.WriteLine("1. Iniciar un nuevo contador");
            Console.WriteLine("2. Detener un contador");
            Console.WriteLine("3. Mostrar estado de los contadores");
            Console.WriteLine("4. Salir");
            Console.Write("Seleccione una opción: ");
            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    StartCounter();
                    break;
                case "2":
                    StopCounter();
                    break;
                case "3":
                    ShowCounters();
                    break;
                case "4":
                    ExitProgram();
                    break;
                default:
                    Console.WriteLine("Opción no válida. Presione cualquier tecla para continuar...");
                    Console.ReadKey();
                    break;
            }
        }
    }

    static void StartCounter()
    {
        Console.Write("Ingrese un ID para el contador: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            if (!counters.ContainsKey(id))
            {
                Console.Write("Ingrese el intervalo en milisegundos: ");
                if (int.TryParse(Console.ReadLine(), out int interval) && interval > 0)
                {
                    Counter counter = new Counter(id, interval);
                    counters[id] = counter;
                    counter.Start();
                    Console.WriteLine($"Contador {id} iniciado con intervalo de {interval} ms.");
                }
                else
                {
                    Console.WriteLine("Intervalo inválido.");
                }
            }
            else
            {
                Console.WriteLine($"Ya existe un contador con el ID {id}.");
            }
        }
        else
        {
            Console.WriteLine("ID inválido.");
        }
        Console.WriteLine("Presione cualquier tecla para continuar...");
        Console.ReadKey();
    }

    static void StopCounter()
    {
        Console.Write("Ingrese el ID del contador a detener: ");
        if (int.TryParse(Console.ReadLine(), out int id) && counters.ContainsKey(id))
        {
            counters[id].Stop();
            counters.Remove(id);
            Console.WriteLine($"Contador {id} detenido.");
        }
        else
        {
            Console.WriteLine("ID no válido o contador no encontrado.");
        }
        Console.WriteLine("Presione cualquier tecla para continuar...");
        Console.ReadKey();
    }

    static void ShowCounters()
    {
        Console.WriteLine("\n=== ESTADO DE LOS CONTADORES ===");
        if (counters.Count == 0)
        {
            Console.WriteLine("No hay contadores activos.");
        }
        else
        {
            foreach (var counter in counters.Values)
            {
                Console.WriteLine($"Contador {counter.Id}: Valor actual = {counter.Value}, Estado = {(counter.IsRunning ? "Activo" : "Detenido")}");
            }
        }
        Console.WriteLine("Presione cualquier tecla para continuar...");
        Console.ReadKey();
    }

    static void ExitProgram()
    {
        Console.WriteLine("Saliendo del programa...");
        foreach (var counter in counters.Values)
        {
            counter.Stop();
        }
        counters.Clear();
        exitProgram = true;
    }
}

