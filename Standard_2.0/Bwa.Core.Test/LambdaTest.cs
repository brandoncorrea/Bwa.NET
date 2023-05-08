namespace Bwa.Core.Test;

[TestFixture]
public class LambdaTest
{
    [Test]
    public void Times()
    {
        (-4).Times(() => throw new Exception());
        (-4).Times(i => throw new Exception());
        (-4).Times(i =>
        {
            throw new Exception();
            return 0;
        });
        (-4).Times(() =>
        {
            throw new Exception();
            return 0;
        });
        0.Times(() => throw new Exception());

        var sum = 0;
        10.Times(() => { sum += 1; });

        var ones = 3.Times(() => 1).ToArray();
        Assert.AreEqual(3, ones.Length);
        Assert.AreEqual(1, ones[0]);
        Assert.AreEqual(1, ones[1]);
        Assert.AreEqual(1, ones[2]);

        var range = 3.Times(i => i).ToArray();
        Assert.AreEqual(3, range.Length);
        Assert.AreEqual(0, range[0]);
        Assert.AreEqual(1, range[1]);
        Assert.AreEqual(2, range[2]);
    }

    [Test]
    public void SwallowsErrorsOnTry()
    {
        Lambda.Try(() => throw new Exception());
        int value = 0;
        Lambda.Try(() => value++);
        Assert.AreEqual(1, value);
    }

    [Test]
    public async Task InvokesMultithreadedAction()
    {
        var value = 0;
        var start = DateTime.Now;
        await Lambda.InvokeParallel(4, () =>
        {
            value++;
            Thread.Sleep(50);
        });
        var end = DateTime.Now;
        Assert.AreEqual(4, value);
        Assert.IsTrue(new TimeSpan(1000000) > end - start);
    }

    [Test]
    public void AppliesManyFunctionsToValue()
    {
        Assert.AreEqual(5, 1.DoTo(i => i + 1, i => i + 3));
        Assert.AreEqual("Hello World!", "Hello".DoTo(s => s + " ", s => s + "World", s => s + "!"));
    }

    [Test]
    public void WaitsForValue()
    {
        int acc = 0;
        Func<int> func = () => ++acc;
        Assert.IsTrue(func.WaitFor(3));
        Assert.AreEqual(3, acc);

        var start = DateTime.Now;
        Assert.IsTrue(func.WaitFor(6, waitMs: 10));
        var end = DateTime.Now;
        Assert.IsTrue(end - start >= new TimeSpan(30000));

        Assert.IsTrue(func.WaitFor(() => acc == 10));
        Assert.AreEqual(10, acc);

        Action action = () => func();
        Assert.IsTrue(action.WaitFor(() => acc == 15));
        Assert.AreEqual(15, acc);

        Assert.IsFalse(func.WaitFor(20, retries: 3));
        Assert.AreEqual(19, acc);

        Assert.IsFalse(func.WaitFor(22, retries: 0));
        Assert.AreEqual(20, acc);
        Assert.IsTrue(func.WaitFor(21, retries: 0));
        Assert.AreEqual(21, acc);
        Assert.IsTrue(action.WaitFor(() => acc == 22, retries: 0));
        Assert.AreEqual(22, acc);
    }

    [Test]
    public async Task WaitsForValueAsync()
    {
        int acc = 0;
        Func<Task<int>> func = () => Task.FromResult(++acc);
        Assert.IsTrue(await func.WaitForAsync(3));
        Assert.AreEqual(3, acc);

        var start = DateTime.Now;
        Assert.IsTrue(await func.WaitForAsync(6, waitMs: 10));
        var end = DateTime.Now;
        Assert.IsTrue(end - start >= new TimeSpan(30000));

        Assert.IsTrue(await func.WaitForAsync(() => acc == 10));
        Assert.AreEqual(10, acc);

        Func<Task> action = async () => await func();
        Assert.IsTrue(await action.WaitForAsync(() => acc == 15));
        Assert.AreEqual(15, acc);

        Assert.IsFalse(await func.WaitForAsync(20, retries: 3));
        Assert.AreEqual(19, acc);

        Assert.IsFalse(await func.WaitForAsync(22, retries: 0));
        Assert.AreEqual(20, acc);
        Assert.IsTrue(await func.WaitForAsync(21, retries: 0));
        Assert.AreEqual(21, acc);
        Assert.IsTrue(await action.WaitForAsync(() => acc == 22, retries: 0));
        Assert.AreEqual(22, acc);
    }
}
