namespace TegCMS.Pages.Data
{
    public class PageComponentControllerAction
    {
        private const string DEFAULT_ACITON = "Index";

        private string _action = DEFAULT_ACITON;

        public string Controller { get; set; }

        public string Action
        {
            get { return _action; }
            set
            {
                _action = value ?? DEFAULT_ACITON;
            }
        }
    }
}