namespace _Scripts.Helpers
{
    public class LogHelper
    {
        public static string Red = "red";
        public static string Green = "green";
        public static string Yellow = "yellow";
        
        public static string Color(string text, string color)
        {
            return $"<color={color}>{text}</color>";
        }

        public static string ActionTag => Color("Action", Green);
        public static string AITag => Color("AI", Red);
    }
}