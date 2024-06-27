namespace BankApplication.Common
{
    public class SendMsg
    {
        public static SendAPI APIMsg(string result, string data)
        {
            try
            {
                SendAPI sendData = new();
                sendData.RESULT = result;
                sendData.DATA = data;

                return sendData;
            }
            catch (Exception ex)
            {
                Logs.Exception(ex);
                return null;
            }
        }
    }
}
