namespace Common.Library.Seedwork;

public abstract class ValueObject
{
protected abstract IEnumerable<object> GetEqualityComponents();

        public override bool Equals(object? obj)
        {
            if (obj == null || obj.GetType() != GetType())
                return false;

            return GetEqualityComponents()
                .SequenceEqual(((ValueObject)obj).GetEqualityComponents());
        }

        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Aggregate(0, (hash, component) => hash ^ (component?.GetHashCode() ?? 0));
        }

        public static bool operator ==(ValueObject left, ValueObject right)
        {
            if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
                return ReferenceEquals(left, right);
            return left.Equals(right);
        }

        public static bool operator !=(ValueObject left, ValueObject right)
        {
            return !(left == right);
        }
}