namespace MetalArchivesCore.Attributes.Abstract
{
    interface IConverterDecorator
    {
        object GetValue();
        void SetDecorator(IConverterDecorator converterDecorator);
    }
}
