using System;
using System.ComponentModel;
using System.Linq.Expressions;


namespace LogSpy.UI
{
    public static class Extensions
    {
        public static void For<T>(this PropertyChangedEventHandler handler, T sender, Expression<Func<T,object>> propertyNameSelector)
        {
            if(handler != null)
            {
                string propertyName =  null;
                var lambdaExpression = propertyNameSelector as LambdaExpression;
                if(lambdaExpression != null)
                {
                    var memberExpression = lambdaExpression.Body as MemberExpression;
                    if(memberExpression != null)
                    {
                        propertyName = memberExpression.Member.Name;
                    }
                }
                if(propertyName == null)
                {
                    throw new ArgumentException("Could not evaluate the changed property name from the expression",
                                                "propertyNameSelector");
                }
                handler(sender, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}