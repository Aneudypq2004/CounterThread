namespace CounterThread
{
    public class Counter
    {
        private Thread thread;
        private bool isRunning;
        private readonly int interval;

        public int Id { get; }
        public int Value { get; private set; }
        public bool IsRunning => isRunning;

        public Counter(int id, int interval)
        {
            Id = id;
            this.interval = interval;
            Value = 0;
            isRunning = false;
        }

        public void Start()
        {
            isRunning = true;
            thread = new Thread(Run);
            thread.Start();
        }

        public void Stop()
        {
            isRunning = false;
            if (thread != null && thread.IsAlive)
            {
                thread.Join();
            }
        }

        private void Run()
        {
            while (isRunning)
            {
                Value++;
                Console.WriteLine($"[Contador {Id}] Valor actual: {Value}");
                Thread.Sleep(interval);
            }
        }
    }

}
