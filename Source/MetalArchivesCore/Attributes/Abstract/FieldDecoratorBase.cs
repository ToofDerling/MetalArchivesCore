namespace MetalArchivesCore.Attributes.Abstract
{
    abstract class FieldDecoratorBase : Attribute, IConverterDecorator
    {
        protected IConverterDecorator _decorator;

        public virtual object GetValue()
        {
                return _decorator != null ? _decorator.GetValue() : null;
        }

        public virtual void SetDecorator(IConverterDecorator converterDecorator)
        {
            _decorator = converterDecorator;
        }
    }
}
