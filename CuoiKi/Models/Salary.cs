namespace CuoiKi.Models
{
    public class Salary : ModelBase
    {
        public override string ID { get; }
        public int BasicPay { get; set; }
        public int KPICost { get; set; }
        public Salary(string ID, int BasicPay, int KPICost)
        {
            this.ID = ID;
            this.BasicPay = BasicPay;
            this.KPICost = KPICost;
        }
    }
}
