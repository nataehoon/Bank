namespace BankApplication.Common
{
    public class Common
    {
        public static string CreateUUID()
        {
            Guid newGuid = Guid.NewGuid();
            return newGuid.ToString(); ;
        }
    }
}
