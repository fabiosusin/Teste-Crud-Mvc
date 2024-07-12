using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace Useful.Extensions
{
    public static class StringExtension
    {

        public const string DefaultErrorMessage = "Ocorreu um erro, tente novamente e caso o erro persista, entre em contato com a nossa equipe de suporte!";
        public const string DefaultSubjectEmail = "financeiro@xplay.digital";

        public static bool ZipCodeIsValid(this string self) => !string.IsNullOrEmpty(self) && self.GetDigits()?.Length == 8;

        public static string GetDigits(this string self) =>
            self == null ? null : new Regex(@"[^\d]").Replace(self, "");

        public static bool IsValidEmail(this string email)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper, RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                static string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        public static bool IsCnpjOrCpf(this string data) => data?.Length == 11 ? IsCpf(data) : IsCnpj(data);

        public static bool IsCnpj(this string cnpj)
        {
            if (string.IsNullOrEmpty(cnpj))
                return false;

            var multiplier1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            var multiplier2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
            if (cnpj.Length != 14)
                return false;

            var tempCnpj = cnpj[..12];
            var sum = 0;
            for (int i = 0; i < 12; i++)
                sum += int.Parse(tempCnpj[i].ToString()) * multiplier1[i];

            var remainder = (sum % 11);
            remainder = remainder < 2 ? 0 : 11 - remainder;

            var digit = remainder.ToString();
            tempCnpj += digit;
            sum = 0;
            for (int i = 0; i < 13; i++)
                sum += int.Parse(tempCnpj[i].ToString()) * multiplier2[i];

            remainder = (sum % 11);
            remainder = remainder < 2 ? 0 : 11 - remainder;
            digit += remainder.ToString();
            return cnpj.EndsWith(digit);
        }

        public static bool IsCpf(this string cpf)
        {
            if (string.IsNullOrEmpty(cpf))
                return false;

            var multiplier1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            var multiplier2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;

            var tempCpf = cpf[..9];
            var sum = 0;
            for (int i = 0; i < 9; i++)
                sum += int.Parse(tempCpf[i].ToString()) * multiplier1[i];

            var remainder = (sum % 11);
            remainder = remainder < 2 ? 0 : 11 - remainder;

            var digit = remainder.ToString();
            tempCpf += digit;
            sum = 0;
            for (int i = 0; i < 10; i++)
                sum += int.Parse(tempCpf[i].ToString()) * multiplier2[i];

            remainder = (sum % 11);
            remainder = remainder < 2 ? 0 : 11 - remainder;
            digit += remainder.ToString();
            return cpf.EndsWith(digit);
        }

        public static bool CellphoneIsValid(this string number) => number?.Length switch
        {
            11 => Regex.IsMatch(number, @"^\(?\d{2}\)?[\s-]?[\s9]?\d{4}-?\d{4}$"),
            13 => Regex.IsMatch(number, @"^\d{2}?\(?\d{2}\)?[\s-]?[\s9]?\d{4}-?\d{4}$"),
            _ => false,
        };
    }
}
