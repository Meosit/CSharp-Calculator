using System.ServiceModel;
using System.ServiceModel.Web;

namespace WcfCalculator
{
    [ServiceContract(Name = "MySuperCalculator", Namespace = "mpp.mksn.by/MySuperCalculator")]
    public interface ICalculator
    {
        [OperationContract]
        [WebInvoke]
        [WebGet]
        [FaultContract(typeof(MathFault))]
        double Add(double a, double b);

        [OperationContract]
        [WebInvoke]
        [WebGet]
        [FaultContract(typeof(MathFault))]
        double Substract(double a, double b);

        [OperationContract]
        [WebInvoke]
        [WebGet]
        [FaultContract(typeof(MathFault))]
        double Multiply(double a, double b);

        [OperationContract]
        [WebInvoke]
        [WebGet]
        [FaultContract(typeof(MathFault))]
        double Divide(double a, double b);

        [OperationContract]
        [WebInvoke]
        [WebGet]
        [FaultContract(typeof(MathFault))]
        double Sqrt(double a);
    }
}