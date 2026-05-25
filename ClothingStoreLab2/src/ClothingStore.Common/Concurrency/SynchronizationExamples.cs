namespace ClothingStore.Common.Concurrency;

public static class SynchronizationExamples
{
    private static readonly object CounterLock = new();

    public static int LockExample()
    {
        int counter = 0;

        Parallel.For(0, 1000, _ =>
        {
            // lock не дозволяє кільком потокам одночасно змінювати counter
            lock (CounterLock)
            {
                counter++;
            }
        });

        return counter;
    }

    public static async Task<int> SemaphoreExampleAsync()
    {
        using SemaphoreSlim semaphore = new(3, 3);
        int completedOperations = 0;

        IEnumerable<Task> tasks = Enumerable.Range(1, 10).Select(async _ =>
        {
            // Semaphore обмежує кількість одночасних операцій
            await semaphore.WaitAsync();

            try
            {
                await Task.Delay(10);
                Interlocked.Increment(ref completedOperations);
            }
            finally
            {
                semaphore.Release();
            }
        });

        await Task.WhenAll(tasks);
        return completedOperations;
    }

    public static int AutoResetEventExample()
    {
        using AutoResetEvent autoResetEvent = new(false);
        int value = 0;

        Thread worker = new(() =>
        {
            value = 100;

            // Сигналізуємо основному потоку, що робота завершена
            autoResetEvent.Set();
        });

        worker.Start();

        // Основний потік чекає сигналу від worker-потоку
        autoResetEvent.WaitOne();
        worker.Join();

        return value;
    }
}
