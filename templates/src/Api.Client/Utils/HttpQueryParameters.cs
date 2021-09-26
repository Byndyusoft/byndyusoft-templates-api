namespace Byndyusoft.Template.Api.Client.Utils
{
    using System.Collections.Generic;
    using System.ComponentModel;

    //TODO переписать на дерево выражения
    public abstract class HttpQueryParameters
    {
        public string ToQueryString()
        {
            var args = new HashSet<string>();
            foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(this))
            {
                var value = descriptor.GetValue(this);
                if (value != null)
                {
                    args.Add($"{descriptor.Name}={descriptor.GetValue(this)}");
                }
            }

            return string.Join("&", args);
        }
    }

}