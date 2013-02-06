namespace Trul.Framework.Rules
{
    public interface IValidatorElement
    {
        void Accept(IValidatorVisitor visitor);
    }
}