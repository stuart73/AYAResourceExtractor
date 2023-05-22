
namespace AYAResourceExtractor
{
    internal class Log
    {
        public object thislock =  new();
        private static Log? instance;
        public string Messages { get; private set; } = "";
        public TextBox? TextBox { get; set; } = null;

        public static Log Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Log();
                }
                return instance;
            }
        }

        private void UpdateTextBox()
        {
            if (TextBox != null)
            {
                if (TextBox.InvokeRequired)
                {
                    TextBox.Invoke(new Action(UpdateTextBox));
                    return;
                }
                lock (thislock)
                {
                    TextBox.Text = Messages;
                }

                TextBox.SelectionStart = TextBox.TextLength;
                TextBox.ScrollToCaret();
            }
        }

        public void Clear()
        {
            lock (thislock)
            {
                Messages = "";
            }
            UpdateTextBox();
        }


        public static void Error(string message)
        {
            string entry = "Error:" + message + "\r\n";

            lock (Instance.thislock)
            {
                Instance.Messages += entry;
            }
            Instance.UpdateTextBox();
        }

        public static void AddMessage(string message)
        {
            string entry = message +"\r\n";

            lock (Instance.thislock)
            {
                Instance.Messages += entry;
            }
            Instance.UpdateTextBox();
        }
    }
}
