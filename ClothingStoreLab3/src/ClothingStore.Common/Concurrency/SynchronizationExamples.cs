namespace ClothingStore.Common.Concurrency;

public static class SynchronizationExamples
{
    private static readonly object Locker = new();

    public static int LockExample()
    {
        int counter = 0;

        Parallel.For(0, 1000, _ =>
        {
            lock (Locker)
            {
                counter++;
            }
        });

        return counter;
    }

    public static async Task<int> SemaphoreExampleAsync()
    {
        using SemaphoreSlim semaphore = new(2);
        int completed = 0;
        List<Task> tasks = new();

        for (int i = 0; i < 10; i++)
        {
            tasks.Add(Task.Run(async () =>
            {
                await semaphore.WaitAsync();
                try
                {
                    await Task.Delay(10);
                    Interlocked.Increment(ref completed);
                }
                finally
                {
                    semaphore.Release();
                }
            }));
        }

        await Task.WhenAll(tasks);
        return completed;
    }

    public static int AutoResetEventExample()
    {
        using AutoResetEvent autoResetEvent = new(false);
        int value = 0;

        Thread worker = new(() =>
        {
            autoResetEvent.WaitOne();
            value = 100;
        });

        worker.Start();
        autoResetEvent.Set();
        worker.Join();

        return value;
    }
}
