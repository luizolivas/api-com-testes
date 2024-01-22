namespace introApiWeb.Helpers
{
    public class ValidaNumTel
    {
        
        public static bool verificaNum(string tel)
        {
            string mascaraEsperada = @"^\(\d{2}\)\s\d{4}-\d{4}$";
            return System.Text.RegularExpressions.Regex.IsMatch(tel, mascaraEsperada);
        }
    }
}
