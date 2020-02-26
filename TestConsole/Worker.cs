public abstract class Worker
{
    protected string _Name;
    protected int _Age;
    protected double _Price;

    public string Name => _Name;
    public int Age => _Age;

    protected Worker(int Age, string Name)
    {
        _Name = Name;
        _Age = Age;
    }

    protected abstract void Pay();

    public override string ToString() => $"Name: {Name}, Age: {Age}, Price: {_Price}";
}
