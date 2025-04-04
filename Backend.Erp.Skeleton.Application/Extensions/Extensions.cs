namespace Backend.Erp.Skeleton.Application.Extensions
{
    public static class Extensions
    {
        public static bool NotEquals(this object str, object comparer) => !str.Equals(comparer);

    }
}
