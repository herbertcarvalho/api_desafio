namespace Tests.Interfaces
{
    public interface ITest
    {
        Task ExecuteTestsAsync(Tuple<List<int>, List<string>> report);
    }
}
