namespace STM32_IWDG_Calculator
{
    public partial class Form1 : Form
    {
        // IwdgCalculator példányosítása
        IwdgCalculator calculator = new IwdgCalculator();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // TextBox értékének beolvasása és átalakítása
                double millisec = Convert.ToDouble(textBox1.Text);

                // Prescaler és Reload kiszámítása
                var (prescaler, reload) = calculator.CalculateIwdgValues(millisec);

                // Eredmény megjelenítése a Label-ben
       
                textBox2.Text = $"Prescaler: {prescaler} Reload: {reload}";
            }
            catch (Exception ex)
            {
                // Hibakezelés: Hibás bevitel esetén hibaüzenet megjelenítése
          
                textBox2.Text = $"Hiba: {ex.Message}";
            }
        }
    }

    public class IwdgCalculator
    {
        private const double LSI_Frequency = 32000;
        private readonly int[] prescalers = { 4, 8, 16, 32, 64, 128, 256 };

        public (int prescaler, int reload) CalculateIwdgValues(double millisec)
        {
            double timeInSeconds = millisec / 1000;

            foreach (int prescaler in prescalers)
            {
                double reloadValue = (timeInSeconds * LSI_Frequency) / prescaler;

                if (reloadValue >= 0 && reloadValue <= 4095)
                {
                    return (prescaler, (int)Math.Round(reloadValue));
                }
            }

            throw new ArgumentException("sok lesz, nem? :)");
        }
    }
}
