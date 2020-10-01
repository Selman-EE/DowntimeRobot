using System.Web.Mvc;

namespace DowntimeRobot.Admin.Filter
{
    public class TrimModelBinder : DefaultModelBinder
    {
        protected override void SetProperty(ControllerContext controllerContext, ModelBindingContext bindingContext,
      System.ComponentModel.PropertyDescriptor propertyDescriptor, object value)
        {
            if (propertyDescriptor.PropertyType == typeof(string))
            {
                var stringValue = (string)value;
                value = !string.IsNullOrWhiteSpace(stringValue) ? _ = stringValue.Trim() : null;
            }
            //
            base.SetProperty(controllerContext, bindingContext,
                                propertyDescriptor, value);
        }
    }
}