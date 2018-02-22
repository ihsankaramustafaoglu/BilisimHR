using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;
using System.ComponentModel.DataAnnotations;

namespace bilisimHR.DataLayer.NHibernate.Helper
{
    public class StringLengthConvention : AttributePropertyConvention<StringLengthAttribute>
    {

        protected override void Apply(StringLengthAttribute attribute, IPropertyInstance instance)
        {
            instance.Length(attribute.MaximumLength);
        }
    }
}
