using System;
using System.CodeDom;
using System.Web.UI;
using System.ComponentModel;
using System.Web.Compilation;
using System.Web.UI.Design;
using System.Web;

// Apply ExpressionEditorAttributes to allow the  
// expression to appear in the designer.
[ExpressionPrefix("databaseExpression")]
[ExpressionEditor("databaseExpressionEditor")]
public class databaseExpressionBuilder : ExpressionBuilder
{
    //// Create a method that will return the result  
    //// set for the expression argument. 
    public static object GetEvalData(string expression, Type target, string entry)
    {
        //return expression;

        // First make sure that the Session collection will be available 
        if (HttpContext.Current == null)
            return null;

        // Get the value from Session 
        object value = HttpContext.Current.Session[expression];

        // Make sure that the specified Session variable exists 
        if (value == null)
            HttpContext.Current.Response.Redirect("~/Default.aspx");
            //throw new InvalidOperationException(string.Format("Session variable '{0}' not found.", expression));

        // Convert the Session variable if its type does not match up with the Web control property type 
        if (target != null)
        {
            PropertyDescriptor propDesc = TypeDescriptor.GetProperties(target)[entry];
            if (propDesc != null && propDesc.PropertyType != value.GetType())
            {
                // Type mismatch - make sure that the Session variable value can be converted to the Web control property type 
                if (propDesc.Converter.CanConvertFrom(value.GetType()) == false)
                    throw new InvalidOperationException(string.Format("Session variable '{0}' cannot be converted to type {1}.", expression, propDesc.PropertyType.ToString()));
                else
                    return propDesc.Converter.ConvertFrom(value);
            }
        }

        // If we reach here, no type mismatch - return the value 
        return value;
    }

    public override CodeExpression GetCodeExpression(BoundPropertyEntry entry, object parsedData, ExpressionBuilderContext context)
    {
        Type type1 = entry.DeclaringType;
        PropertyDescriptor descriptor1 = TypeDescriptor.GetProperties(type1)[entry.PropertyInfo.Name];
        CodeExpression[] expressionArray1 = new CodeExpression[3];
        expressionArray1[0] = new CodePrimitiveExpression(entry.Expression.Trim());
        expressionArray1[1] = new CodeTypeOfExpression(type1);
        expressionArray1[2] = new CodePrimitiveExpression(entry.Name);
        return new CodeCastExpression(descriptor1.PropertyType, new CodeMethodInvokeExpression(new CodeTypeReferenceExpression(base.GetType()), "GetEvalData", expressionArray1));
    }
}