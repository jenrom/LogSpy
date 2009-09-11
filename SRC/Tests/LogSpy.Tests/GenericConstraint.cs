using Rhino.Mocks.Constraints;
namespace LogSpy.Tests
{
    public class GenericConstraint<T>: AbstractConstraint
    {
        private T parameter;

        public T GetParameter()
        {
            return parameter;
        }

        public override bool Eval(object obj)
        {
            parameter = (T) obj;
            return true;
        }

        public override string Message
        {
            get { return string.Empty; }
        }
    }
}