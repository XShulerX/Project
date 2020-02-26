using System;

public abstract class Worker
{
    private string Name { get; set; }
    private int Age { get; set; }

    protected abstract void Pay();
}
