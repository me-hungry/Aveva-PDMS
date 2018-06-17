using Aveva.Pdms.Database;
using Ps = Aveva.Pdms.Database.DbPseudoAttribute;
using ATT = Aveva.Pdms.Database.DbAttributeInstance;

namespace UdaTestAddin
{
    public class UdaTestCode
    {

        public static void Run()
        {
            RegisterDelegate();
        }

        static public void RegisterDelegate()
        {
            // get Result uda attribute
            var userDefinedAttributeResult = DbAttribute.GetDbAttribute(":uResultTest");
            // Create instance of delegate containing "uResultTest" method
            var doubleDelegate = new Ps.GetStringDelegate(TeStCalculation);

            // Pass delegate instance to core PDMS. This will be invoked later
            // when :uResultTest is queried.
            // In this case registry for all valid element types.
            Ps.AddGetStringAttribute(userDefinedAttributeResult, doubleDelegate);
        }

        static private string TeStCalculation(DbElement ele, DbAttribute att, DbQualifier qualifier)
        {
            // get Expression uda attribute
            var userDefinedAttributeExp = DbAttribute.GetDbAttribute(":uExpTest");
            var value = string.Empty;
            try
            {
                // evaluate attribute from :uExpTest to :uResultTest
                value = ele.EvaluateAsString(DbExpression.Parse(ele.GetAsString(userDefinedAttributeExp)));
            }
            catch (System.Exception)
            {
                value = "Default value";
            }

            // Result of UDA must be returned
            return value;
        }
    }
}
