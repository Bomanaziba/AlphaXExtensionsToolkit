namespace  AlphaX.Extensions.Generics.Tests.Model
{
    public class TestClass1
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public override bool Equals(object obj)
        {
            if (obj is not TestClass1 other) return false;
            return Id == other.Id && Name == other.Name;
        }
        public override int GetHashCode() => HashCode.Combine(Id, Name);

    }
}