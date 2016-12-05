using System.Runtime.Serialization;

namespace WcfCalculator
{
    [DataContract(Namespace = "mpp.mksn.by/MySuperCalculator")]
    public class MathFault
    {
        [DataMember]
        public string Operation { get; set; }

        [DataMember]
        public string ProblemType { get; set; }
    }
}